using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DataContext;
using Project.Services.Interfaces;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService service;
        private readonly IEmailService emailService;
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;

        public PostController(IPostService service, IEmailService emailService, IConfiguration configuration, ApplicationDbContext context)
        {
            this.service = service;
            this.emailService = emailService;
            this.configuration = configuration;
            this.context = context;
        }

        [HttpGet("{id}")]
        public async Task<PostDto> GetById(int id)
        {
            return await service.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<List<PostDto>> GetAll()
        {
            return await service.GetAllAsync();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PostDto>> Post(PostDto post)
        {
            var addedPost = await service.AddAsync(post);
            if (addedPost == null)
            {
                return BadRequest("Failed to add post");
            }
            return Ok(addedPost);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task Put(PostDto post)
        {
            await service.UpdateAsync(post);
        }

        // Endpoint to like a post
        [HttpPost("like/{id}")]
        public async Task<IActionResult> LikePost(int id)
        {
            try
            {
                var likesCount = await service.LikePostAsync(id);
                return Ok(likesCount);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Endpoint to send a report email to the site manager
        [Authorize]
        [HttpPost("send-report-email/{postId}")]
        public async Task<IActionResult> SendReportEmail(int postId)
        {
            // Retrieve post data
            PostDto post = await service.GetByIdAsync(postId);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            // Get site manager's email and name from configuration
            string? siteManagerEmail = configuration["EmailSettings:SiteManagerEmail"];
            string? siteManagerName = configuration["EmailSettings:SiteManagerName"];

            if (string.IsNullOrEmpty(siteManagerEmail))
            {
                throw new InvalidOperationException("Missing configuration: EmailSettings:SiteManagerEmail");
            }

            if (string.IsNullOrEmpty(siteManagerName))
            {
                throw new InvalidOperationException("Missing configuration: EmailSettings:siteManagerName");
            }

            // Generate a unique token for deletion request
            var token = Guid.NewGuid().ToString();
            var deleteToken = new DeleteTokenDto
            {
                PostId = postId,
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(48)
            };
            context.DeleteTokens.Add(deleteToken);
            await context.SaveChangesAsync();

            // Create a link for deleting the post
            string deleteLink = $"http://localhost:3000/delete-post/{token}";
            // Send report email to the site manager
            await emailService.SendReportEmailAsync(siteManagerName, siteManagerEmail, post.Content, deleteLink);
            return Ok("Email sent");
        }

        // Endpoint to get the reported post using a token (used for admin deletion)
        [HttpGet("delete-post/{token}")]
        public async Task<IActionResult> GetPostByToken(string token)
        {
            // Retrieve the token from the database
            var deleteToken = await context.DeleteTokens.FirstOrDefaultAsync(t => t.Token == token);
            if (deleteToken == null || deleteToken.Expiration < DateTime.UtcNow)
            {
                return NotFound("Token not found or expired");
            }

            // Remove the post
            var post = await context.Posts.FirstOrDefaultAsync(p => p.Id == deleteToken.PostId);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            return Ok(new { post.Content });
        }

        // Remove the reported post and delete the token
        [HttpDelete("delete-post/{token}")]
        public async Task<IActionResult> DeletePost(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is missing");
            }

            var deleteToken = await context.DeleteTokens.FirstOrDefaultAsync(dt => dt.Token == token);

            if (deleteToken == null || deleteToken.Expiration < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired token");
            }

            var post = await context.Posts.FindAsync(deleteToken.PostId);
            if (post == null)
            {
                return NotFound();
            }

            context.Posts.Remove(post);
            await context.SaveChangesAsync();

            context.DeleteTokens.Remove(deleteToken);
            await context.SaveChangesAsync();

            return Ok("Post deleted successfully");
        }
    }
}
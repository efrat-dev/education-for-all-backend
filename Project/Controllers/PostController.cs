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
        private readonly IDeleteTokenService deleteTokenService;
        private readonly IWebHostEnvironment env;
        private readonly ApplicationDbContext context;

        public PostController(IPostService service, IEmailService emailService, IDeleteTokenService deleteTokenService, IConfiguration configuration, IWebHostEnvironment env, ApplicationDbContext context)
        {
            this.service = service;
            this.deleteTokenService = deleteTokenService;
            this.context = context;
            this.env = env;
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

        [Authorize]
        [HttpPost("send-report-email/{postId}")]
        public async Task<IActionResult> SendReportEmail(int postId)
        {
            var post = await service.GetByIdAsync(postId);
            if (post == null) return NotFound("Post not found");

            // Create a link for deleting the post
            bool isDevelopment = env.IsDevelopment();

            var token = await deleteTokenService.GenerateTokenAsync(postId);

            await service.SendReportEmailAsync(post, token, isDevelopment);
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

        [HttpDelete("delete-post/{token}")]
        public async Task<IActionResult> DeletePost(string token)
        {
            var deleteToken = await deleteTokenService.GetTokenAsync(token);
            if (deleteToken == null) return BadRequest("Invalid or expired token");

            await service.DeleteByIdAsync(deleteToken.PostId);
            await deleteTokenService.DeleteTokenAsync(token);
            return Ok("Post deleted successfully");
        }
    }
}
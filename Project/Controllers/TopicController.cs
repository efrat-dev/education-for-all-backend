using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Services.Interfaces;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IForumService<TopicDto> _topicService;
        private readonly IPostService _postService;
        private readonly ITopicReadingService _topicReadingService;

        public TopicController(IForumService<TopicDto> topicService, IPostService postService, ITopicReadingService topicReadingService)
        {
            Console.WriteLine("הגעתי לקונטרולר של טופיק");
            _topicService = topicService;
            _postService = postService;
            _topicReadingService = topicReadingService;
        }

        [HttpGet("{id}")]
        public async Task<TopicDto> GetById(int id)
        {
            return await _topicService.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<List<TopicDto>> GetAll()
        {
            Console.WriteLine("הגעתי לקונטרולר של טופיק לפונקציה גטאול");
            return await _topicService.GetAllAsync();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] TopicDto topic)
        {
            try
            {
                Console.WriteLine("אני בתוך פוסט של טופיק");
                var addedTopic = await _topicService.AddAsync(topic);
                Console.WriteLine("אחרי הסרויויס"+ addedTopic);
                if (addedTopic == null)
                {
                    return BadRequest(new { message = "Failed to add topic" });
                }
                return Ok(addedTopic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<TopicDto> Put(TopicDto topic)
        {
            await _topicService.UpdateAsync(topic);
            return topic;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _topicService.DeleteByIdAsync(id);
        }

        [HttpGet("{id}/posts")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsByTopicId(int id)
        {
            try
            {
                var posts = await _postService.GetPostsByTopicIdAsync(id);
                if (posts == null || !posts.Any())
                {
                    return NotFound(new { message = "No posts found for this topic" });
                }
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("{id}/read")]
        public async Task<IActionResult> ReadTopic(int id)
        {
            try
            {
                var topic = await _topicService.GetByIdAsync(id);
                if (topic == null)
                {
                    return NotFound(new { message = "Topic not found" });
                }

                await _topicReadingService.ReadTopicAsync(topic);
                return Ok(new { message = "Reading started successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}

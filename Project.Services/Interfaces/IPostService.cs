using Common.Dto;

namespace Project.Services.Interfaces
{
    public interface IPostService : IForumService<PostDto>
    {
        Task<List<PostDto>> GetPostsByTopicIdAsync(int topicId);
        Task<int> LikePostAsync(int postId);
    }
}

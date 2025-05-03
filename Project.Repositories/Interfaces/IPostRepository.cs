using Project.Repositories.Entities;

namespace Project.Repositories.Interfaces
{
    public interface IPostRepository : IForumRepository<Post>
    {
        Task<List<Post>> GetPostsByTopicIdAsync(int topicId);
    }
}

using Microsoft.EntityFrameworkCore;
using Project.Repositories.Entities;
using Project.Repositories.Interfaces;

namespace Project.Repositories.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IContext context;

        public PostRepository(IContext context)
        {
            this.context = context;
        }

        public async Task<Post> AddAsync(Post post)
        {
            post.Date = DateTime.Now;
            await context.Posts.AddAsync(post);
            await context.Save();
            return post;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var post = context.Posts.FirstOrDefault(p => p.Id == id);
            if (post != null)
            {
                context.Posts.Remove(post);
                await context.Save();
            }
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await context.Posts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await context.Posts.ToListAsync();
        }

        public async Task<Post> UpdateAsync(Post post)
        {
            context.Posts.Update(post);
            await context.Save();
            return post;
        }

        public async Task<List<Post>> GetPostsByTopicIdAsync(int topicId)
        {
            return await context.Posts.Where(post => post.TopicId == topicId).ToListAsync();
        }
    }
}

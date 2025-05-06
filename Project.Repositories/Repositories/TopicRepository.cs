using Microsoft.EntityFrameworkCore;
using Project.Repositories.Entities;
using Project.Repositories.Interfaces;

namespace Project.Repositories.Repositories
{
    public class TopicRepository : IForumRepository<Topic>
    {
        private readonly IContext context;

        public TopicRepository(IContext context)
        {
            Console.WriteLine("הגעתי לרפוזיטורי של טופיק");
            this.context = context;
        }

        public async Task<Topic> AddAsync(Topic topic)
        {
            await context.Topics.AddAsync(topic);
            await context.Save();
            return topic;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var topic = await context.Topics.FindAsync(id);
            if (topic != null)
            {
                context.Topics.Remove(topic);
                await context.Save();
            }
        }

        public async Task<Topic?> GetByIdAsync(int id)
        {
            return await context.Topics.Include(t => t.Posts)
                                       .Include(t => t.Counselors)
                                       .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Topic>> GetAllAsync()
        {
            Console.WriteLine("הגעתי לרפוזיטורי של טופיק לפונקציה גטאול");
            return await context.Topics
                .Include(t => t.Posts)
                .Include(t => t.Counselors)
                .ToListAsync();
        }

        public async Task<Topic> UpdateAsync(Topic topic)
        {
            context.Topics.Update(topic);
            await context.Save();
            return topic;
        }
    }
}

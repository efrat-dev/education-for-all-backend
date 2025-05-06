using AutoMapper;
using Common.Dto;
using Project.Repositories.Entities;
using Project.Repositories.Interfaces;
using Project.Services.Interfaces;

namespace Project.Services.Services
{
    public class TopicService : IForumService<TopicDto>
    {
        private readonly IForumRepository<Topic> topicRepository;
        private readonly IMapper mapper;

        public TopicService(IForumRepository<Topic> topicRepository, IMapper mapper)
        {
            Console.WriteLine("הגעתי לסרוויס של טופיק");
            this.topicRepository = topicRepository;
            this.mapper = mapper;
        }

        public async Task<TopicDto?> AddAsync(TopicDto entity)
        {
            var topic = await topicRepository.AddAsync(mapper.Map<Topic>(entity));
            return topic != null ? mapper.Map<TopicDto>(topic) : null;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await topicRepository.DeleteByIdAsync(id);
        }

        public async Task<List<TopicDto>> GetAllAsync()
        {
            Console.WriteLine(" הגעתי לסרוויס של טופיק לפונקציה גטאול");
            var topics = await topicRepository.GetAllAsync();
            return mapper.Map<List<TopicDto>>(topics);
        }

        public async Task<TopicDto> GetByIdAsync(int id)
        {
            return await mapper.Map<Task<TopicDto>>(topicRepository.GetByIdAsync(id));
        }

        public async Task<TopicDto> UpdateAsync(TopicDto item)
        {
            await topicRepository.UpdateAsync(mapper.Map<Topic>(item));
            return item;
        }
    }
}


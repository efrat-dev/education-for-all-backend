using Common.Dto;

namespace Project.Services.Interfaces
{
    public interface ITopicReadingService
    {
        Task ReadTopicAsync(TopicDto topic);
    }
}

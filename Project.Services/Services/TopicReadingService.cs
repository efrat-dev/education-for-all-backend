using Microsoft.CognitiveServices.Speech;
using Common.Dto;
using Project.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.CognitiveServices.Speech.Audio;

namespace Project.Services.Services
{
    public class TopicReadingService : ITopicReadingService
    {
        private readonly IPostService _postService;
        private readonly SpeechConfig _speechConfig;
        private readonly SpeechSynthesizer _speechSynthesizer;
        private readonly IConfiguration configuration;

        public TopicReadingService(IPostService postService, IConfiguration configuration)
        {
            _postService = postService;
            this.configuration = configuration;

            string? azureKey = configuration["AzureSpeech:Key"];
            string? azureRegion = configuration["AzureSpeech:Region"];

            if (string.IsNullOrWhiteSpace(azureKey) || string.IsNullOrWhiteSpace(azureRegion))
                throw new InvalidOperationException("Azure Speech configuration is missing.");

            _speechConfig = SpeechConfig.FromSubscription(azureKey, azureRegion);

            // Use AudioConfig with stream output to avoid relying on local audio system — required for environments like Render that lack audio playback support
            //On Render cloud, don't use it:
            //_speechSynthesizer = new SpeechSynthesizer(_speechConfig);
            //Use it!
            var audioConfig = AudioConfig.FromStreamOutput(new PullAudioOutputStream());
        }


        public async Task ReadTopicAsync(TopicDto topic)
        {
            var posts = await _postService.GetPostsByTopicIdAsync(topic.Id);
            if (posts == null || !posts.Any())
                return;
           
            await SpeakAsync($"כותרת השאלה היא: {topic.Title}");

            var orderedPosts = posts.OrderBy(p => p.Date).ToList();

            await SpeakAsync($"השאלה של פותח האשכול היא: {orderedPosts[0].Content}");

            for (int i = 1; i < orderedPosts.Count; i++)
            {
                await SpeakAsync("תגובה חדשה:");
                await SpeakAsync(orderedPosts[i].Content);
            }
            await SpeakAsync("קריאת האשכול הסתיימה");
            await SpeakAsync("להוספת תגובה אנא לחצו על הכפתור שבתחתית הדף.");
        }

        private async Task SpeakAsync(string text)
        {
            using var result = await _speechSynthesizer.SpeakTextAsync(text);
            if (result.Reason != ResultReason.SynthesizingAudioCompleted)
                return;
        }
    }
}
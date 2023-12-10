using OpenAI_API.Completions;
using OpenAI_API;
using webAPI.DTOs.Response;
using webAPI.Interfaces;

namespace webAPI.Services
{
    public class GPTService : IGPTService
    {
        private readonly IConfiguration _configuration;

        public GPTService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public async Task<GPTResponse> Ask(string prompt)
        {
            var apiKey = _configuration.GetSection("Appsettings:GChatAPIKEY").Value;
            var apiModel = _configuration.GetSection("Appsettings:Model").Value;

            var api = new OpenAIAPI(apiKey);

            var completionRequest = new CompletionRequest()
            {
                Prompt = prompt,
                Model = apiModel,
                Temperature = 0.5,
                MaxTokens = 100,
                TopP = 1.0,
                FrequencyPenalty = 0.0,
                PresencePenalty = 0.0,
            };

            var result = await api.Completions.CreateCompletionsAsync(completionRequest);

            Random rng = new Random();
            var randomResult = rng.Next(0, result.Completions.Count);

            var choice = result.Completions[randomResult];

            return new GPTResponse
            {
                Text = choice.Text.Trim(),
            };
        }
    }
}

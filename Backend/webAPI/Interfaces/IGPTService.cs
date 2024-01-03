using webAPI.DTOs.Response;

namespace webAPI.Interfaces
{
    public interface IGPTService
    {
        public Task<GPTResponse> Ask(string prompt);
    }
}

using Practice.ApiKeyAuthentication.Services;
using Practice.ApiKeyAuthentication.Ultils;

namespace Practice.ApiKeyAuthentication.Services
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    }

    public class ApiKeyValidation : IApiKeyValidation
    {
        private readonly IConfiguration _configuration;

        public ApiKeyValidation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValidApiKey(string userApiKey)
        {
            var apiKey = _configuration.GetValue<string>(Constants.ApiKeyName);

            return !string.IsNullOrWhiteSpace(userApiKey) && apiKey == userApiKey;
        }
    }

}

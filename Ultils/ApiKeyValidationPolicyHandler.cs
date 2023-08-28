using Microsoft.AspNetCore.Authorization;
using Practice.ApiKeyAuthentication.Services;

namespace Practice.ApiKeyAuthentication.Ultils
{
    public class ApiKeyRequirement : IAuthorizationRequirement
    {
    }

    public class ApiKeyValidationPolicyHandler : AuthorizationHandler<ApiKeyRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApiKeyValidation _apiKeyValidation;

        public ApiKeyValidationPolicyHandler(IHttpContextAccessor httpContextAccessor, IApiKeyValidation apiKeyValidation)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiKeyValidation = apiKeyValidation;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {
            var apiKey = _httpContextAccessor?.HttpContext?.Request.Headers[Constants.ApiKeyHeaderName].ToString();
            if (string.IsNullOrWhiteSpace(apiKey) || !_apiKeyValidation.IsValidApiKey(apiKey))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}

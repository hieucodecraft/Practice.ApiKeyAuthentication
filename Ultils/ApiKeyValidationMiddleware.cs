using System.Net;
using Practice.ApiKeyAuthentication.Services;
using Practice.ApiKeyAuthentication.Ultils;

namespace Practice.ApiKeyAuthentication.Ultils
{
    public class ApiKeyValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiKeyValidation _apiKeyValidation;

        public ApiKeyValidationMiddleware(RequestDelegate next, IApiKeyValidation apiKeyValidation)
        {
            _next = next;
            _apiKeyValidation = apiKeyValidation;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userApiKey = context.Request.Headers[Constants.ApiKeyHeaderName];

            if (string.IsNullOrWhiteSpace(userApiKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            if (!_apiKeyValidation.IsValidApiKey(userApiKey!))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
            await _next(context);
        }
    }
}

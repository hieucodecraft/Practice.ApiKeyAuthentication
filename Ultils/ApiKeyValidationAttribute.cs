using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Practice.ApiKeyAuthentication.Services;
using Practice.ApiKeyAuthentication.Ultils;

namespace Practice.ApiKeyAuthentication.Ultils
{
    public class ApiKeyValidationAttribute : ServiceFilterAttribute
    {
        public ApiKeyValidationAttribute() : base(typeof(ApiKeyAuthFilter))
        {
        }
    }

    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IApiKeyValidation _apiKeyValidation;

        public ApiKeyAuthFilter(IApiKeyValidation apiKeyValidation)
        {
            _apiKeyValidation = apiKeyValidation;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var requestApiKey = context.HttpContext.Request.Headers[Constants.ApiKeyHeaderName].ToString();
            if (string.IsNullOrWhiteSpace(requestApiKey))
            {
                context.Result = new BadRequestResult();
                return;
            }

            if (!_apiKeyValidation.IsValidApiKey(requestApiKey))
                context.Result = new UnauthorizedResult();
        }
    }
}

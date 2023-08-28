using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Practice.ApiKeyAuthentication.Services;
using Practice.ApiKeyAuthentication.Ultils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var environmentName = builder.Environment.EnvironmentName;
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{environmentName}.json", true, true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpContextAccessor();

// Register services
builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
builder.Services.AddScoped<ApiKeyAuthFilter>();
builder.Services.AddScoped<IAuthorizationHandler, ApiKeyValidationPolicyHandler>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiKeyPolicy", policy =>
    {
        policy.AddAuthenticationSchemes(new[] { JwtBearerDefaults.AuthenticationScheme });
        policy.Requirements.Add(new ApiKeyRequirement());
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

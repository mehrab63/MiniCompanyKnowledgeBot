using MiniCompanyKnowledgeBot.Models.Dtos;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace MiniCompanyKnowledgeBot.Middleware
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandler> _logger; 

        public CustomExceptionHandler(RequestDelegate next, ILogger<CustomExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); 
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    _logger.LogWarning($"Unauthorized Access - Path: {httpContext.Request.Path}, Method: {httpContext.Request.Method}, IP: {httpContext.Connection.RemoteIpAddress}");
                }
                else if (httpContext.Response.StatusCode != (int)HttpStatusCode.OK)
                {
                    _logger.LogWarning($"Unexpected response status code: {httpContext.Response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"خطای داخلی در سرور - Path: {httpContext.Request.Path}");

                if (ex is UnauthorizedAccessException || ex.Source == "Microsoft.AspNetCore.Authorization")
                {
                    _logger.LogWarning($"Unauthorized Exception - Message: {ex.Message}");
                    await GetResponse(httpContext, 401);
                }
                else
                {
                    await GetResponse(httpContext);
                }
            }
        }

        private async Task GetResponse(HttpContext httpContext, int statusCode = 500)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            var errorMessage = statusCode == 401 ? "دسترسی غیرمجاز" : "خطای داخلی در سرور";
            var result = new ResultDto<bool>(false, new List<string>() { errorMessage }, false);
            var jsonResponse = JsonConvert.SerializeObject(result);

            await httpContext.Response.WriteAsync(jsonResponse);
        }
    }

    public static class CustomExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
    }
}

using System.Net;
using System.Text.Json;
using FranchisApi.Models;

namespace FranchisApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                await HandleExceptionAsync(context, ex, context.RequestServices.GetService<IWebHostEnvironment>());
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IWebHostEnvironment? env)
        {
            var code = HttpStatusCode.InternalServerError;
            var errorResponse = new ErrorResponse
            {
                Error = "An unexpected error occurred."
            };

            if (env != null && env.IsDevelopment())
            {
                errorResponse.Detail = exception.ToString();
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            
            var result = JsonSerializer.Serialize(errorResponse);
            
            return context.Response.WriteAsync(result);
        }
    }
}

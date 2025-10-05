using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace Ecommerce.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate ?_next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (_next is not null)
                {
                    await _next(context);

                }
                if(context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                    await HandleNotFoundApiAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleNotFoundApiAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = $"The end point {context.Request.Path} not found"
            }.ToString();
            await context.Response.WriteAsync(response);

        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };
            var response = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
            }.ToString();
            await context.Response.WriteAsync(response);
        }
     }
}

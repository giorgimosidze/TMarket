using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TMarket.WEB.RequestModels;
using TMarket.WEB.RequestModels.Errors;

namespace TMarket.WEB.Helpers.CustomMiddlewares
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorLoggingMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger<ErrorLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"მოხდა შეცდომა: {e}");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = "Internal Error"
                }.ToString());
            }
        }
    }
}

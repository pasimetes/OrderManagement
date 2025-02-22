using OrderManagement.Domain.Exceptions;
using Serilog.Context;
using System.Net;

namespace OrderManagement.WebApi.Middleware
{
    public class ErrorHandlingMiddleware(RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        IWebHostEnvironment environment)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers["x-correlation-id"].FirstOrDefault() ?? Guid.NewGuid().ToString();

            using (LogContext.PushProperty("correlationId", correlationId))
            {
                try
                {
                    await next(context);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);

                    var statusCode = ex switch
                    {
                        NotFoundException => HttpStatusCode.NotFound,
                        BusinessException => HttpStatusCode.InternalServerError,

                        _ => HttpStatusCode.InternalServerError
                    };

                    var response = new
                    {
                        error = statusCode == HttpStatusCode.InternalServerError && !environment.IsDevelopment()
                            ? "Something went wrong."
                            : ex.Message
                    };

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)statusCode;
                    await context.Response.WriteAsJsonAsync(response);
                }
            }
        }
    }
}

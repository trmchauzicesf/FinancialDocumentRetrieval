using FinancialDocumentRetrieval.Models.Common.Errors;
using FinancialDocumentRetrieval.Models.Common.Exceptions;
using FinancialDocumentRetrieval.Models.Common.Response;
using System.Net;
using System.Text.Json;

namespace FinancialDocumentRetrieval.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env
        )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        //ova metoda mora da se zove bas ovako da bi frameworke znao da je pozove u middleware
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            switch (ex)
            {
                case FinancialDocumentRetrievalException e:
                    // custom application error
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new ApiResponse
            {
                Data = new ApiException
                {
                    Message = ex.Message,
                    Details = _env.IsDevelopment() ? ex.StackTrace?.ToString() : "Internal server error"
                },
                Successful = false,
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}

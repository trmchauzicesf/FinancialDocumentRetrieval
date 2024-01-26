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
        private const string ResponseContentType = "application/json";

        public ExceptionMiddleware(
            RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env
        )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

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
            context.Response.ContentType = ResponseContentType;

            switch (ex)
            {
                case FinancialDocumentRetrievalException e:
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;
                case KeyNotFoundException e:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new ApiResponse
            {
                Data = new ApiException
                {
                    Message = ex.Message,
                    Details = _env.IsDevelopment() ? ex.StackTrace : "Internal server error"
                },
                Successful = false,
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}
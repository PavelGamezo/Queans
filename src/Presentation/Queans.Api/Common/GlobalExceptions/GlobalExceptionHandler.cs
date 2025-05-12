
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Npgsql.Internal;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Queans.Api.Common.GlobalExceptions
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private const string UnhandledExceptionMessage = "An unhandled exception has occurred while executing the request.";

        private static readonly JsonSerializerOptions serializerOptions = new(JsonSerializerDefaults.Web)
        {
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception) when (context.RequestAborted.IsCancellationRequested)
            {
                const string message = "The request was aboted by the client.";
                _logger.LogDebug(exception, message);

                context.Response.Clear();
                context.Response.StatusCode = 499;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, UnhandledExceptionMessage);

                const string contentType = "application/problem+json";
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = contentType;

                var problemDetails = CreateProblemDetails(context, exception);
                var json = ToJson(problemDetails);
                await context.Response.WriteAsync(json);
            }
        }

        private ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
        {
            var statusCode = context.Response.StatusCode;
            var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);
            if (string.IsNullOrEmpty(reasonPhrase))
            {
                reasonPhrase = UnhandledExceptionMessage;
            }

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = reasonPhrase,
            };

            problemDetails.Detail = exception.Message;
            problemDetails.Extensions["traceId"] = context.TraceIdentifier;
            problemDetails.Extensions["data"] = exception.Data;

            return problemDetails;
        }

        private string ToJson(in ProblemDetails problemDetails)
        {
            try
            {
                return JsonSerializer.Serialize(problemDetails, serializerOptions);
            }
            catch (Exception ex)
            {
                const string msg = "An exception has occurred while serializing error to JSON.";
                _logger.LogError(ex, msg);
            }

            return string.Empty;
        }
    }
}

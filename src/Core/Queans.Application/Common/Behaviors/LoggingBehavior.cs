using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Queans.Application.Common.CQRS.Commands;
using System.Windows.Input;

namespace Queans.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
        where TResponse : IErrorOr
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Starting request {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                DateTime.UtcNow);

            var result = await next();

            if (result.IsError)
            {
                logger.LogInformation(
                    "Failure request with error {@RequestName}, {@Error}, {@DateTimeUtc}",
                    typeof(TRequest).Name,
                    result.Errors,
                    DateTime.UtcNow);

                return result;
            }

            logger.LogInformation(
                "Completed request {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                DateTime.UtcNow);

            return result;
        }
    }
}

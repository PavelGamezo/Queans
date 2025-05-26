using MediatR;
using Microsoft.Extensions.Logging;
using Queans.Domain.Questions.Events;

namespace Queans.Application.Questions.Events
{
    public class QuestionCreatedNotificationHandler(ILogger<QuestionCreatedNotificationHandler>  logger)
        : INotificationHandler<QuestionCreatedEvent>
    {
        public Task Handle(QuestionCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Question {questionId} by {AuthorId} was added to database",
                notification.Question.Id,
                notification.Question.Author.Id);

            return Task.CompletedTask;
        }
    }
}

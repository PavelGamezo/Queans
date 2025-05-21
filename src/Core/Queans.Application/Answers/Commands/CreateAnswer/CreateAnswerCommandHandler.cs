using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;
using Queans.Domain.Questions.Entities;

namespace Queans.Application.Answers.Commands.CreateAnswer
{
    public class CreateAnswerCommandHandler : ICommandHandler<CreateAnswerCommand, ErrorOr<Success>>
    {
        private readonly IQuestionRepository _questionRepository;

        public CreateAnswerCommandHandler(
            IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<ErrorOr<Success>> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var (text, questionId, authorId) = request;
            var initialRating = 0;
            var question = await _questionRepository.GetQuestionByIdAsync(questionId, cancellationToken);
            if (question is null)
            {
                return ApplicationErrors.NotFoundQuestionError;
            }

            var author = question.Author;
            if (author.Id != authorId)
            {
                return ApplicationErrors.NotFoundUser;
            }

            var answerCreationResult = Answer.CreateAnswer(text, author, question, initialRating);
            if (answerCreationResult.IsError)
            {
                return answerCreationResult.Errors;
            }

            question.AddAnswer(answerCreationResult.Value);

            await _questionRepository.SaveAsync(cancellationToken);

            return Result.Success;
        }
    }
}

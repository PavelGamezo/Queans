using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;
using Queans.Domain.Questions;

namespace Queans.Application.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandHandler : ICommandHandler<CreateQuestionCommand, ErrorOr<Success>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;

        public CreateQuestionCommandHandler(
            IQuestionRepository questionRepository,
            IUserRepository userRepository)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<Success>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var (title, description, authorId) = request;

            var author = await _userRepository.GetUserByIdAsync(authorId, cancellationToken);
            if (author is null)
            {
                return ApplicationErrors.NotFoundUser;
            }

            var question = Question.CreateQuestion(0, author, title, description, DateTime.UtcNow);
            if (question.IsError)
            {
                return question.Errors;
            }

            return Result.Success;
        }
    }
}

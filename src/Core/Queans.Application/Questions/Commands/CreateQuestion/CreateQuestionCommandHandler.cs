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
        private readonly ITagRepository _tagRepository;

        public CreateQuestionCommandHandler(
            IQuestionRepository questionRepository,
            IUserRepository userRepository,
            ITagRepository tagRepository)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
        }

        public async Task<ErrorOr<Success>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var (title, description, authorId, tags) = request;

            var author = await _userRepository.GetUserByIdAsync(authorId, cancellationToken);
            if (author is null)
            {
                return ApplicationErrors.NotFoundUser;
            }

            var existingTags = await _tagRepository.GetExistingTags(tags, cancellationToken);
            var missingTags = tags.Except(
                    existingTags.Select(
                        tag => tag.Name))
                    .ToList();

            if (missingTags.Any())
            {
                return ApplicationErrors.NotFoundTagError;
            }

            var questionFactoryResult = Question.CreateQuestion(0, author, title, description, existingTags, DateTime.UtcNow);
            if (questionFactoryResult.IsError)
            {
                var errors = questionFactoryResult.Errors;
                return errors;
            }

            var question = questionFactoryResult.Value;

            _questionRepository.Add(question);

            await _questionRepository.SaveAsync(cancellationToken);

            return Result.Success;
        }
    }
}

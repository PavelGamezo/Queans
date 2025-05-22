using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;

namespace Queans.Application.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandHandler : ICommandHandler<UpdateQuestionCommand, ErrorOr<Success>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ITagRepository _tagRepository;

        public UpdateQuestionCommandHandler(
            IQuestionRepository questionRepository,
            ITagRepository tagRepository)
        {
            _questionRepository = questionRepository;
            _tagRepository = tagRepository;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var (id, title, description, tags) = request;

            var question = await _questionRepository.GetQuestionByIdAsync(id, cancellationToken);
            if (question is null)
            {
                return ApplicationErrors.NotFoundQuestionError;
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

            question.UpdateQuestion(title, description, existingTags);

            return Result.Success;
        }
    }
}

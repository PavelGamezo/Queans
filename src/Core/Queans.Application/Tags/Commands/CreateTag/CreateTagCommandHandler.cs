using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;
using Queans.Domain.Questions.Entities;

namespace Queans.Application.Tags.Commands.CreateTag
{
    public class CreateTagCommandHandler : ICommandHandler<CreateTagCommand, ErrorOr<Success>>
    {
        private readonly ITagRepository _tagRepository;

        public CreateTagCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<ErrorOr<Success>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var tagName = request.Name;
            if (await _tagRepository.IsExistByNameAsync(tagName, cancellationToken))
            {
                return ApplicationErrors.TagExistError;
            }

            var tagFactoryResult = Tag.CreateTag(tagName);
            if (tagFactoryResult.IsError)
            {
                return tagFactoryResult.Errors;
            }

            var tag = tagFactoryResult.Value;
            _tagRepository.AddTag(tag);
            await _tagRepository.SaveAsync(cancellationToken);

            return Result.Success;
        }
    }
}

using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;

namespace Queans.Application.Tags.Commands.UpdateTag
{
    public class UpdateTagCommandHandler : ICommandHandler<UpdateTagCommand, ErrorOr<Success>>
    {
        private readonly ITagRepository _tagRepository;

        public UpdateTagCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var (tagId, name) = request;

            var tag = await _tagRepository.GetTagByIdAsync(tagId, cancellationToken);
            if (tag is null)
            {
                return ApplicationErrors.NotFoundTagError;
            }

            tag.UpdateTag(name);

            await _tagRepository.SaveAsync(cancellationToken);

            return Result.Success;
        }
    }
}

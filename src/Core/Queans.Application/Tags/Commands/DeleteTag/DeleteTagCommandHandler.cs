using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;

namespace Queans.Application.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandHandler : ICommandHandler<DeleteTagCommand, ErrorOr<Success>>
    {
        private readonly ITagRepository _tagRepository;

        public DeleteTagCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<ErrorOr<Success>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var tagId = request.Id;

            var tag = await _tagRepository.GetTagByIdAsync(tagId, cancellationToken);
            if (tag is null)
            {
                return ApplicationErrors.NotFoundTagError;
            }

            _tagRepository.DeleteTag(tag);
            await _tagRepository.SaveAsync(cancellationToken);

            return Result.Success;
        }
    }
}

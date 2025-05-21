using Queans.Domain.Questions;
using Queans.Domain.Questions.Entities;

namespace Queans.Application.Common.Persistence
{
    public interface ITagRepository
    {
        Task<Tag> GetTagByIdAsync(Guid id, CancellationToken cancellationToken);

        void AddTag(Tag tag);

        void UpdateTag(Tag tag);

        void DeleteTag(Tag tag);

        Task SaveAsync(CancellationToken cancellationToken);
    }
}

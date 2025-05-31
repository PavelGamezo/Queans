using Queans.Domain.Votes.Entities;
using Queans.Domain.Votes.Enums;

namespace Queans.Application.Common.Persistence
{
    public interface IVoteRepository
    {
        Task<bool> IsVoteExistAsync(
            Guid userId,
            Guid targetId,
            VoteTargetType targetType,
            CancellationToken cancellationToken);

        void AddVote(Vote vote);

        void RemoveVote(Vote vote);

        void UpdateVote(Vote vote);

        Task<Vote?> GetVoteByIdAsync(Guid voteId, CancellationToken cancellationToken);

        Task SaveAsync(CancellationToken cancellationToken);
    }
}

using Microsoft.EntityFrameworkCore;
using Queans.Application.Common.Persistence;
using Queans.Domain.Votes.Entities;
using Queans.Domain.Votes.Enums;
using Queans.Infrastructure.Persistence.Contexts;

namespace Queans.Infrastructure.Persistence.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly QueansDbContext _dbContext;

        public VoteRepository(QueansDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddVote(Vote vote)
        {
            _dbContext.Votes.Add(vote);
        }

        public async Task<Vote?> GetVoteByIdAsync(Guid voteId, CancellationToken cancellationToken)
        {
            return await _dbContext.Votes
                .Include(vote => vote.User)
                .SingleOrDefaultAsync(vote => vote.Id == voteId);
        }

        public async Task<bool> IsVoteExistAsync(
            Guid userId,
            Guid targetId,
            VoteTargetType targetType,
            CancellationToken cancellationToken)
        {
            var vote = await _dbContext.Votes
                .Include(vote => vote.User)
                .SingleOrDefaultAsync(
                    vote => vote.UserId == userId && vote.TargetType == targetType && vote.TargetId == targetId,
                    cancellationToken: cancellationToken);

            if (vote is null)
            {
                return false;
            }

            return true;
        }

        public void RemoveVote(Vote vote)
        {
            _dbContext?.Votes.Remove(vote);
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync();
        }

        public void UpdateVote(Vote vote)
        {
            _dbContext.Votes.Update(vote);
        }
    }
}

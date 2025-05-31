using ErrorOr;
using Queans.Domain.Common;
using Queans.Domain.Users;
using Queans.Domain.Users.Errors;
using Queans.Domain.Votes.Enums;
using Queans.Domain.Votes.Errors;

namespace Queans.Domain.Votes.Entities
{
    public class Vote : Entity<Guid>
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public Guid TargetId { get; private set; }

        public VoteTargetType TargetType { get; private set; }

        public VoteType VoteType { get; private set; }

        private Vote(Guid id) : base(id) { }  

        private Vote(
            Guid id,
            User user,
            Guid targetId,
            VoteTargetType targetType,
            VoteType voteType) : base(id)
        {
            UserId = user.Id;
            User = user;
            TargetId = targetId;
            TargetType = targetType;
            VoteType = voteType;
        }

        public static ErrorOr<Vote> CreateVote(
            User user,
            Guid targetId,
            VoteTargetType targetType,
            VoteType voteType)
        {
            if (user is null)
            {
                return UserDomainErrors.NotFoundUserError;
            }

            if (targetId == Guid.Empty)
            {
                return VoteDomainErrors.InvalidTargetIdentifierError;
            }

            var id = Guid.NewGuid();

            return new Vote(
                id,
                user,
                targetId,
                targetType,
                voteType);
        }
    }
}

using Queans.Domain.Common;
using Queans.Domain.Questions;
using Queans.Domain.Questions.Entities;
using Queans.Domain.Users;
using Queans.Domain.Voting.Enums;

namespace Queans.Domain.Voting.Entities
{
    public class Vote : Entity<Guid>
    {
        public Guid Id { get; init; }

        public User? User { get; private set; }

        public Question? Question { get; private set; }

        public Answer? Answer { get; private set; }

        public VoteType Type { get; init; }

        public Vote(Guid id) : base(id)
        {
        }
    }
}

using Queans.Domain.Common;
using Queans.Domain.Questions;
using Queans.Domain.Questions.Entities;
using Queans.Domain.Users;
using Queans.Domain.Votes.Enums;

namespace Queans.Domain.Votes.Entities
{
    public class Vote : Entity<Guid>
    {
        public User User { get; private set; }

        public Question Question { get; private set; }

        public Answer Answer { get; private set; }

        public VoteType VoteType { get; init; }

        public Vote(Guid id) : base(id)
        {
        }
    }
}

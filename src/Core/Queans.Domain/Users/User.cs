using ErrorOr;
using Queans.Domain.Common;
using Queans.Domain.Questions;
using Queans.Domain.Questions.Entities;
using Queans.Domain.Users.Entities;
using Queans.Domain.Users.Events;
using Queans.Domain.Users.ValueObjects;
using Queans.Domain.Votes.Entities;
using Queans.Domain.Votes.Enums;

namespace Queans.Domain.Users
{
    public class User : AggregateRoot<Guid>
    {
        private readonly List<Role> _roles = new();
        public IReadOnlyCollection<Role> Roles => _roles;

        private readonly List<Question>? _question = new();
        public IReadOnlyCollection<Question>? Questions => _question;

        private readonly List<Answer>? _answers = new();
        public IReadOnlyCollection<Answer>? Answers => _answers;

        private readonly List<Vote>? _votes = new();
        public IReadOnlyCollection<Vote>? Votes => _votes;

        public string UserName { get; private set; } = string.Empty;

        public Email UserEmail { get; private set; } = string.Empty;

        public string PasswordHash { get; private set; } = string.Empty;

        public Rating Rating { get; private set; } = 0;


        protected User(
            Guid id,
            string userName,
            Email userEmail,
            string passwordHash,
            Rating rating) 
            : base(id)
        {
            UserName = userName;
            UserEmail = userEmail;
            PasswordHash = passwordHash;
            Rating = rating;
        }

        public static ErrorOr<User> Create(string userName, string userEmail, string passwordHash, int rating)
        {
            var identifier = Guid.NewGuid();
            
            var emailResult = Email.Create(userEmail);
            if (emailResult.IsError)
            {
                return emailResult.Errors;
            }

            var ratingResult = Rating.Create(rating);
            if (ratingResult.IsError)
            {
                return ratingResult.Errors;
            }

            var user = new User(identifier, userName, emailResult.Value, passwordHash, ratingResult.Value);

            user.AddDomainEvent(new UserRegisteredEvent(user));

            return user;
        }

        public void RegisterUserRole(Role role)
        {
            _roles.Add(role);

            AddDomainEvent(new UserRoleCreatedEvent(Id, role.Id));
        }

        public void ChangeRating(VoteType voteType)
        {
            if ((int)voteType == 1)
            {
                Rating.UpvoteRating();
            }
            else
            {
                Rating.DownvoteRating();
            }
        }
    }
}

using ErrorOr;
using Queans.Domain.Common;
using Queans.Domain.Users.Events;
using Queans.Domain.Users.ValueObjects;

namespace Queans.Domain.Users
{
    public class User : AggregateRoot<Guid>
    {
        public string UserName { get; private set; } = string.Empty; // ValueObject

        public Email UserEmail { get; private set; } = string.Empty;

        public string PasswordHash { get; private set; } = string.Empty;

        public Rating Rating { get; private set; } = 0;


        public User(
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

        private User(Guid userId) : base(userId) { }

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

            var user =  new User(identifier, userName, emailResult.Value, passwordHash, ratingResult.Value);

            user.AddDomainEvent(new UserRegisteredEvent(user));

            return user;
        }
    }
}

using ErrorOr;
using Queans.Domain.Common;
using Queans.Domain.Questions.ValueObjects;
using Queans.Domain.Users;
using Queans.Domain.Users.ValueObjects;

namespace Queans.Domain.Questions
{
    public class Question : AggregateRoot<Guid>
    {
        //private readonly List<Tag> _tags = new();

        //private readonly List<Answer> _answers = new();

        //public IReadOnlyCollection<Tag> Tags { get; private set; } => _tags;

        //public IReadOnlyCollection<Answer> Answers { get; private set; } => _answers;

        public Rating Rating { get; private set; } = 0;

        public User Author { get; private set; } = null!;

        public Title Title { get; private set; } = string.Empty;

        public string Description { get; private set; } = String.Empty;

        public DateTime DateOfCreation { get; private set; } = default!;

        public DateTime DateOfUpdate { get; private set; } = default!;

        public Question(
            Guid id,
            int rating,
            User author,
            string title,
            string description,
            DateTime dateOfCreation)
            : base(id)
        {
            Rating = rating;
            Author = author;
            Title = title;
            Description = description;
            DateOfCreation = dateOfCreation;
            DateOfUpdate = dateOfCreation;
        }

        public ErrorOr<Question> CreateQuestion(
            int rating,
            User user,
            string title,
            string description,
            DateTime dateOfCreation)
        {
            var identifier = Guid.NewGuid();

            var titleResult = Title.Create(title);
            if (titleResult.IsError)
            {
                return titleResult.Errors;
            }

            var descriptionResult = ValueObjects.Description.CreateDescription(description);
            if (descriptionResult.IsError)
            {
                return descriptionResult.Errors;
            }

            return new Question(
                identifier,
                rating,
                user,
                titleResult.Value,
                descriptionResult.Value,
                dateOfCreation);
        }
    }
}

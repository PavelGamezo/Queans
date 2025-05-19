using ErrorOr;
using Queans.Domain.Common;
using Queans.Domain.Questions.Entities;
using Queans.Domain.Questions.Events;
using Queans.Domain.Questions.ValueObjects;
using Queans.Domain.Users;
using Queans.Domain.Users.ValueObjects;

namespace Queans.Domain.Questions
{
    public class Question : AggregateRoot<Guid>
    {
        //private readonly List<Answer> _answers = [];

        private readonly List<Tag>? _tags = new();
        public IReadOnlyCollection<Tag>? Tags => _tags;

        //public IReadOnlyCollection<Answer> Answers => _answers;

        public Rating Rating { get; private set; } = 0;

        public User Author { get; private set; } = null!;

        public Title Title { get; private set; } = string.Empty;

        public Description Description { get; private set; } = string.Empty;

        public DateTime DateOfCreation { get; private set; } = default;

        public DateTime DateOfUpdate { get; private set; } = default;

        private Question(Guid id) : base(id) { }

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

        public void AddQuestionTag(Tag tag)
        {
            _tags.Add(tag);

            AddDomainEvent(new QuestionTagAddedEvent(tag));
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

            var question = new Question(
                identifier,
                rating,
                user,
                titleResult.Value,
                descriptionResult.Value,
                dateOfCreation);

            AddDomainEvent(new QuestionCreatedEvent(question));

            return question;
        }
    }
}

using ErrorOr;
using Queans.Domain.Common;
using Queans.Domain.Questions.Entities;
using Queans.Domain.Questions.Errors;
using Queans.Domain.Questions.Events;
using Queans.Domain.Questions.ValueObjects;
using Queans.Domain.Users;
using Queans.Domain.Users.ValueObjects;
using Queans.Domain.Votes.Enums;

namespace Queans.Domain.Questions
{
    public class Question : AggregateRoot<Guid>
    {

        private readonly List<Tag>? _tags = new();
        public IReadOnlyCollection<Tag>? Tags => _tags;

        private readonly List<Answer> _answers = new();
        public IReadOnlyCollection<Answer> Answers => _answers;

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

            AddDomainEvent(new TagAddedEvent(tag.Id, Id));
        }

        public void AddQuestionTagList(List<Tag> tags)
        {
            _tags.AddRange(tags);
        }

        public void AddAnswer(Answer answer)
        {
            _answers.Add(answer);

            AddDomainEvent(new AnswerAddedEvent(answer.Id, Id));
        }

        public void ApplyVote(VoteType voteType)
        {
            Rating += (int)voteType;
            
            Author.ChangeRating(voteType);

            DateOfUpdate = DateTime.UtcNow;
        }

        public ErrorOr<Success> UpdateQuestion(string title, string description, List<Tag> tags)
        {
            var descriptionCreatingResult = Description.CreateDescription(description);
            if (descriptionCreatingResult.IsError)
            {
                return descriptionCreatingResult.Errors;
            }

            var titleCreatingResult = Title.Create(title);
            if (titleCreatingResult.IsError)
            {
                return titleCreatingResult.Errors;
            }

            Description = descriptionCreatingResult.Value;
            Title = titleCreatingResult.Value;
            
            _tags.Clear();
            _tags.AddRange(tags);

            DateOfUpdate = DateTime.UtcNow;

            return Result.Success;
        }

        public static ErrorOr<Question> CreateQuestion(
            int rating,
            User user,
            string title,
            string description,
            List<Tag> tags,
            DateTime dateOfCreation)
        {
            var identifier = Guid.NewGuid();

            var titleResult = Title.Create(title);
            if (titleResult.IsError)
            {
                return titleResult.Errors;
            }

            var descriptionResult = Description.CreateDescription(description);
            if (descriptionResult.IsError)
            {
                return descriptionResult.Errors;
            }

            if (!tags.Any())
            {
                return QuestionDomainErrors.IncorrectTagNameInputError;
            }

            var question = new Question(
                identifier,
                rating,
                user,
                titleResult.Value,
                descriptionResult.Value,
                dateOfCreation);

            question.AddQuestionTagList(tags);

            question.AddDomainEvent(new QuestionCreatedEvent(question));

            return question;
        }
    }
}

using ErrorOr;
using Queans.Domain.Common;
using Queans.Domain.Questions.Errors;
using Queans.Domain.Questions.Events;
using Queans.Domain.Questions.ValueObjects;
using Queans.Domain.Users;
using Queans.Domain.Users.Errors;
using Queans.Domain.Users.ValueObjects;
using Queans.Domain.Votes.Enums;

namespace Queans.Domain.Questions.Entities
{
    public class Answer : Entity<Guid>
    {
        private Answer(Guid id) : base(id) { }

        public Answer(
            Guid id, 
            string text,
            User author,
            Question question,
            int rating,
            DateTime dateOfCreation) : base(id)
        {
            Text = text;
            Author = author;
            Question = question;
            Rating = rating;
            DateOfCreation = dateOfCreation;
            DateOfUpdate = dateOfCreation;
        }

        public AnswerText Text { get; private set; } = string.Empty;

        public User Author { get; private set; } = default!;

        public Question Question { get; private set; } = default!;

        public Rating Rating { get; private set; } = 0;

        public DateTime DateOfCreation { get; private set; } = default;

        public DateTime DateOfUpdate { get; private set; } = default;

        // TODO: 
        // Create normal business logic for upvote/downvote answer
        public void ChangeRating(int rating)
        {
            Rating += rating;
        }

        public ErrorOr<Success> ChangeText(string text)
        {
            var textResult = AnswerText.Create(text);
            if (textResult.IsError)
            {
                return textResult.Errors;
            }

            Text = textResult.Value;

            DateOfUpdate = DateTime.UtcNow;

            return Result.Success;
        }

        public static ErrorOr<Answer> CreateAnswer(string text, User author, Question question,int rating)
        {
            var textResult = AnswerText.Create(text);
            if (textResult.IsError)
            {
                return textResult.Errors;
            }

            if (author is null)
            {
                return UserDomainErrors.NotFoundUserError;
            }

            if (question is null)
            {
                return QuestionDomainErrors.NotFoundQuestionError;
            }

            if (rating < 0)
            {
                return UserDomainErrors.FailureRatingError;
            }

            var identifier = Guid.NewGuid();

            var answer = new Answer(
                identifier,
                textResult.Value,
                author,
                question,
                rating,
                DateTime.UtcNow);

            answer.AddDomainEvent(new AnswerCreatedEvent(answer.Id));

            return answer;
        }

        public void ApplyVote(VoteType voteType)
        {
            Rating += (int)voteType;

            Author.ChangeRating(voteType);

            DateOfUpdate = DateTime.UtcNow;
        }
    }
}

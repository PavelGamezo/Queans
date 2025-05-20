using ErrorOr;
using Queans.Domain.Common;
using Queans.Domain.Questions.Errors;

namespace Queans.Domain.Questions.ValueObjects
{
    public class AnswerText : ValueObject
    {
        public string Value { get; private set; }

        public AnswerText(string value) => Value = value;

        public static ErrorOr<AnswerText> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return QuestionDomainErrors.IncorrectAnswerTextInputError;
            }

            return new AnswerText(value);
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(AnswerText answer) =>
            answer.Value;

        public static implicit operator AnswerText(string answer) => 
            new AnswerText(answer);

        public override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}

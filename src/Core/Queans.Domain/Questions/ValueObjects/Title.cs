using ErrorOr;
using Queans.Domain.Common;
using Queans.Domain.Questions.Errors;

namespace Queans.Domain.Questions.ValueObjects
{
    public class Title : ValueObject
    {
        private const int MAX_LENGTH_VALUE = 1000;

        public string Value { get; init; }

        public Title(string value) => Value = value;

        public static ErrorOr<Title> Create(string value)
        {
            if (value.Length > MAX_LENGTH_VALUE)
            {
                return QuestionDomainErrors.IncorrectTitleInputError;
            }

            return new Title(value);
        }

        public override string ToString() => Value;

        public static implicit operator string(Title title) 
            => title.Value;

        public static implicit operator Title(string value)
            => new (value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}

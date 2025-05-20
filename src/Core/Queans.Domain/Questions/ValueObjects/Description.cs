using ErrorOr;
using Queans.Domain.Common;
using Queans.Domain.Questions.Errors;

namespace Queans.Domain.Questions.ValueObjects
{
    public class Description : ValueObject
    {
        public string Value { get; private set; }

        public Description(string value) => Value = value;

        public static ErrorOr<Description> CreateDescription(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return QuestionDomainErrors.IncorrectDescriptionInputError;
            }

            return new Description(value);
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(Description description) 
            => description.Value;

        public static implicit operator Description(string description)
            => new(description);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}

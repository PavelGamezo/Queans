using ErrorOr;
using System.Text.RegularExpressions;

namespace Queans.Domain.Users.ValueObjects
{
    public class Email : Common.ValueObject
    {
        private const int MAX_VALUE_LENGTH = 256;

        public string Value { get; init; }

        public Email(string value) => Value = value;

        public static ErrorOr<Email> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Errors.UserDomainErrors.IncorrectEmailError;
            }

            if (value.Length >= MAX_VALUE_LENGTH)
            {
                return Errors.UserDomainErrors.IncorrectEmailError;
            }

            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return Errors.UserDomainErrors.IncorrectEmailError;
            }

            return new Email(value);
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator Email(string email) =>
            new Email(email);

        public static implicit operator string(Email email) =>
            email.ToString();

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}

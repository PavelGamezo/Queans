using ErrorOr;

namespace Queans.Domain.Users.Errors
{
    public static class UserDomainErrors
    {
        public static readonly Error IncorrectEmailError = Error.Validation(
            code: "General.Failure",
            description: "Email format is invalid");

        public static readonly Error FailureRatingError = Error.Validation(
            code: "General.Failure",
            description: "Email format is invalid");
    }
}

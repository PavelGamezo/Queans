using ErrorOr;

namespace Queans.Domain.Users.Errors
{
    public static class UserDomainErrors
    {
        public static readonly Error IncorrectEmailError = Error.Failure(
            code: "General.Failure",
            description: "Email format is invalid");

        public static readonly Error FailureRatingError = Error.Failure(
            code: "General.Failure",
            description: "Email format is invalid");
    }
}

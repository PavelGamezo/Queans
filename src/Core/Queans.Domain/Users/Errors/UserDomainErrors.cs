using ErrorOr;

namespace Queans.Domain.Users.Errors
{
    public static class UserDomainErrors
    {
        public static readonly Error IncorrectEmailError = Error.Validation(
            code: "General.Validation",
            description: "Email format is invalid");

        public static readonly Error FailureRatingError = Error.Validation(
            code: "General.Validation",
            description: "Email format is invalid");

        public static readonly Error FailureRoleError = Error.Unexpected(
            code: "General.Validation",
            description: "Role must be not null");

        public static readonly Error NotFoundUserError = Error.NotFound(
            code: "General.NotFound",
            description: "User is not exist");
    }
}

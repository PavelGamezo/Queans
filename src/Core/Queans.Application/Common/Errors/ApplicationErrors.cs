using ErrorOr;

namespace Queans.Application.Common.Errors
{
    public static class ApplicationErrors
    {
        public static Error UserExistError = Error.Conflict(
            code: "Application.Conflict",
            description: "User has already exist");

        public static Error NotFoundUser = Error.NotFound(
            code: "Application.NotFound",
            description: "User with entered data is not exist");
        
        public static Error RoleNotFoundError = Error.NotFound(
            code: "Application.NotFound",
            description: "Internal error");
    }
}

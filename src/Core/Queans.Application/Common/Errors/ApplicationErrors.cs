using ErrorOr;

namespace Queans.Application.Common.Errors
{
    public static class ApplicationErrors
    {
        public static Error UserExistError = Error.Conflict(
            code: "Application.conflict",
            description: "User has already exist");
    }
}

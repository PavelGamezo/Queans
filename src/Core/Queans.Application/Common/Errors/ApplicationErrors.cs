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

        public static Error InvalidTagsCountError = Error.Validation(
            code: "Application.Validation",
            description: "Invalid count of tags");

        public static Error TagExistError = Error.Conflict(
            code: "Application.Conflict",
            description: "Tag is already exist");

        public static Error NotFoundQuestionError = Error.NotFound(
            code: "Application.NotFound",
            description: "Question is not exist");

        public static Error NotFoundAnswerError = Error.NotFound(
            code: "Application.NotFound",
            description: "Answer is not exist");
        
        public static Error NotFoundTagError = Error.NotFound(
            code: "Application.NotFound",
            description: "Tag is not exist");

        public static Error UserAlreadyVotedError = Error.Conflict(
            code: "Application.Conflict",
            description: "User already voted for this question");
    }
}

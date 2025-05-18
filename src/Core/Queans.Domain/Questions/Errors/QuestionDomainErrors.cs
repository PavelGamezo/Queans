using ErrorOr;

namespace Queans.Domain.Questions.Errors
{
    public static class QuestionDomainErrors
    {
        public static Error IncorrectTitleInput = Error.Validation(
            code: "General.Validation",
            description: "Incorrect title input");

        public static Error IncorrectDescriptionInput = Error.Validation(
            code: "General.Validation",
            description: "Incorrect title input");
    }
}

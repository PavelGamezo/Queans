using ErrorOr;

namespace Queans.Domain.Questions.Errors
{
    public static class QuestionDomainErrors
    {
        public static Error IncorrectTitleInputError = Error.Validation(
            code: "General.Validation",
            description: "Incorrect title input");

        public static Error IncorrectDescriptionInputError = Error.Validation(
            code: "General.Validation",
            description: "Incorrect description input");

        public static Error IncorrectTagNameInputError = Error.Validation(
            code: "General.Validation",
            description: "Incorrect tag name input");

        public static Error IncorrectAnswerTextInputError = Error.Validation(
            code: "General.Validation",
            description: "Text is invalid. Can't input empty text.");
        
        public static Error NotFoundQuestionError = Error.NotFound(
            code: "General.Validation",
            description: "Question is not exist");
    }
}

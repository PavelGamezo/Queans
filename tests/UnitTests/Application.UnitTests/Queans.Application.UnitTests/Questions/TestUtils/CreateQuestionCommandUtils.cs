using Queans.Application.Questions.Commands.CreateQuestion;

namespace Queans.Application.UnitTests.Questions.TestUtils
{
    public static class CreateQuestionCommandUtils
    {
        public static CreateQuestionCommand CreateCommand() =>
            new CreateQuestionCommand(
                UnitTests.TestUtils.Constants.Constants.Question.Title,
                UnitTests.TestUtils.Constants.Constants.Question.Description,
                UnitTests.TestUtils.Constants.Constants.User.UserId,
                UnitTests.TestUtils.Constants.Constants.Question.Tags);
    }
}

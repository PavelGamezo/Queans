using Queans.Application.Answers.Commands.CreateAnswer;

namespace Queans.Application.UnitTests.Answers.TestUtilies
{
    public static class CreateAnswerCommandUtil
    {
        public static CreateAnswerCommand CreateCommand() =>
            new CreateAnswerCommand(
                    TestUtils.Constants.Constants.Answer.Text,
                    TestUtils.Constants.Constants.Question.QuestionId,
                    TestUtils.Constants.Constants.User.UserId
                );
    }
}

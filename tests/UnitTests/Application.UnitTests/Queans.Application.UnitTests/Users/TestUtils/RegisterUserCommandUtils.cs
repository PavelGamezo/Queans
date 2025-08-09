using Queans.Application.Answers.Commands.CreateAnswer;
using Queans.Application.Users.Commands.RegisterUser;

namespace Queans.Application.UnitTests.Users.TestUtils
{
    public static class RegisterUserCommandUtils
    {
        public static RegisterUserCommand CreateCommand() =>
            new RegisterUserCommand(
                    UnitTests.TestUtils.Constants.Constants.User.UserName,
                    UnitTests.TestUtils.Constants.Constants.User.UserEmail,
                    UnitTests.TestUtils.Constants.Constants.User.Password
                );
    }
}

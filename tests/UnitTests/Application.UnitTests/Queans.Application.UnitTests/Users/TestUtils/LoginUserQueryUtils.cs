using Queans.Application.Users.Queries.LoginUser;

namespace Queans.Application.UnitTests.Users.TestUtils
{
    public static class LoginUserQueryUtils
    {
        public static LoginUserQuery CreateQuery() =>
            new LoginUserQuery(
                UnitTests.TestUtils.Constants.Constants.User.UserEmail,
                UnitTests.TestUtils.Constants.Constants.User.Password);
    }
}

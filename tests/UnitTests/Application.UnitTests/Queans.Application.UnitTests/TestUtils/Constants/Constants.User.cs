namespace Queans.Application.UnitTests.TestUtils.Constants
{
    public static partial class Constants
    {
        public static class User
        {
            public static readonly Guid UserId = Guid.NewGuid();

            public const string UserName = "User Name";

            public const string UserEmail = "userEmail@gmail.com";

            public const string Password = "hashed_password";

            public const int Rating = 0;
        }
    }
}

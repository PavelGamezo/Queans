namespace Queans.Api.Users
{
    public record RegisterUserRequest(
        string UserName,
        string UserEmail,
        string Password);
}

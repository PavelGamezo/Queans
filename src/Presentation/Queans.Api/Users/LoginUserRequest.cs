namespace Queans.Api.Users
{
    public record LoginUserRequest(
        string UserEmail,
        string Password);
}

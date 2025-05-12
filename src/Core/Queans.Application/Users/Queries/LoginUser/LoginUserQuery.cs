using ErrorOr;
using Queans.Application.Common.CQRS.Queries;
using Queans.Application.Common.DTOs;

namespace Queans.Application.Users.Queries.LoginUser
{
    public record LoginUserQuery(string Email, string Password) : IQuery<ErrorOr<string>>;
}

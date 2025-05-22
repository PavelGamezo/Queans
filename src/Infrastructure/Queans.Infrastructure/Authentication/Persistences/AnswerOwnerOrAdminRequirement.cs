using Microsoft.AspNetCore.Authorization;

namespace Queans.Infrastructure.Authentication.Persistences
{
    public record AnswerOwnerOrAdminRequirement : IAuthorizationRequirement;
}

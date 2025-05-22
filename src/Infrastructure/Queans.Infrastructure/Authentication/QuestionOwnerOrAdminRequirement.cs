using Microsoft.AspNetCore.Authorization;

namespace Queans.Infrastructure.Authentication
{
    public record QuestionOwnerOrAdminRequirement : IAuthorizationRequirement;
}

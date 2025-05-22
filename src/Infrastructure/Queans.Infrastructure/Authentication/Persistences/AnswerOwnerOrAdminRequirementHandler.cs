using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Queans.Application.Common.Persistence;
using System.Security.Claims;

namespace Queans.Infrastructure.Authentication.Persistences
{
    public class AnswerOwnerOrAdminRequirementHandler : AuthorizationHandler<AnswerOwnerOrAdminRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAnswerRepository _answerRepository;

        public AnswerOwnerOrAdminRequirementHandler(
            IHttpContextAccessor contextAccessor,
            IAnswerRepository answerRepository)
        {
            _contextAccessor = contextAccessor;
            _answerRepository = answerRepository;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AnswerOwnerOrAdminRequirement requirement)
        {
            var httpContext = _contextAccessor.HttpContext;
            if (httpContext == null)
            {
                return;
            }

            if (!httpContext.Request.RouteValues.TryGetValue("answerId", out var idObj) ||
                idObj is not string idStr ||
                !Guid.TryParse(idStr, out var answerId))
            {
                return;
            }

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = context.User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userRole))
            {
                return;
            }

            if (!Guid.TryParse(userId, out var id))
            {
                return;
            }

            var answer = await _answerRepository.GetAnswerByIdAsync(answerId, CancellationToken.None);
            if (answer == null)
            {
                return;
            }

            var isOwner = answer.Author.Id == id;
            var isAdmin = userRole == "Admin";

            if (isOwner || isAdmin)
            {
                context.Succeed(requirement);
            }
        }
    }
}

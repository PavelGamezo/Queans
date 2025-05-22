using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Queans.Application.Common.Persistence;
using System.Security.Claims;

namespace Queans.Infrastructure.Authentication
{
    public class QuestionOwnerOrAdminRequirementHandler : AuthorizationHandler<QuestionOwnerOrAdminRequirement>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public QuestionOwnerOrAdminRequirementHandler(
            IQuestionRepository questionRepository,
            IHttpContextAccessor contextAccessor)
        {
            _questionRepository = questionRepository;
            _contextAccessor = contextAccessor;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            QuestionOwnerOrAdminRequirement requirement)
        {
            var httpContext = _contextAccessor.HttpContext;
            if (httpContext == null)
            {
                return;
            }

            if (!httpContext.Request.RouteValues.TryGetValue("questionId", out var idObj) ||
                idObj is not string idStr ||
                !Guid.TryParse(idStr, out var questionId))
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

            var question = await _questionRepository.GetQuestionByIdAsync(questionId, CancellationToken.None);
            if (question == null)
            {
                return;
            }
            
            var isOwner = question.Author.Id == id;
            var isAdmin = userRole == "Admin";

            if (isOwner || isAdmin)
            {
                context.Succeed(requirement);
            }
        }
    }
}

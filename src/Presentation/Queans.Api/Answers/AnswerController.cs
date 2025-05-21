using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queans.Api.Common.Errors;
using Queans.Application.Answers.Commands.CreateAnswer;
using Queans.Application.Answers.Commands.UpdateAnswer;
using System.Security.Claims;

namespace Queans.Api.Answers
{
    [Route("api/")]
    public class AnswerController : ApiBaseController
    {
        private readonly ISender _sender;

        public AnswerController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsnwer(
            [FromBody] string text,
            [FromRoute] Guid questionId)
        {
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userClaim is null || !Guid.TryParse(userClaim, out var authorId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(new CreateAnswerCommand(text, questionId, authorId));

            return result.Match(
                onValueResult => Ok(onValueResult),
                onErrorResult => Problem(onErrorResult));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAnswer(
            [FromBody] string text)
        {
            var result = await _sender.Send(new UpdateAnswerCommand())
        }
    }
}

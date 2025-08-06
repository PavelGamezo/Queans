using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queans.Api.Common.Errors;
using Queans.Application.Answers.Commands.CreateAnswer;
using Queans.Application.Answers.Commands.RemoveAnswer;
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

        [Authorize]
        [HttpPost]
        [Route("questions/{questionId}/answers")]
        public async Task<IActionResult> CreateAsnwer(
            [FromBody] CreateAnswerRequest request,
            [FromRoute] Guid questionId)
        {
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userClaim is null || !Guid.TryParse(userClaim, out var authorId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(new CreateAnswerCommand(request.Text, questionId, authorId));

            return result.Match(
                onValueResult => Created(),
                onErrorResult => Problem(onErrorResult));
        }

        [Authorize(Policy = "answer-owner-or-admin")]
        [HttpPut]
        [Route("answers/{answerId}/update")]
        public async Task<IActionResult> UpdateAnswer(
            [FromRoute] Guid answerId,
            [FromBody] string text)
        {
            var result = await _sender.Send(new UpdateAnswerCommand(answerId, text));

            return result.Match(
                onValueResult => Accepted(),
                onErrorResult => Problem(onErrorResult));
        }

        [Authorize(Policy = "answer-owner-or-admin")]
        [HttpDelete]
        [Route("answers/{answerId}/delete")]
        public async Task<IActionResult> RemoveAnswer(
            [FromRoute] Guid answerId)
        {
            var result = await _sender.Send(new RemoveAnswerCommand(answerId));

            return result.Match(
                onValueResult => NoContent(),
                onErrorResult => Problem(onErrorResult));
        }
    }
}
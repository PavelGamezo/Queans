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

        //TODO: AnswerOwnerOrAdminPolicy
        [Authorize(Policy = "answer-owner-or-admin")]
        [HttpPut]
        [Route("answers/{answerId}/update")]
        public async Task<IActionResult> UpdateAnswer(
            [FromRoute] Guid id,
            [FromBody] string text)
        {
            var result = await _sender.Send(new UpdateAnswerCommand(id, text));

            return result.Match(
                onValueResult => Ok(onValueResult),
                onErrorResult => Problem(onErrorResult));
        }

        // Delete
        [Authorize(Policy = "answer-owner-or-admin")]
        [HttpDelete]
        [Route("answers/{answerId}/delete")]
        public async Task<IActionResult> RemoveAnswer()
        {
            return Ok();
        }
    }
}
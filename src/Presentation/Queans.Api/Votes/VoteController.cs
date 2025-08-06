using MediatR;
using Microsoft.AspNetCore.Mvc;
using Queans.Api.Common.Errors;
using Queans.Application.Answers.Commands.DownvoteAnswer;
using Queans.Application.Answers.Commands.UpvoteAnswer;
using Queans.Application.Questions.Commands.DownvoteQuestion;
using Queans.Application.Questions.Commands.UpvoteQuestion;
using System.Security.Claims;

namespace Queans.Api.Votes
{
    [Route("api/votes")]
    public class VoteController : ApiBaseController
    {
        private readonly ISender _sender;

        public VoteController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Route("questions/{questionId}/upvote")]
        public async Task<IActionResult> UpvoteQuestion(
            [FromRoute] Guid questionId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim is null || Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(new UpvoteQuestionCommand(questionId, userId));

            return result.Match(
                onValueResult => Ok(),
                onErrorResult => Problem(onErrorResult));
        }

        [HttpPost]
        [Route("questions/{questionId}/downvote")]
        public async Task<IActionResult> DownvoteQuestion(
            [FromRoute] Guid questionId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim is null || Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(new DownvoteQuestionCommand(questionId, userId));

            return result.Match(
                onValueResult => Ok(),
                onErrorResult => Problem(onErrorResult));
        }

        [HttpPost]
        [Route("answers/{answerId}/downvote")]
        public async Task<IActionResult> DownvoteAnswer(
            [FromRoute] Guid answerId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim is null || Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(new DownvoteAnswerCommand(answerId, userId));

            return result.Match(
                onValueResult => Ok(),
                onErrorResult => Problem(onErrorResult));
        }

        [HttpPost]
        [Route("answers/{answerId}/upvote")]
        public async Task<IActionResult> UpvoteAnswer(
            [FromRoute] Guid answerId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim is null || Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(new UpvoteAnswerCommand(answerId, userId));

            return result.Match(
                onValueResult => Ok(),
                onErrorResult => Problem(onErrorResult));
        }
    }
}

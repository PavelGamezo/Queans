using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queans.Api.Common.Errors;
using Queans.Application.Questions.Commands.CreateQuestion;
using Queans.Application.Questions.Queries.GetQuestionsByTags;
using Queans.Application.Questions.Queries.GetQuestionsList;
using System.Security.Claims;

namespace Queans.Api.Questions
{
    [Route("api/[controller]")]
    public class QuestionController : ApiBaseController
    {
        private readonly ISender _sender;

        public QuestionController(ISender sender)
        {
            _sender = sender;
        }

        [Route("questions")]
        [HttpGet]
        public async Task<IActionResult> GetListOfQuestions()
        {
            var result = await _sender.Send(new GetQuestionsListQuery());

            return result.Match(
                onValueResult => Ok(onValueResult),
                onErrorResult => Problem(onErrorResult));
        }

        [Route("questions/filter-by-tags")]
        [HttpGet]
        public async Task<IActionResult> GetQuestionsByTagFiltering(
            [FromQuery] List<string> tags)
        {
            var result = await _sender.Send(new GetQuestionsByTagsQuery(tags));

            return result.Match(
                onValueResult => Ok(onValueResult),
                onErrorResult => Problem(onErrorResult));
        }

        [Authorize("User, Admin")]
        [Route("questions/create")]
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var result = await _sender.Send(new CreateQuestionCommand(request.Title, request.Description, userId));

            return result.Match(
                onValueResult => Ok(onValueResult),
                onErrorResult => Problem(onErrorResult));
        }
    }
}

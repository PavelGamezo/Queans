using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queans.Api.Common.Errors;
using Queans.Application.Common.DTOs;
using Queans.Application.Tags.Commands.CreateTag;
using Queans.Application.Tags.Commands.DeleteTag;
using Queans.Application.Tags.Commands.UpdateTag;

namespace Queans.Api.Tags
{
    [Route("api/")]
    public class TagController : ApiBaseController
    {
        private readonly ISender _sender;

        public TagController(ISender sender)
        {
            _sender = sender;
        }

        [Route("tags/create-tag")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTag(
            [FromBody] CreateTagRequest request)
        {
            var result = await _sender.Send(new CreateTagCommand(request.Name));

            return result.Match(
                onValueResult => Ok(),
                onErrorResult => Problem(onErrorResult));
        }

        [Route("tags/{id}/delete")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTag([FromRoute] Guid id)
        {
            var result = await _sender.Send(new DeleteTagCommand(id));

            return result.Match(
                onValueResult => Ok(onValueResult),
                onErrorResult => Problem(onErrorResult));
        }

        [Route("tags/update-tag")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTag([FromBody] UpdateTagRequest request)
        {
            var result = await _sender.Send(new UpdateTagCommand(
                request.TagId,
                request.Name));

            return result.Match(
                onValueResult => Ok(onValueResult),
                onErrorResult => Problem(onErrorResult));
        }
    }
}

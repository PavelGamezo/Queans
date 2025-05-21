using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Queans.Api.Common.Errors;

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


    }
}

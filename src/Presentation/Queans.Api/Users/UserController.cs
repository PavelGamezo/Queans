using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Queans.Api.Common.Errors;

namespace Queans.Api.Users
{
    [Route("[controller]")]
    public class UserController : ApiBaseController
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterUserRequest request)
        {

            return Ok(request);
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login([FromQuery]LoginUserRequest request)
        {
            return Ok(request);
        }
    }
}

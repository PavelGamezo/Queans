using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Queans.Api.Common.Errors;
using Queans.Application.Users.Commands.RegisterUser;

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
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var result = await _sender.Send(
                new RegisterUserCommand
                    (request.UserName,
                    request.UserEmail,
                    request.Password));

            return result.Match(
                onValueResult => Ok(onValueResult),
                onErrorResult => Problem(onErrorResult));
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login([FromQuery]LoginUserRequest request)
        {
            return Ok(request);
        }
    }
}

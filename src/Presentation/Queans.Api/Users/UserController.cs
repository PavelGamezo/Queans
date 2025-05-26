using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queans.Api.Common.Errors;
using Queans.Application.Users.Commands.RegisterUser;
using Queans.Application.Users.Queries.LoginUser;

namespace Queans.Api.Users
{
    [Route("api/")]
    public class UserController : ApiBaseController
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var result = await _sender.Send(new RegisterUserCommand(
                request.UserName,
                request.UserEmail,
                request.Password));

            return result.Match(
                onValueResult => Ok(onValueResult),
                onErrorResult => Problem(onErrorResult));
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login([FromQuery]LoginUserRequest request)
        {
            var result = await _sender.Send(new LoginUserQuery(
                request.UserEmail,
                request.Password));

            return result.Match(
                onValueResult =>
                {
                    HttpContext.Response.Cookies.Append(
                        "jwt",
                        onValueResult,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = DateTime.UtcNow.AddMinutes(60)
                        });
                    return Ok(onValueResult);
                },
                onErrorResult => Problem(onErrorResult));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok();
        }
    }
}

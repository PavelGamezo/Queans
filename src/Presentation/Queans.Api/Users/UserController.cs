using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Queans.Api.Users
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
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

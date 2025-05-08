using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Queans.Api.Common.Errors
{
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        public IActionResult Problem(List<Error> errors)
        {
            if (errors.Count == 0)
            {
                return Problem();
            }

            if (errors.All(error => error.Type == ErrorType.Validation))
            {
                return ValidationProblem(errors);
            }

            return Problem(errors.First());
        }

        public IActionResult Problem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Failure => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,
            };

            return Problem(statusCode: statusCode, title: error.Description);
        }

        public IActionResult ValidationProblem(List<Error> errors)
        {
            var modelState = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelState.AddModelError(
                    key: error.Code,
                    errorMessage: error.Description);
            }

            return ValidationProblem(modelState);
        }
    }
}

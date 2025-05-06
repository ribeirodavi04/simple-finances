using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleFinances.Application.UseCases.User.Register;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;

namespace SimpleFinances.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase, [FromBody] RequestRegisterUserJson requestUser)
        {
            var result = await useCase.Execute(requestUser);
            return Created(string.Empty, result);
        }
    }
}

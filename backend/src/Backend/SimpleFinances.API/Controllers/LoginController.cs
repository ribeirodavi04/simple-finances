using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleFinances.Application.UseCases.Login.DoLogin;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;

namespace SimpleFinances.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson requestLogin)
        {
            var response = await useCase.Execute(requestLogin);
            return Ok(response);
        }
    }
}

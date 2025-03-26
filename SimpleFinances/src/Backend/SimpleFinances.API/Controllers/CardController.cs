using Microsoft.AspNetCore.Mvc;
using SimpleFinances.Application.UseCases.Card.Register;
using SimpleFinances.Application.UseCases.User.Register;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;

namespace SimpleFinances.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredCardJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseRegisteredCardJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterCardUseCase useCase, [FromBody] RequestRegisterCardJson requestCard)
        {
            var result = await useCase.Execute(requestCard);
            return Created(string.Empty, result);
        }
    }
}

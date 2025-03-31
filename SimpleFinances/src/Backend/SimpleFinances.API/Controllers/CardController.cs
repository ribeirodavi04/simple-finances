using Microsoft.AspNetCore.Mvc;
using SimpleFinances.API.Attributes;
using SimpleFinances.Application.UseCases.Card.Register;
using SimpleFinances.Application.UseCases.Card.Update;
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
        [AuthenticatedUser]
        public async Task<IActionResult> Register([FromServices] IRegisterCardUseCase useCase, [FromBody] RequestCardJson requestCard)
        {
            var result = await useCase.Execute(requestCard);
            return Created(string.Empty, result);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseRegisteredCardJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [AuthenticatedUser]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateCardUseCase useCase,
            [FromRoute]  int id,
            [FromBody] RequestCardJson requestCard)
        {
            await useCase.Execute(id, requestCard);
            return NoContent();
        }
    }
}

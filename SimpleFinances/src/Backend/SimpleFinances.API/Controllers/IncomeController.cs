using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleFinances.API.Attributes;
using SimpleFinances.Application.UseCases.Card.Register;
using SimpleFinances.Application.UseCases.Card.Update;
using SimpleFinances.Application.UseCases.Income.Register;
using SimpleFinances.Application.UseCases.Income.Update;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;

namespace SimpleFinances.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseCardJson), StatusCodes.Status201Created)]
        [AuthenticatedUser]
        public async Task<IActionResult> Register([FromServices] IRegisterIncomeUseCase useCase, [FromBody] RequestIncomeJson requestIncome)
        {
            var result = await useCase.Execute(requestIncome);
            return Created(string.Empty, result);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseCardJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [AuthenticatedUser]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateIncomeUseCase useCase,
            [FromRoute] int id,
            [FromBody] RequestIncomeJson requestIncome)
        {
            await useCase.Execute(id, requestIncome);
            return NoContent();
        }
    }
}

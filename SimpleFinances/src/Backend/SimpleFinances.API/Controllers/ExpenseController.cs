using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleFinances.API.Attributes;
using SimpleFinances.Application.UseCases.Card.Delete;
using SimpleFinances.Application.UseCases.Card.GetById;
using SimpleFinances.Application.UseCases.Card.Register;
using SimpleFinances.Application.UseCases.Card.Update;
using SimpleFinances.Application.UseCases.Expense.Delete;
using SimpleFinances.Application.UseCases.Expense.GetById;
using SimpleFinances.Application.UseCases.Expense.Register;
using SimpleFinances.Application.UseCases.Expense.Update;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;

namespace SimpleFinances.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status201Created)]
        [AuthenticatedUser]
        public async Task<IActionResult> Register([FromServices] IRegisterExpenseUseCase useCase, [FromBody] RequestExpenseJson requestExpense)
        {
            var result = await useCase.Execute(requestExpense);
            return Created(string.Empty, result);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [AuthenticatedUser]
        public async Task<IActionResult> Update(
          [FromServices] IUpdateExpenseUseCase useCase,
          [FromRoute] int id,
          [FromBody] RequestExpenseJson requestExpense)
        {
            await useCase.Execute(id, requestExpense);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [AuthenticatedUser]
        public async Task<IActionResult> Delete([FromServices] IDeleteExpenseUseCase useCase, [FromRoute] int id)
        {
            await useCase.Execute(id);
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [AuthenticatedUser]
        public async Task<IActionResult> GetById([FromServices] IGetExpenseByIdUseCase useCase, [FromRoute] int id)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }
    }
}

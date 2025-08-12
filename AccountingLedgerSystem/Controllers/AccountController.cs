using Core.DTOs;
using Infrastructure.Features.Accounts.Command;
using Infrastructure.Features.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountingLedgerSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAccountsCommand());
            return Ok(result);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetAccountByIdQuery { Id = id });
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AccountDto accountDto)
        {
            var command = new AddAccountCommand { Account = accountDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AccountDto accountDto)
        {
            var command = new UpdateAccountCommand
            {
                Id = id,
                Account = accountDto
            };

            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();

            return Ok(new { message = "Account updated successfully." });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteAccountCommand { Id = id });
            if (!result)
                return NotFound();

            return Ok(new { message = "Account deleted successfully." });
        }
    }
}

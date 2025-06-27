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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAccountsCommand());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountDto accountDto)
        {
            var command = new AddAccountCommand { Account = accountDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}

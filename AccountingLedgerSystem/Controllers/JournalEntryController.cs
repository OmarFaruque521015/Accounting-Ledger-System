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
    public class JournalEntryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JournalEntryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JournalEntryDto dto)
        {
            var command = new AddJournalEntryCommand { Entry = dto };
            var result = await _mediator.Send(command);
            return Ok(new { Id = result });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetJournalEntriesQuery());
            return Ok(result);
        }
    }
}

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

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] JournalEntryDto dto)
        {
            var command = new AddJournalEntryCommand { Entry = dto };
            var result = await _mediator.Send(command);
            return Ok(new { Id = result });
        }

        [HttpGet("getJournalEntry")]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetJournalEntriesQuery());
            return Ok(result);
        }

        [HttpGet("getJournalEntry/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetAccountByIdQuery { Id = id });
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JournalEntryDto dto)
        {
            var command = new UpdateJournalEntryCommand
            {
                Id = id,
                JournalEntry = dto
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

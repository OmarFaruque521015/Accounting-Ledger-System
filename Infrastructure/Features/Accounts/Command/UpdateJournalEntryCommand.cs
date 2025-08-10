using Core.DTOs;
using MediatR;

public class UpdateJournalEntryCommand : IRequest<bool>
{
    public int Id { get; set; }
    public JournalEntryDto JournalEntry { get; set; }
}

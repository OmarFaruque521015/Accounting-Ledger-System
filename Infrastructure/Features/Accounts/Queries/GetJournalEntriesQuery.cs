using Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Accounts.Queries
{
    public class GetJournalEntriesQuery : IRequest<List<JournalEntryViewDto>>
    {
        public int Id { get; set; }
    }
}

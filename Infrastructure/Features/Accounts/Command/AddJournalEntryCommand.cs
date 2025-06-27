using Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Accounts.Command
{
    public class AddJournalEntryCommand : IRequest<int>
    {
        public JournalEntryDto Entry { get; set; } = null!;
    }
}

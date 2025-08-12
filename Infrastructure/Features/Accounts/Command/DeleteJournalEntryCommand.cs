using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Accounts.Command
{
    public class DeleteJournalEntryCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}

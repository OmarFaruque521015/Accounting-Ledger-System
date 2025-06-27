using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class JournalEntryLineViewDto
    {
        public int LineId { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; } = null!;
        public string AccountType { get; set; } = null!;
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}

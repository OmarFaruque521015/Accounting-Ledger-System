using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class JournalEntryViewDto
    {
        public int JournalEntryId { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public List<JournalEntryLineViewDto> Lines { get; set; } = new();
    }
}

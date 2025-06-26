using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class JournalEntry
    {
        public int Id {  get; set; }
        public DateTime Date { get; set; }
        public string? Description {  get; set; }
        public ICollection<JournalEntry> JournalEntries { get; set; }
    }
}

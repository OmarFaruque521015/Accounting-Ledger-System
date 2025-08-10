using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped]
        public ICollection<JournalEntry> JournalEntries { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Type { get; set; } = string.Empty;
        //public ICollection<JournalEntryLine> journalEntryLines { get; set; }
    }
}

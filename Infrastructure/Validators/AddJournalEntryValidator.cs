using Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class AddJournalEntryValidator : AbstractValidator<JournalEntryDto>
    {
        public AddJournalEntryValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
            //RuleFor(x => x.Lines).NotEmpty().WithMessage("At least one line is required.");
            //RuleFor(x => x).Must(HasBalancedEntry).WithMessage("Total Debit and Credit must be equal.");
        }

        //private bool HasBalancedEntry(JournalEntryDto entry)
        //{
        //    var debit = entry.Lines.Sum(x => x.Debit);
        //    var credit = entry.Lines.Sum(x => x.Credit);
        //    return debit == credit;
        //}
    }
}

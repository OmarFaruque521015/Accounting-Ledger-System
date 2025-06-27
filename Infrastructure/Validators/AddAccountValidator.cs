using Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Infrastructure.Validators
{
    public class AddAccountValidator : AbstractValidator<AccountDto>
    {
        public AddAccountValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Account Name is required !");
            RuleFor(x => x.Type).NotEmpty().Must(type => new[]
            {
                "Asset", "Liability", "Equity", "Revenue", "Expense"
            }.Contains(type)).WithMessage("Type must be Asset, Liability, Equity, Revenue, or Expense !");
        }
    }
}

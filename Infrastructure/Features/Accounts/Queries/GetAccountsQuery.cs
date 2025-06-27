using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using MediatR;

namespace Infrastructure.Features.Accounts.Queries
{
    public class GetAccountsCommand: IRequest<List<AccountDto>>
    {
    }
}

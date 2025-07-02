using Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Accounts.Queries
{
    public class GetTrialBalanceQuery:IRequest<List<TrialBalanceDto>>
    {
    }
}

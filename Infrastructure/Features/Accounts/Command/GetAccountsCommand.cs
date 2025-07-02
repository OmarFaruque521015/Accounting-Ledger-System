using MediatR;
using Microsoft.EntityFrameworkCore;
using Core.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Features.Accounts.Queries
{
    public class GetAccountsCommandHandler : IRequestHandler<GetAccountsCommand, List<AccountDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetAccountsCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AccountDto>> Handle(GetAccountsCommand request, CancellationToken cancellationToken)
        {
            var accounts = await _context.Account
                .Select(a => new AccountDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Type = a.Type
                })
                .ToListAsync(cancellationToken);

            return accounts;
        }
    }
}

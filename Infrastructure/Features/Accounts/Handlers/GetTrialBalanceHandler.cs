using Core.DTOs;
using Infrastructure.Features.Accounts.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Accounts.Handlers
{
    public class GetTrialBalanceHandler : IRequestHandler<GetTrialBalanceQuery, List<TrialBalanceDto>>
    {
        private readonly ApplicationDbContext _context;
        public GetTrialBalanceHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TrialBalanceDto>> Handle(GetTrialBalanceQuery query, CancellationToken cancellationToken)
        {
            var result = new List<TrialBalanceDto>();
            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync(cancellationToken);

            using var command = conn.CreateCommand();
            command.CommandText = "sp_GetTrialBalance";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new TrialBalanceDto
                {
                    AccountId = Convert.ToInt32(reader["AccountId"]),
                    AccountName = reader["AccountName"].ToString()!,
                    AccountType = reader["AccountType"].ToString()!,
                    TotalDebit = Convert.ToDecimal(reader["TotalDebit"]),
                    TotalCredit = Convert.ToDecimal(reader["TotalCredit"]),
                    NetBalance = Convert.ToDecimal(reader["NetBalance"])
                });
            }
            return result;
        }
    }
}

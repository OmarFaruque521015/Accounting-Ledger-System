using Core.DTOs;
using Infrastructure.Features.Accounts.Queries;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Accounts.Handlers
{
    public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdQuery, AccountDto>
    {
        private readonly ApplicationDbContext _context;

        public GetAccountByIdHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AccountDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync(cancellationToken);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_GetAccountById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id });

            using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
            if (await reader.ReadAsync(cancellationToken))
            {
                return new AccountDto
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Type = reader["Type"].ToString()
                };
            }

            return null!;
        }
    }
}

using Azure.Core;
using Infrastructure.Features.Accounts.Command;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Accounts.Handlers
{
    public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        public UpdateAccountHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
        {
            var perameters = new[]
            {
                new SqlParameter("@Id",SqlDbType.Int){Value=command.Id},
                new SqlParameter("@Name",SqlDbType.NVarChar){Value=command.Account.Name},
                new SqlParameter("@Type",SqlDbType.NVarChar){Value=command.Account.Type}
            };

            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync(cancellationToken);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_UpdateAccount";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(perameters);

            var affected=await cmd.ExecuteNonQueryAsync(cancellationToken);
            return affected > 0;
        }
    }
}

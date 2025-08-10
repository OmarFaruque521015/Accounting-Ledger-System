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
    public class UpdateJournalEntryHandler : IRequestHandler<UpdateJournalEntryCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        public UpdateJournalEntryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateJournalEntryCommand command, CancellationToken cancellationToken)
        {
            var perameters = new[]
            {
                new SqlParameter("@Id",SqlDbType.Int){Value=command.Id},
                new SqlParameter("@date",SqlDbType.NVarChar){Value=command.JournalEntry.Date},
                new SqlParameter("@description",SqlDbType.NVarChar){Value=command.JournalEntry.Description}
            };

            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync(cancellationToken);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_UpdateJournalEntry";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(perameters);

            var affected=await cmd.ExecuteNonQueryAsync(cancellationToken);
            return affected > 0;
        }
    }
}

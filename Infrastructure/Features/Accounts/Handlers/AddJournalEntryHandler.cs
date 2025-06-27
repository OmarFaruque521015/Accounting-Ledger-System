using Infrastructure.Features.Accounts.Command;
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
    public class AddJournalEntryHandler : IRequestHandler<AddJournalEntryCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public AddJournalEntryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddJournalEntryCommand request, CancellationToken cancellationToken)
        {
            var entryId = 0;

            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync(cancellationToken);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_AddJournalEntry";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = request.Entry.Date });
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 255) { Value = request.Entry.Description ?? (object)DBNull.Value });

            var result = await cmd.ExecuteScalarAsync(cancellationToken);
            entryId = Convert.ToInt32(result);

            foreach (var line in request.Entry.Lines)
            {
                var insertLine = conn.CreateCommand();
                insertLine.CommandText = @"
                INSERT INTO JournalEntryLine (JournalEntryId, AccountId, Debit, Credit)
                VALUES (@JournalEntryId, @AccountId, @Debit, @Credit)";
                insertLine.Parameters.Add(new SqlParameter("@JournalEntryId", entryId));
                insertLine.Parameters.Add(new SqlParameter("@AccountId", line.AccountId));
                insertLine.Parameters.Add(new SqlParameter("@Debit", line.Debit));
                insertLine.Parameters.Add(new SqlParameter("@Credit", line.Credit));

                await insertLine.ExecuteNonQueryAsync(cancellationToken);
            }

            return entryId;
        }
    }
}

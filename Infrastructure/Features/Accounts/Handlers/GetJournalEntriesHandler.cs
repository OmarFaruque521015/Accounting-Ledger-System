using Core.DTOs;
using Infrastructure.Features.Accounts.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Accounts.Handlers
{
    public class GetJournalEntriesHandler : IRequestHandler<GetJournalEntriesQuery, List<JournalEntryViewDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetJournalEntriesHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<JournalEntryViewDto>> Handle(GetJournalEntriesQuery request, CancellationToken cancellationToken)
        {
            //var result = new List<JournalEntryViewDto>();
            //var lookup = new Dictionary<int, JournalEntryViewDto>();

            //using var conn = _context.Database.GetDbConnection();
            //await conn.OpenAsync(cancellationToken);

            //using var cmd = conn.CreateCommand();
            //cmd.CommandText = "sp_GetJournalEntries";
            //cmd.CommandType = CommandType.StoredProcedure;

            //using var reader = await cmd.ExecuteReaderAsync();

            //while (await reader.ReadAsync())
            //{
            //    var entryId = Convert.ToInt32(reader["Id"]);

            //    if (!lookup.ContainsKey(entryId))
            //    {
            //        lookup[entryId] = new JournalEntryViewDto
            //        {
            //            //JournalEntryId = entryId,
            //            Date = Convert.ToDateTime(reader["Date"]),
            //            Description = reader["Description"].ToString(),
            //            //Lines = new List<JournalEntryLineViewDto>()
            //        };

            //        result.Add(lookup[entryId]);
            //    }

            //    //var line = new JournalEntryLineViewDto
            //    //{
            //    //    LineId = Convert.ToInt32(reader["LineId"]),
            //    //    AccountId = Convert.ToInt32(reader["AccountId"]),
            //    //    AccountName = reader["AccountName"].ToString()!,
            //    //    AccountType = reader["AccountType"].ToString()!,
            //    //    Debit = Convert.ToDecimal(reader["Debit"]),
            //    //    Credit = Convert.ToDecimal(reader["Credit"])
            //    //};

            //    //lookup[entryId].Lines.Add(line);
            //}

            //return result;

            var result = new List<JournalEntryViewDto>();
            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync(cancellationToken);

            using var command = conn.CreateCommand();
            command.CommandText = "sp_GetJournalEntries";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new JournalEntryViewDto
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Date = Convert.ToDateTime(reader["Date"]),
                    Description = reader["Description"].ToString()
                });
            }
            return result;
        }
    }
}

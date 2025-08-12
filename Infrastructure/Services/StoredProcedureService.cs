using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class StoredProcedureService : IStoredProcedureService
    {
        private readonly ApplicationDbContext _context;
        public StoredProcedureService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> ExecuteNonQueryAsync(string procedureName, params object[] parameters)
        {
            return await _context.Database.ExecuteSqlRawAsync(
                            $"EXEC {procedureName} {BuildParameterList(parameters.Length)}",
                            parameters
                        );
        }

        public async Task<T> ExecuteScalarAsync<T>(string procedureName, params object[] parameters)
        {
            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = procedureName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (var param in parameters)
                cmd.Parameters.Add(param);

            var result = await cmd.ExecuteScalarAsync();
            await conn.CloseAsync();

            return (T)Convert.ChangeType(result, typeof(T));
        }

        public async Task<List<T>> QueryAsync<T>(string procedureName, params object[] parameters) where T : class, new()
        {
            return await _context.Set<T>()
                .FromSqlRaw($"EXEC {procedureName} {BuildParameterList(parameters.Length)}", parameters)
                .ToListAsync();
        }

        private string BuildParameterList(int count)
        {
            return string.Join(", ", Enumerable.Range(0, count).Select(i => $"@p{i}"));
        }
    }
}

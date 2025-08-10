using Infrastructure.Features.Accounts.Command;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.Accounts.Handlers
{
    public class AddAccountHandler : IRequestHandler<AddAccountCommand, int>
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AddAccountHandler(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<int> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Prepare parameters
                var parameters = new[]
                {
                    new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = request.Account.Name },
                    new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = request.Account.Type }
                };

                // Open the connection manually (ADO.NET-style)
                using var conn = _context.Database.GetDbConnection();
                await conn.OpenAsync(cancellationToken);

                // Create and configure command
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "sp_AddAccount";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);

                // Execute and return the newly created Account ID
                var result = await cmd.ExecuteScalarAsync(cancellationToken);

                if (result == null || result == DBNull.Value)
                {
                    throw new Exception("Account already exists.");
                }

                return Convert.ToInt32(result);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("already exists"))
                {
                    throw new Exception("Account already exists.", ex);
                }

                throw new Exception("An error occurred while adding the account.", ex);
            }
        }
    }
}

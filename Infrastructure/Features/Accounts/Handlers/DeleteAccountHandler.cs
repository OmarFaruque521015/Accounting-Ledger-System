using Infrastructure.Features.Accounts.Command;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Features.Accounts.Handlers
{
    public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteAccountHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var parameter = new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id };

            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync(cancellationToken);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_DeleteAccount";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(parameter);

            var affected = await cmd.ExecuteNonQueryAsync(cancellationToken);
            return affected > 0;
        }
    }
}

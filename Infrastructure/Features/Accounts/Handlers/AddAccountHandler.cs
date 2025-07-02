using Infrastructure.Features.Accounts.Command;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.Accounts.Handlers;

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
        var parameters = new[]
        {
            new SqlParameter("@Name", SqlDbType.NVarChar) { Value = request.Account.Name },
            new SqlParameter("@Type", SqlDbType.NVarChar) { Value = request.Account.Type }
        };

        using var conn = _context.Database.GetDbConnection();
        await conn.OpenAsync(cancellationToken);

        using var cmd = conn.CreateCommand();
        cmd.CommandText = "sp_AddAccount";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddRange(parameters);

        var result = await cmd.ExecuteScalarAsync(cancellationToken);
        return Convert.ToInt32(result);
    }
}

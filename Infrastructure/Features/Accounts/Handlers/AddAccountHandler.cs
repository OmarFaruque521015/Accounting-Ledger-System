using Core.DTOs;
using Infrastructure.Features.Accounts.Queries;
using Infrastructure;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Application.Features.Accounts.Handlers;

public class GetAccountsHandler : IRequestHandler<GetAccountsCommand, List<AccountDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAccountsHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AccountDto>> Handle(GetAccountsCommand request, CancellationToken cancellationToken)
    {
        var accounts = new List<AccountDto>();

        using var conn = _context.Database.GetDbConnection();
        await conn.OpenAsync(cancellationToken);

        using var command = conn.CreateCommand();
        command.CommandText = "sp_GetAccounts";
        command.CommandType = CommandType.StoredProcedure;

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            accounts.Add(new AccountDto
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString()!,
                Type = reader["Type"].ToString()!
            });
        }

        return accounts;
    }
}

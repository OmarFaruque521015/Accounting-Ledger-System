using Core.DTOs;
using MediatR;

public class UpdateAccountCommand : IRequest<bool>
{
    public int Id { get; set; }
    public AccountDto Account { get; set; }
}

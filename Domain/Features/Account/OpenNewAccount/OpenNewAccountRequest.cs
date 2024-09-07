using MediatR;

namespace Domain.Features.Account.OpenNewAccount;

public abstract record BaseOpenNewAccountRequest(Guid CustomerId, decimal InitialBalance) : IRequest<OpenNewAccountResponse>;

public record OpenNewAccountRequest(Guid CustomerId, decimal InitialBalance) 
    : BaseOpenNewAccountRequest(CustomerId , InitialBalance>=0?InitialBalance:throw new ArgumentOutOfRangeException(nameof(InitialBalance)));
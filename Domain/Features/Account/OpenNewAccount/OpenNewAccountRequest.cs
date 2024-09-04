using MediatR;

namespace Domain.Features.Account.OpenNewAccount;

public record OpenNewAccountRequest(Guid CustomerId, decimal InitialBalance) : IRequest<OpenNewAccountResponse>;
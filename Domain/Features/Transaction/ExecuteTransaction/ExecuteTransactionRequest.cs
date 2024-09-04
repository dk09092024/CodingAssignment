using MediatR;

namespace Domain.Features.Transaction.ExecuteTransaction;

public record ExecuteTransactionRequest() : IRequest<ExecuteTransactionResponse>;
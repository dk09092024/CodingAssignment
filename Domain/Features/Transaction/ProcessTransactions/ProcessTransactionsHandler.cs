using Domain.Features.Transaction.ExecuteTransactionsForAccount;
using Domain.Features.Transaction.ValidateTransaction;
using Domain.Model.ENUM;
using Domain.Repositories;
using Hangfire;
using MediatR;

namespace Domain.Features.Transaction.ProcessTransactions;

public class ProcessTransactionsHandler : IRequestHandler<ProcessTransactionsRequest, ProcessTransactionsResponse>
{
    private const int BatchSizeValidation = 1000;
    private const int BatchSizeExecution = 1000;
    private ITransactionRepository _transactionRepository;
    private IMediator _mediator;

    public ProcessTransactionsHandler(ITransactionRepository transactionRepository, IMediator mediator)
    {
        _transactionRepository = transactionRepository;
        _mediator = mediator;
    }

    public async Task<ProcessTransactionsResponse> Handle(ProcessTransactionsRequest request, CancellationToken cancellationToken)
    {
        var recivedTransactionProtocolls = 
            await _transactionRepository.GetAllReceivedTransactionsAsync(BatchSizeValidation, cancellationToken);
        var validTransactionProtocolls= 
            await _transactionRepository.GetAllValidTransactionsAsync(BatchSizeExecution, cancellationToken);

        foreach (var recivedTransaction in recivedTransactionProtocolls)
        {
            if(!cancellationToken.IsCancellationRequested)
                BackgroundJob.Enqueue(() => _mediator.Send(new ValidateTransactionRequest(recivedTransaction.TransactionId),
                    new CancellationToken()));
        }

        foreach (var validTransactionProtocoll in validTransactionProtocolls)
        {
            await _transactionRepository.UpdateTransactionStateAsync(validTransactionProtocoll, TransactionState.Processing,
                null, null, null,cancellationToken);
        }
        foreach (var accountId in validTransactionProtocolls.Select(protocol => protocol.AccountId ).Distinct())
        {
            if(!cancellationToken.IsCancellationRequested)
                BackgroundJob.Enqueue(() => _mediator.Send(new ExecuteTransactionsForAccountRequest(accountId,
                    validTransactionProtocolls.Select(protocol => protocol.TransactionId).ToArray()), new CancellationToken()));
        }
        return new ProcessTransactionsResponse(recivedTransactionProtocolls.Count, validTransactionProtocolls.Count);

    }

    
}
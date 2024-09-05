using Domain.Features.Transaction.ExecuteTransaction;
using Domain.Features.Transaction.ValidateTransaction;
using Domain.Model;
using Domain.Model.ENUM;
using Domain.Repositories;
using Hangfire;
using MediatR;

namespace Domain.Features.Transaction.ProcessTransactions;

public class ProcessTransactionsHandler : IRequestHandler<ProcessTransactionsRequest, ProcessTransactionsResult>
{
    private const int BatchSizeValidation = 1000;
    private const int BatchSizeExecution = 1000;
    private ITransactionRepository _transactionRepository;
    private IMediator _mediator;

    public ProcessTransactionsHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IMediator mediator)
    {
        _transactionRepository = transactionRepository;
        _mediator = mediator;
    }

    public async Task<ProcessTransactionsResult> Handle(ProcessTransactionsRequest request, CancellationToken cancellationToken)
    {
        var recivedTransactionProtokolls = await _transactionRepository.GetAllRecivedTransactionsAsync(BatchSizeValidation);
        var validTransactionProtokolls= await _transactionRepository.GetAllValidTransactionsAsync(BatchSizeExecution);

        foreach (var recivedTransaction in recivedTransactionProtokolls)
        {
            BackgroundJob.Enqueue(() => _mediator.Send(new ValidateTransactionRequest(recivedTransaction.TransactionId), cancellationToken));
        }

        foreach (var accountId in validTransactionProtokolls.Select(protokol => protokol.AccountId ).Distinct())
        {
            BackgroundJob.Enqueue(() => _mediator.Send(new ExecuteTransactionsForAccountRequest(accountId, validTransactionProtokolls.Select(protokol => protokol.TransactionId).ToArray()), cancellationToken));
        }
        return new ProcessTransactionsResult(recivedTransactionProtokolls.Count, validTransactionProtokolls.Count);

    }

    
}
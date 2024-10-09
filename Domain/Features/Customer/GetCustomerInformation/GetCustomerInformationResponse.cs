using Domain.Model.ENUM;

namespace Domain.Features.Customer.GetCustomerInformation;

public record GetCustomerInformationResponse(Guid Id, string Name, string Surname, CustomerAccountResponse[] Account);

public record CustomerAccountResponse(Guid Id, decimal Balance, CustomerTransactionHistoryResponse[] TransactionHistory);

public record CustomerTransactionHistoryResponse(Guid Id, TransactionState State,
    CustomerTransactionResponse Transaction, DateTime? TimeExecuted, decimal? BalanceBeforeExecution,
    decimal? BalanceAfterExecution);

public record CustomerTransactionResponse(Guid Id, TransactionType Type, decimal Amount, DateTime TimeReceived);
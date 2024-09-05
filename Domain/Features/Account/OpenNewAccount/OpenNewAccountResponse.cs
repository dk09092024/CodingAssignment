namespace Domain.Features.Account.OpenNewAccount;

public record OpenNewAccountResponse(Guid AccountId, Guid? InitialTransactionId=null!);
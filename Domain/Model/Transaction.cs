using Domain.Model.ENUM;

namespace Domain.Model;

public class Transaction 
{
    public required Guid Id { get; set; } = new();
    public required decimal Amount { get; set; }
    public required Guid TargetAccountId { get; set; }
    public required Account TargetAccount { get; set; }
    public required TransactionType Type { get; set; }
    public required DateTime TimeRecived { get; set; }
}
using Domain.Model.ENUM;
using Domain.Model.General;

namespace Domain.Model;

public class TransactionProtokol : Entity
{ 
    public required Guid AccountId { get; set; }
    public required Account Account { get; set; }
    public required Guid TransactionId { get; set; }
    public required Transaction Transaction { get; set; }
    public decimal? BalanceBefore { get; set; }
    public decimal? BalanceAfter { get; set; }
    public required TransactionState State { get; set; }
    public DateTime? TimeOfExecution { get; set; }
}
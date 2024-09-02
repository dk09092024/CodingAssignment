using Infrastructure.Model.ENUM;
using Infrastructure.Model.General;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Model;

public class TransactionProtokol : Entity
{ 
    public required Account Account { get; set; }
    public required Transaction Transaction { get; set; }
    public decimal? BalanceBefore { get; set; }
    public decimal? BalanceAfter { get; set; }
    public required TransactionState State { get; set; }
    public DateTime? TimeOfExecution { get; set; }
}
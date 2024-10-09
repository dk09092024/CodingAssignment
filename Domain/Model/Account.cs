using Domain.Model.General;

namespace Domain.Model;

public class Account : Entity
{
    public required decimal Balance { get; set; }
    public required List<TransactionProtocol> TransactionHistory { get; set; } = new();
    public required Customer Customer { get; set; }
}
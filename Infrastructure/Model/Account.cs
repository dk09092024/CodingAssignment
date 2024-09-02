using Infrastructure.Model.General;

namespace Infrastructure.Model;

public class Account : Entity
{
    public required decimal Balance { get; set; }
    public required List<TransactionProtokol> TransactionHistory { get; set; } = new();
    public required Customer Customer { get; set; }
}
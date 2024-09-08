using Domain.Model.General;

namespace Domain.Model;

public class Customer : Entity
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required List<Account> Accounts { get; set; } = new();
}
namespace Domain.Model.General;

public abstract class Entity
{
    public required Guid Id { get; set; }
    public required DateTime TimeCreated { get; set; }
    public DateTime? LastModified { get; set; }
}
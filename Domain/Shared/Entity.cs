namespace Domain.Shared;

public class Entity : EntityDomainEvent
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedDate { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; protected set; }
}
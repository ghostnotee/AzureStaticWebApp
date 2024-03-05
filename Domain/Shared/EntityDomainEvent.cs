#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using MediatR;

namespace Domain.Shared;

public abstract class EntityDomainEvent
{
    private List<INotification> _domainEvents;
    public List<INotification> DomainEvents => _domainEvents;

    public void AddDomainEvent(INotification domainEvent)
    {
        _domainEvents = _domainEvents ?? new List<INotification>();
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(INotification domainEvent)
    {
        _domainEvents?.Remove(domainEvent);
    }
}
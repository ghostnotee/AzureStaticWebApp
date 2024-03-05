using Application.Common.Interfaces.Persistence;
using Domain.Shared;
using MediatR;

namespace Application.Common.Behaviors;

public class UnitOfWorkBehavior<TRequest, TResponse>(IEnumerable<IUnitOfWork> unitOfWorks, IPublisher publisher)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();
        await PublishDomainEvents(cancellationToken);
        foreach (var context in unitOfWorks.Where(x => x.GetChanges<Entity>().Any())) await context.SaveChangesAsync(cancellationToken);
        return response;
    }

    private async Task PublishDomainEvents(CancellationToken cancellationToken)
    {
        while (AnyDomainEvents())
        {
            var entities = unitOfWorks.SelectMany(x => x.GetChanges<EntityDomainEvent>())
                .Where(x => true).ToList();

            var notifications = entities.SelectMany(x => x.DomainEvents!).ToList();

            entities.ForEach(x => x.DomainEvents!.Clear());

            foreach (var @event in notifications) await publisher.Publish(@event, cancellationToken);
        }
    }

    private bool AnyDomainEvents()
    {
        return unitOfWorks.SelectMany(x => x.GetChanges<EntityDomainEvent>())
            .Where(x => x.DomainEvents != null)
            .Any(x => x.DomainEvents.Count != 0);
    }
}
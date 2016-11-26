namespace Common.Domain
{
    public interface IDomainEventPublisher
    {
        void Subscribe<T>(IHandle<T> handler) where T : IDomainEvent;
        void Publish<T>(T domainEvent) where T : IDomainEvent;
    }
}

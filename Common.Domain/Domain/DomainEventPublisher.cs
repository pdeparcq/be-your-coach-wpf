using MemBus;
using MemBus.Configurators;

namespace Deparcq.Common.Domain
{
    public class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly IBus _bus;

        public DomainEventPublisher()
        {
            _bus = BusSetup.StartWith<Conservative>().Construct();
        }

        public void Publish<T>(T domainEvent) where T : IDomainEvent
        {
            _bus.Publish(domainEvent);
        }

        public void Subscribe<T>(IHandle<T> handler) where T : IDomainEvent
        {
            _bus.Subscribe((T domainEvent) =>
            {
                handler.Handle(domainEvent);
            });
        }
    }
}

namespace Deparcq.Common.Domain
{
    public interface IHandle<in T> where T:IDomainEvent
    {
        void Handle(T domainEvent);
    }
}

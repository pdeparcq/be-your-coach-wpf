using System.Collections.Generic;
using Deparcq.Common.Domain;

namespace Deparcq.Common.Application
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly List<IEntityRepository> _repositories;

        public IDomainEventPublisher DomainEventPublisher { get; set; }

        public UnitOfWork(IDomainEventPublisher publisher)
        {
            _repositories = new List<IEntityRepository>();
            DomainEventPublisher = publisher;
        }

        public void Register(IEntityRepository entityRepository)
        {
            _repositories.Add(entityRepository);
        }

        public void Commit()
        {
            _repositories.ForEach(r =>
            {
                foreach (var entity in r.EntitiesWithEvents)
                {
                    foreach (var domainEvent in entity.Events)
                    {
                        DomainEventPublisher.Publish(domainEvent);
                    }
                    entity.ClearEvents();
                }
            });
            _repositories.ForEach(r => r.SaveChanges());
        }

        public void Rollback()
        {
            _repositories.ForEach(r =>
            {
                foreach (var entity in r.EntitiesWithEvents)
                {
                    entity.ClearEvents();
                }
            });
            _repositories.ForEach(r => r.UndoChanges());
        }
    }
}

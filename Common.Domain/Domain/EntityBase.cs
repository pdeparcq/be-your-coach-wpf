using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Deparcq.Common.Domain
{
    public abstract class EntityBase : IEntity
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid();
            Events = new List<IDomainEvent>();
        }

        public Guid Id { get; private set; }

        [JsonIgnore]
        public ICollection<IDomainEvent> Events { get; }

        public void ClearEvents()
        {
            Events.Clear();
        }
    }
}
using System;
using System.Collections.Generic;

namespace Common.Domain
{
    public interface IEntity
    {
        Guid Id { get; }

        ICollection<IDomainEvent> Events { get; }

        void ClearEvents();
    }
}

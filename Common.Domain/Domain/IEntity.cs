using System;
using System.Collections.Generic;

namespace Deparcq.Common.Domain
{
    public interface IEntity
    {
        Guid Id { get; }

        ICollection<IDomainEvent> Events { get; }

        void ClearEvents();
    }
}

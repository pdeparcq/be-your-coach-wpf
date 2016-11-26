using System.Collections.Generic;

namespace Deparcq.Common.Domain
{
    public interface IEntityRepository {
        void SaveChanges();
        void UndoChanges();
        IEnumerable<IEntity> EntitiesWithEvents { get; }
    }
}
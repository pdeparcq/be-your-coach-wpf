using System.Collections.Generic;

namespace Common.Domain
{
    public interface IEntityRepository {
        void SaveChanges();
        void UndoChanges();
        IEnumerable<IEntity> EntitiesWithEvents { get; }
    }
}
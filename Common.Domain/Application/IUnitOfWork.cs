using Deparcq.Common.Domain;

namespace Deparcq.Common.Application
{
    public interface IUnitOfWork
    {
        void Register(IEntityRepository repository);
        void Commit();
        void Rollback();
    }
}

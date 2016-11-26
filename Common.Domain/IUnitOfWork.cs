namespace Common.Domain
{
    public interface IUnitOfWork
    {
        void Register(IEntityRepository repository);
        void Commit();
        void Rollback();
    }
}

using System;

namespace Deparcq.Common.Application
{
    public class ApplicationServiceBase
    {
        protected IUnitOfWork UnitOfWork { get; }

        protected ApplicationServiceBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected T InUnitOfWork<T>(Func<T> execute)
        {
            try
            {
                var result = execute();
                UnitOfWork.Commit();
                return result;
            }
            catch (Exception)
            {
                UnitOfWork.Rollback();
                throw;
            }
        }
    }
}
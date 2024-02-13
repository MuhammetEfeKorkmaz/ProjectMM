using Dal.Abstract.ForUser;
using FullSharedCore.DataAccess.Abstract;

namespace Dal.Abstract
{
    public interface IUnitOfWork: IBaseUnitOfWork
    {
        public ISystemUserDal systemUserDal { get; }
        public IOperationClaimsDal operationClaimsDal { get; }


    
    }
}

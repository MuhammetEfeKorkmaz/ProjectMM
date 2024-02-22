using Dal.Abstract.ForUser;
using Entities.DbModels.UserModels;
using FullSharedCore.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;





namespace Dal.Concrete.ForUser
{
    public class OperationClaimsDal : Repository<OperationClaims>, IOperationClaimsDal 
    { 
        public OperationClaimsDal(DbSet<OperationClaims> dbset) : base(dbset) { } }

}

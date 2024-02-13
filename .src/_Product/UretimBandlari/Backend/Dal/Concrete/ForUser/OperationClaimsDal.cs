using Dal.Abstract.ForUser;
using Entities.DbModels.UserModels;
using FullSharedCore.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;





namespace Dal.Concrete.ForUser
{
    public class OperationClaimsDal : Repository<OperationClaims>, IOperationClaimsDal 
    {
       // private static DbSet<OperationClaims> dbset { get => RepositoryGet.GetDbSet<OperationClaims, PDbContext>(); }
        public OperationClaimsDal(DbSet<OperationClaims> dbset) : base(dbset) { } }

}

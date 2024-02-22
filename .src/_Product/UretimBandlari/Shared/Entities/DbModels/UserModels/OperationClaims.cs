using FullSharedResults.BaseModels;
namespace Entities.DbModels.UserModels
{
    public class OperationClaims : _EntitiyBaseModel
    {
        public OperationClaims()
        {
            SystemUsers = new HashSet<OperationClaimsSystemUser>();
        }

       
        public string Name { get; set; }

        public virtual ICollection<OperationClaimsSystemUser> SystemUsers { get; set; }
    }
}

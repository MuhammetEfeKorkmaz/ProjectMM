using FullSharedCore.SharedModels.BaseModels;

namespace Entities.DbModels.UserModels
{
    public class OperationClaims : _EntitiyBaseModel
    {
        public OperationClaims()
        {
            SystemUsers = new List<SystemUser>();
        }

       
        public string Name { get; set; }

        public virtual List<SystemUser> SystemUsers { get; set; }
    }
}

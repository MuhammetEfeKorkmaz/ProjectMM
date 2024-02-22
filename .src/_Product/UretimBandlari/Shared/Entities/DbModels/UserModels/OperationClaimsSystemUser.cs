using FullSharedResults.BaseModels;
namespace Entities.DbModels.UserModels
{
    public class OperationClaimsSystemUser : _EntitiyBaseModel
    {
        public int SystemUserId { get; set; }
        public int OperationClaimsId { get; set; }

        public OperationClaims OperationClaims { get; set; }
        public SystemUser SystemUser { get; set; }

    }
}

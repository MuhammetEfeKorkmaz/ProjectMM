using FullSharedResults.BaseModels;
namespace Entities.DbModels.UserModels
{
    public class SystemUser : _EntitiyBaseModel
    {
        public SystemUser()
        {
            OperationClaims = new HashSet<OperationClaimsSystemUser>();
        }


   
        public string Name { get; set; }


        public string Nick { get; set; }

      
        public string Email { get; set; }

     
        public byte[] PasswordHash { get; set; }

       
        public byte[] PasswordSalt { get; set; }


        public string Password { get; set; }


        public virtual  ICollection<OperationClaimsSystemUser> OperationClaims { get; set; }
    }












}

using FullSharedCore.SharedModels.BaseModels;

namespace Entities.DbModels.UserModels
{
    public class SystemUser : _EntitiyBaseModel
    {
        public SystemUser()
        {
            OperationClaimss = new List<OperationClaims>();
        }


   
        public string Name { get; set; }


        public string Nick { get; set; }

      
        public string Email { get; set; }

     
        public byte[] PasswordHash { get; set; }

       
        public byte[] PasswordSalt { get; set; }


        public string Password { get; set; }


        public virtual  List<OperationClaims> OperationClaimss { get; set; }
    }
}

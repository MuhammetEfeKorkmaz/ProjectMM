using FullSharedResults.BaseModels;

namespace DTOs.UserModels
{
    public class SystemUserReturnDto:_DtoBaseModel
    {
        public SystemUserReturnDto()
        {
            OperationClaimss = new List<OperationClaimsReturnDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }


        public string Nick { get; set; }


        public string Email { get; set; }
          
        public string Password { get; set; }


        public List<OperationClaimsReturnDto> OperationClaimss { get; set; }
    }
}

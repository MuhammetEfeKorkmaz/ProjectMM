using FullSharedResults.BaseModels;

namespace DTOs.UserModels
{
    public class SystemUserAddUpdateDto: _DtoBaseModel
    {
        public string Name { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<int> OperationClaimsID { get; set; }

        public int Id { get; set; }
    }
}

namespace DTOs.UserModels
{
    public class OperationClaimAddUpdateForSystemUserDto : _DtoBaseModel
    {
        public int SystemUserFID { get; set; }
        public int OperationClaimsFID { get; set; }
    }
}

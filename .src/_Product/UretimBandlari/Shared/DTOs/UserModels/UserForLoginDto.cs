using FullSharedResults.BaseModels;

namespace DTOs.UserModels
{
    public class UserForLoginDto : _DtoBaseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

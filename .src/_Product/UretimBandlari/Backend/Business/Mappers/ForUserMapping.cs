using AutoMapper;
using DTOs.UserModels;
using Entities.DbModels.UserModels;

namespace Business.Mappers
{
    public class ForUserMapping : Profile
    {
        public ForUserMapping()
        {
            CreateMap<SystemUser, SystemUserAddUpdateDto>().ReverseMap();
            CreateMap<SystemUserAddUpdateDto, SystemUser>().ReverseMap();
            CreateMap<SystemUser, SystemUserReturnDto>().ReverseMap();
            CreateMap<SystemUserReturnDto, SystemUser>().ReverseMap();
            CreateMap<OperationClaimsReturnDto, OperationClaims>().ReverseMap();
            CreateMap<OperationClaims, OperationClaimsReturnDto>().ReverseMap();
        }
    }
}

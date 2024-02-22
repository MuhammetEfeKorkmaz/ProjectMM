using AutoMapper;
using Business.Concrete.ForUser.Commands.Models;
using DTOs.UserModels;
using DTOs.UserModels.Commands;
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


            CreateMap<OgrenciAddCommandDto, OgrenciAddCommandMeditr>().ReverseMap();
            CreateMap<OgrenciAddCommandMeditr, OgrenciAddCommandDto>().ReverseMap();
             


           // CreateMap<OgrenciAddCommandMeditr, OgrenciAddCommandDto>().IncludeAllDerived();
            //CreateMap<OgrenciAddCommandDto, OgrenciAddCommandMeditr>().IncludeAllDerived();
        }
    }
}

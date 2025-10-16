using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PL.ViewModels;

namespace PL.MappingProfile
{
    public class RoleProfile : Profile
    {
        public RoleProfile() 
        {
            CreateMap<IdentityRole, RoleViewModel>()
                 .ForMember(D=>D.RoleName, O=>O.MapFrom(S=>S.Name)).ReverseMap();
        }
    }
}

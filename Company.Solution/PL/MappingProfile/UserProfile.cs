using AutoMapper;
using DAL.Entities;
using PL.ViewModels;

namespace PL.MappingProfile
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<AppUser, UserViewModel>().ReverseMap();
        }
    }
}

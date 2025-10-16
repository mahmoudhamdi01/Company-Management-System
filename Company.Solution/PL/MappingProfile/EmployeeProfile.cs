using AutoMapper;
using DAL.Entities;
using PL.ViewModels;

namespace PL.MappingProfile
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}

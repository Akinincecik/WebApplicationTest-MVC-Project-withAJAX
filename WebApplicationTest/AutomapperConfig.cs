using AutoMapper;
using WebApplicationTest.Entities;
using WebApplicationTest.Models;

namespace WebApplicationTest
{
    public class AutomapperConfig:Profile
    {
        public AutomapperConfig()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, CreateUserViewModel>().ReverseMap();
            CreateMap<User, EditUserViewModel>().ReverseMap();
            CreateMap<StudentInfo, StudentViewModel>().ReverseMap();
            CreateMap<StudentInfo, CreateStudentViewModel>().ReverseMap();
            CreateMap<StudentInfo, EditStudentViewModel>().ReverseMap();

        }
    }
}

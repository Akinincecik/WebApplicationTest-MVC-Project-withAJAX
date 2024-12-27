using AutoMapper;
using WebApplicationTest.Entities;
using WebApplicationTest.Models;

namespace WebApplicationTest
{
    public class AutomapperConfig:Profile
    {
        public AutomapperConfig()
        {
            CreateMap<User, UserViewModels>().ReverseMap();
            CreateMap<User, CreateUserModel>().ReverseMap();
            CreateMap<User, EditUserModel>().ReverseMap();

        }
    }
}

using AutoMapper;
using EnglishQuizSystem.DTO;
using EnglishQuizSystem.Models;

namespace EnglishQuizSystem.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}

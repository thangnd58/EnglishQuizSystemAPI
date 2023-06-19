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
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Quiz, QuizDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<UserAnswer, UserAnswerDTO>().ReverseMap();
            CreateMap<UserQuiz, UserQuizDTO>().ReverseMap();
        }
    }
}

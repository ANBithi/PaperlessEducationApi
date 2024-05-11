using Api.Controllers;
using Api.Models;
using Api.Models.Post;
using Api.Requests;
using Api.ViewModels;
using AutoMapper;


namespace Api.Tools.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            AllowNullCollections = true;
            CreateMap<Reaction, ReactionViewModel>();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<Exam, ExamMetadataViewModel>();
            CreateMap<AddQuestionRequest, Question>();
            CreateMap<AddAnswerRequest, Answer>();
            CreateMap<Question, QuestionViewModel>();
            CreateMap<Answer, AnswerViewModel>();
            CreateMap<AddActivityRequest, UserActivity>();
            CreateMap<Faculty, FacultyProfileViewModel>();
            CreateMap<Student, StudentProfileViewModel>().ForMember(dest => dest.Advisor, act => act.Ignore());
            CreateMap<AddCourseRequest, Course>();


        }

    }
}

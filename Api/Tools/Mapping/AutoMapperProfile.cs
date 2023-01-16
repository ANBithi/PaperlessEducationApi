using Api.Models.Post;
using Api.Models.UserInteraction;
using Api.Requests.UserRequests;
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
        }

    }
}

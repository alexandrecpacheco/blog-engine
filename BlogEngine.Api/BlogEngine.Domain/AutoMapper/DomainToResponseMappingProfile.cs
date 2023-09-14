using BlogEngine.Domain.Dto.Response;
using BlogEngine.Domain.Entities;
using Profile = AutoMapper.Profile;

namespace BlogEngine.Domain.AutoMapper
{
    public class DomainToResponseMappingProfile : Profile
    {
        public DomainToResponseMappingProfile()
        {
            CreateMap<PostsEntity, PostsResponse>();
            CreateMap<CommentsEntity, CommentsResponse>();
            CreateMap<SubmitEntity, SubmitResponse>();
            CreateMap<PostsEntity, PostsResponse>()
                .ForMember(p => p.Submit, o => o.MapFrom(q => q.Submit))
                .ForMember(p => p.Comments, o => o.MapFrom(q => q.Comments));
        }
    }
}

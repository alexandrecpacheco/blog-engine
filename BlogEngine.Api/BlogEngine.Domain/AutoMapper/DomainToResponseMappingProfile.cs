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
        }
    }
}

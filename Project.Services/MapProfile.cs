using AutoMapper;
using Common.Dto;
using Project.Repositories.Entities;

namespace Project.Services
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Counselor, CounselorDto>().ReverseMap();
            CreateMap<Topic, TopicDto>().ReverseMap();
            CreateMap<Post, PostDto>().ReverseMap();

            CreateMap<Task<User>, Task<UserDto>>().ReverseMap();
            CreateMap<Task<Counselor>, Task<CounselorDto>>().ReverseMap();
            CreateMap<Task<Topic>, Task<TopicDto>>().ReverseMap();
            CreateMap<Task<Post>, Task<PostDto>>().ReverseMap();

            CreateMap<Task<List<User>>, Task<List<UserDto>>>().ReverseMap();
            CreateMap<Task<List<Counselor>>, Task<List<CounselorDto>>>().ReverseMap();
            CreateMap<Task<List<Topic>>, Task<List<TopicDto>>>().ReverseMap();
            CreateMap<Task<List<Post>>, Task<List<PostDto>>>().ReverseMap();

            CreateMap<Topic, TopicDto>()
              .ForMember(dest => dest.DateLastActive, opt => opt.MapFrom(src => src.DateLastActive))
              .ReverseMap();
        }
    }
}

using AutoMapper;
using TodoApp.Models;
using TodoApp.Models.Dtos;

namespace TodoApp.Server.Config.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.Task, TaskDto>()
                .ForMember(dest => dest.AssigneeName, opt => opt.MapFrom(x => x.Assignee.FullName));

            CreateMap<User, UserDto>();
        }
    }
}

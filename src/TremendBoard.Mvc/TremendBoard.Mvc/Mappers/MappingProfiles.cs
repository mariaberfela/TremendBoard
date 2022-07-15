using AutoMapper;
using System.Collections.Generic;
using TremendBoard.Infrastructure.Services.DTOs;
using TremendBoard.Mvc.Models.ProjectViewModels;
using TremendBoard.Mvc.Models.RoleViewModels;
using TremendBoard.Mvc.Models.UserViewModels;

namespace TremendBoard.Mvc.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserDto, UserDetailViewModel>();
            CreateMap<ProjectUserDto, ProjectUserDetailViewModel>();
            CreateMap<RoleDto, ApplicationRoleDetailViewModel>();
            CreateMap<ProjectDto, ProjectDetailViewModel>();
            CreateMap<ProjectDetailViewModel, ProjectDto>()
                .ForMember(dest => dest.ProjectUsers,
                opt => opt.MapFrom(src => src.ProjectUsers))
                .ForMember(dest => dest.Roles,
                opt => opt.MapFrom(src => src.Roles))
                .ForMember(dest => dest.Users,
                opt => opt.MapFrom(src => src.Users));
            //CreateMap<IList<ProjectUserDto>, IList<ProjectUserDetailViewModel>>();
            //CreateMap<IEnumerable<UserDto>, IEnumerable<UserDetailViewModel>>();
            //CreateMap<IEnumerable<RoleDto>, IEnumerable<ApplicationRoleDetailViewModel>>();
        }
    }
}

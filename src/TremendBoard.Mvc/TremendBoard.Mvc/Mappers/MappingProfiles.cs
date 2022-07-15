using AutoMapper;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.DTOs;
using TremendBoard.Mvc.Models.ProjectViewModels;
using TremendBoard.Mvc.Models.RoleViewModels;
using TremendBoard.Mvc.Models.UserViewModels;

namespace TremendBoard.Mvc.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProjectDetailViewModel, Project>();
            CreateMap<ProjectDetailViewModel, ProjectDTO>();

            CreateMap<ProjectUserDTO, ProjectUserDetailViewModel>();
            CreateMap<UserDTO, UserDetailViewModel>().
                ForMember(user => user.ApplicationRoles, opt => opt.Ignore());
            CreateMap<ApplicationRoleDTO, ApplicationRoleDetailViewModel>();
            CreateMap<ProjectDTO, ProjectDetailViewModel>();
        }
    }
}

using TremendBoard.Infrastructure.Data.Models;
using AutoMapper;
using TremendBoard.Mvc.Models.ProjectViewModels;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Mvc.Models.UserViewModels;
using TremendBoard.Mvc.Models.RoleViewModels;

namespace TremendBoard.Mvc.Mappers
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDetailViewModel>();
            CreateMap<ProjectDetailViewModel, Project>();
            CreateMap<ApplicationUser,UserDetailViewModel>();
            CreateMap<UserDetailViewModel,ApplicationUser>();
            CreateMap<ApplicationRole, ApplicationRoleDetailViewModel>();
            CreateMap<ApplicationRoleDetailViewModel,ApplicationRole>();
        }
       
        
    }
}

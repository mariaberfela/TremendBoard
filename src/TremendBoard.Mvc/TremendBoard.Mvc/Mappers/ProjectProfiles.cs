using AutoMapper;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Mvc.Models.ProjectViewModels;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Mvc.Models.UserViewModels;
using TremendBoard.Mvc.Models.RoleViewModels;

namespace TremendBoard.Mvc.Mappers
{
    public class ProjectProfiles:Profile
    {
        public ProjectProfiles()
        {
            CreateMap<Project, ProjectDetailViewModel>();
            CreateMap<ProjectDetailViewModel, Project>();

            CreateMap<ApplicationUser,UserDetailViewModel>();
            CreateMap<UserDetailViewModel, ApplicationUser>();

            CreateMap<ApplicationRole, ApplicationRoleDetailViewModel>();
            CreateMap<ApplicationRoleDetailViewModel, ApplicationRole>();

        }
    }
}

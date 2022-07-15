using AutoMapper;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.ViewModels;
using TremendBoard.Mvc.Models.ProjectViewModels;
using TremendBoard.Mvc.Models.RoleViewModels;
using TremendBoard.Mvc.Models.UserViewModels;

namespace TremendBoard.Mvc.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDetailViewModel, Project>();
            CreateMap<UserDetailDTO, UserDetailViewModel>();
            CreateMap<ProjectUserDetailDTO, ProjectUserDetailViewModel>();
            CreateMap<ApplicationRoleDetailDTO, ApplicationRoleDetailViewModel>();
            CreateMap<ProjectDetailDTO, ProjectDetailViewModel>();
        }
    }
}

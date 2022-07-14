using AutoMapper;
using TremendBoard.Infrastructure.Data.Models.ViewModels;
using TremendBoard.Mvc.Models.ProjectViewModels;
using TremendBoard.Mvc.Models.RoleViewModels;
using TremendBoard.Mvc.Models.UserViewModels;

namespace TremendBoard.Mvc.Profiles
{
    public class ProjectDetailsProfile : Profile
    {
        public ProjectDetailsProfile()
        {
            CreateMap<ProjectDetailViewModel, ProjectDetailsViewModel>();
        }
    }
}

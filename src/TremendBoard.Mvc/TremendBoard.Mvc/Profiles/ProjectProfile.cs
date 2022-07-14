using AutoMapper;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Mvc.Models.ProjectViewModels;

namespace TremendBoard.Mvc.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDetailViewModel, Project>();
        }
    }
}

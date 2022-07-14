using AutoMapper;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Mvc.Models.ProjectViewModels;

namespace TremendBoard.Mvc.Mappers
{
    public class ProjectDetailViewModelToProject : Profile
    {
        public ProjectDetailViewModelToProject()
        {
            CreateMap<ProjectDetailViewModel, Project>();
        }
    }
}

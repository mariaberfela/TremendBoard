using AutoMapper;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Mvc.Models.ProjectViewModels;

namespace TremendBoard.Mvc.Mapper
{
    public class ProjectDetailViewModel_Project : Profile
    {
        public ProjectDetailViewModel_Project()
        {
            CreateMap<ProjectDetailViewModel, Project>();
        }
    }
}

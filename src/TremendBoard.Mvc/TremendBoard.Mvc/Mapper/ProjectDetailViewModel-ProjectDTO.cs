using AutoMapper;
using TremendBoard.Infrastructure.Data.Models.DTO;
using TremendBoard.Mvc.Models.ProjectViewModels;

namespace TremendBoard.Mvc.Mapper
{
    public class ProjectDetailViewModel_ProjectDTO : Profile
    {
        public ProjectDetailViewModel_ProjectDTO()
        {
            CreateMap<ProjectDetailViewModel, ProjectDTO>();
        }
    }
}

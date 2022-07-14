using AutoMapper;
using TremendBoard.Infrastructure.Data.Models.DTOs;
using TremendBoard.Mvc.Models.ProjectViewModels;

namespace TremendBoard.Mvc.Mappers
{
    public class ProjectDetailViewModelToProjectDTO : Profile
    {
        public ProjectDetailViewModelToProjectDTO()
        {
            CreateMap<ProjectDetailViewModel, ProjectDTO>();
        }
    }
}

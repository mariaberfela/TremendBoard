using TremendBoard.Infrastructure.Services.DTOs;
using TremendBoard.Mvc.Models.ProjectViewModels;

namespace TremendBoard.Mvc.Mappers
{
    public interface IMapViewModelToDto
    {
        ProjectDto ProjectViewModelToProjectDto(ProjectDetailViewModel projectDetailViewModel);

    }
}

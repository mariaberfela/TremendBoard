using TremendBoard.Infrastructure.Services.DTOs;
using TremendBoard.Mvc.Models.ProjectViewModels;
using TremendBoard.Mvc.Models.RoleViewModels;
using TremendBoard.Mvc.Models.UserViewModels;

namespace TremendBoard.Mvc.Mappers
{
    public interface IMapDtoToViewModel
    {
        ProjectDetailViewModel ProjectDtoToViewModel(ProjectDto projectDto);
        ProjectDetailViewModel IncompleteProjectDtoToViewModel(ProjectDto projectDto);
        ProjectUserDetailViewModel ProjectUserDtoToViewModel(ProjectUserDto projectUserDto);
        ApplicationRoleDetailViewModel RoleToViewModel(RoleDto roleDto);
        UserDetailViewModel UserDtoToViewModel(UserDto userDto);
    }
}

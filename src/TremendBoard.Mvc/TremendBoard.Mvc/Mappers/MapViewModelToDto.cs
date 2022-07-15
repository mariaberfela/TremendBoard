using AutoMapper;
using System.Collections.Generic;
using TremendBoard.Infrastructure.Services.DTOs;
using TremendBoard.Mvc.Models.ProjectViewModels;

namespace TremendBoard.Mvc.Mappers
{
    public class MapViewModelToDto : IMapViewModelToDto
    {
        private readonly IMapper _mapper;
        public MapViewModelToDto(IMapper mapper)
        {
            _mapper = mapper;
        }
    
        public ProjectDto ProjectViewModelToProjectDto(ProjectDetailViewModel projectDetailViewModel)
        {

            //var projectUsers = new List<ProjectUserDto>();
            //var users = new List<UserDto>();
            //var roles = new List<RoleDto>();

            //foreach (var projectUser in projectDetailViewModel.ProjectUsers)
            //{
            //    var projectUserAux = new ProjectUserDto
            //    {
            //        ProjectId = projectUser.ProjectId,
            //        UserId = projectUser.UserId,
            //        RoleId = projectUser.RoleId,
            //        FirstName = projectUser.FirstName,
            //        LastName = projectUser.LastName,
            //        UserRoleName = projectUser.UserRoleName
            //    };

            //    projectUsers.Add(projectUserAux);
            //}

            //foreach (var user in projectDetailViewModel.Users)
            //{
            //    var userAux = new UserDto
            //    {
            //        Id = user.Id,
            //        ApplicationRoles = user.ApplicationRoles,
            //        UserRoleId = user.UserRoleId,
            //        CurrentUserRole = user.CurrentUserRole,
            //        FirstName = user.FirstName,
            //        LastName = user.LastName,
            //        Email = user.Email,
            //        PhoneNumber = user.PhoneNumber
            //    };
            //    users.Add(userAux);
            //}

            //foreach (var role in projectDetailViewModel.Roles)
            //{
            //    var roleAux = new RoleDto
            //    {
            //        Id = role.Id,
            //        RoleName = role.RoleName,
            //        Description = role.Description,
            //        UserRoleName = role.UserRoleName,
            //        StatusMessage = role.StatusMessage
            //    };

            //    roles.Add(roleAux);
            //}

            //var viewModel = new ProjectDto
            //{
            //    Id = projectDetailViewModel.Id,
            //    Name = projectDetailViewModel.Name,
            //    Description = projectDetailViewModel.Description,
            //    ProjectStatus = projectDetailViewModel.ProjectStatus,
            //    Deadline = projectDetailViewModel.Deadline,
            //    ProjectUsers = projectUsers,
            //    Users = users,
            //    Roles = roles
            //};

            //return viewModel;
            return _mapper.Map<ProjectDto>(projectDetailViewModel);
        }
    }
}

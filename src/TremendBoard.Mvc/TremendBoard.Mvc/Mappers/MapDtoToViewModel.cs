using AutoMapper;
using System.Collections.Generic;
using TremendBoard.Infrastructure.Services.DTOs;
using TremendBoard.Mvc.Models.ProjectViewModels;
using TremendBoard.Mvc.Models.RoleViewModels;
using TremendBoard.Mvc.Models.UserViewModels;

namespace TremendBoard.Mvc.Mappers
{
    public class MapDtoToViewModel : IMapDtoToViewModel
    {
        private readonly IMapper _mapper;
        public MapDtoToViewModel(IMapper mapper)
        {
            _mapper = mapper;

        }

        public ProjectDetailViewModel IncompleteProjectDtoToViewModel(ProjectDto projectDto)
        {
            return _mapper.Map<ProjectDetailViewModel>(projectDto);
        }

        public ProjectDetailViewModel ProjectDtoToViewModel(ProjectDto projectDto)
        {
            
            var projectUsers = new List<ProjectUserDetailViewModel>();
            var users = new List<UserDetailViewModel>();
            var roles = new List<ApplicationRoleDetailViewModel>();

            foreach(var projectUser in projectDto.ProjectUsers)
            {
                var projectUserAux = new ProjectUserDetailViewModel
                { 
                    ProjectId = projectUser.ProjectId,
                    UserId = projectUser.UserId,
                    RoleId = projectUser.RoleId,
                    FirstName = projectUser.FirstName,
                    LastName = projectUser.LastName,
                    UserRoleName = projectUser.UserRoleName
                };

                projectUsers.Add(projectUserAux);
            }

            foreach(var user in projectDto.Users)
            {
                var userAux = new UserDetailViewModel
                {
                    Id = user.Id,
                    ApplicationRoles = user.ApplicationRoles,
                    UserRoleId = user.UserRoleId,
                    CurrentUserRole = user.CurrentUserRole,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };
                users.Add(userAux);
            }

            foreach(var role in projectDto.Roles)
            {
                var roleAux = new ApplicationRoleDetailViewModel
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    Description = role.Description,
                    UserRoleName = role.UserRoleName,
                    StatusMessage = role.StatusMessage
                };

                roles.Add(roleAux);
            }

            var viewModel = new ProjectDetailViewModel
            {
                Id = projectDto.Id,
                Name = projectDto.Name,
                Description = projectDto.Description,
                ProjectStatus = projectDto.ProjectStatus,
                Deadline = projectDto.Deadline,
                ProjectUsers = projectUsers,
                Users = users,
                Roles = roles
            };

            return viewModel;

        }

        public ProjectUserDetailViewModel ProjectUserDtoToViewModel(ProjectUserDto projectUserDto)
        {
            return _mapper.Map<ProjectUserDetailViewModel>(projectUserDto);
        }

        public ApplicationRoleDetailViewModel RoleToViewModel(RoleDto roleDto)
        {
            return _mapper.Map<ApplicationRoleDetailViewModel>(roleDto); 
        }

        public UserDetailViewModel UserDtoToViewModel(UserDto userDto)
        {
            return _mapper.Map<UserDetailViewModel>(userDto);
        }
    }
}

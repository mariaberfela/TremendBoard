using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Enums;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.DTOs;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class ProjectService: IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Project project)
        {
            project.CreatedDate = DateTime.Now;

            await _unitOfWork.Project.AddAsync(project);

            await _unitOfWork.SaveAsync();
        }

        public async Task<ProjectDTO> Edit(string id)
        {
            var project = await _unitOfWork.Project.GetByIdAsync(id);

            if (project == null)
            {
                throw new ApplicationException($"Unable to load project with ID '{id}'.");
            }

            var users = await _unitOfWork.User.GetAllAsync();
            var usersView = users.Select(user => new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });

            var roles = await _unitOfWork.Role.GetAllAsync();
            var rolesView = roles
                .Where(x => x.Name != Role.Admin.ToString())
                .OrderBy(x => x.Name)
                .Select(r => new ApplicationRoleDTO
                {
                    Id = r.Id,
                    RoleName = r.Name,
                    Description = r.Description
                });

            var model = new ProjectDTO
            {
                Id = id,
                Name = project.Name,
                Description = project.Description,
                ProjectStatus = project.ProjectStatus,
                Deadline = project.Deadline,
                ProjectUsers = new List<ProjectUserDTO>(),
                Users = usersView,
                Roles = rolesView
            };

            var userRoles = _unitOfWork.Project.GetProjectUserRoles(id);

            foreach (var userRole in userRoles)
            {
                var user = users.FirstOrDefault(x => x.Id == userRole.UserId);
                var role = roles.FirstOrDefault(x => x.Id == userRole.RoleId);

                var projectUser = new ProjectUserDTO
                {
                    ProjectId = id,
                    UserId = userRole.UserId,
                    RoleId = userRole.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserRoleName = role.Name
                };

                model.ProjectUsers.Add(projectUser);
            }

            return model;
        }

        public async Task<ProjectDTO> Edit(ProjectDTO model, Project project)
        {
            project.Name = model.Name;
            project.Description = model.Description;
            project.ProjectStatus = model.ProjectStatus;
            project.Deadline = model.Deadline;

            var users = await _unitOfWork.User.GetAllAsync();
            var usersView = users.Select(user => new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });

            var roles = await _unitOfWork.Role.GetAllAsync();
            var rolesView = roles
                .Where(x => x.Name != Role.Admin.ToString())
                .OrderBy(x => x.Name)
                .Select(r => new ApplicationRoleDTO
                {
                    Id = r.Id,
                    RoleName = r.Name,
                    Description = r.Description
                });

            model.Roles = rolesView;
            model.Users = usersView;

            var userRoles = _unitOfWork.Project.GetProjectUserRoles(project.Id);

            model.ProjectUsers = new List<ProjectUserDTO>();

            foreach (var userRole in userRoles)
            {
                var user = users.FirstOrDefault(x => x.Id == userRole.UserId);
                var role = roles.FirstOrDefault(x => x.Id == userRole.RoleId);
                var projectUser = new ProjectUserDTO
                {
                    ProjectId = project.Id,
                    UserId = userRole.UserId,
                    RoleId = userRole.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserRoleName = role.Name
                };

                model.ProjectUsers.Add(projectUser);
            }

            _unitOfWork.Project.Update(project);
            await _unitOfWork.SaveAsync();

            model.StatusMessage = $"{project.Name} project has been updated";

            return model;
        }
    }
}

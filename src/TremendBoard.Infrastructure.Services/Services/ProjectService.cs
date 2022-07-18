using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.DTOs;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Enums;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(ProjectDto project)
        {

            var p = new Project
            {
                Name = project.Name,
                Description = project.Description,
                CreatedDate = DateTime.Now,
                Deadline = project.Deadline,
                ProjectStatus = project.ProjectStatus
            };

            await _unitOfWork.Project.AddAsync(p);

            await _unitOfWork.SaveAsync();

        }

        public async Task<ProjectDto> Edit(string id)
        {
            var project = await _unitOfWork.Project.GetByIdAsync(id);

            if (project == null)
            {
                throw new ApplicationException($"Unable to load project with ID '{id}'.");
            }

            var users = await _unitOfWork.User.GetAllAsync();
            var usersView = users.Select(user => new UserDto
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
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    RoleName = r.Name,
                    Description = r.Description
                });

            var model = new ProjectDto
            {
                Id = id,
                Name = project.Name,
                Description = project.Description,
                Deadline = project.Deadline,
                ProjectStatus = project.ProjectStatus,
                ProjectUsers = new List<ProjectUserDto>(),
                Users = usersView,
                Roles = rolesView
            };

            var userRoles = _unitOfWork.Project.GetProjectUserRoles(id);

            foreach (var userRole in userRoles)
            {
                var user = users.FirstOrDefault(x => x.Id == userRole.UserId);
                var role = roles.FirstOrDefault(x => x.Id == userRole.RoleId);

                var projectUser = new ProjectUserDto
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

        public async Task Edit(ProjectDto model)
        {
            var projectId = model.Id;
            var project = await _unitOfWork.Project.GetByIdAsync(projectId);

            //if (project == null)
            //{
            //    ModelState.AddModelError("Error", "Unable to load the project");
            //    return View(model);
            //}

            project.Name = model.Name;
            project.Description = model.Description;
            project.Deadline = model.Deadline;
            project.ProjectStatus = model.ProjectStatus;

            var users = await _unitOfWork.User.GetAllAsync();
            var usersView = users.Select(user => new UserDto
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
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    RoleName = r.Name,
                    Description = r.Description
                });

            model.Roles = rolesView;
            model.Users = usersView;

            var userRoles = _unitOfWork.Project.GetProjectUserRoles(project.Id);

            model.ProjectUsers = new List<ProjectUserDto>();

            foreach (var userRole in userRoles)
            {
                var user = users.FirstOrDefault(x => x.Id == userRole.UserId);
                var role = roles.FirstOrDefault(x => x.Id == userRole.RoleId);
                var projectUser = new ProjectUserDto
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
            
        }
    }
}

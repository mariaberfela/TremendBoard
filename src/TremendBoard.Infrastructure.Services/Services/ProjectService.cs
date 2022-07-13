using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Entities;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Enums;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class ProjectService: IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Project _project;
        public ProjectService(IUnitOfWork unitOfWork, Project project)
        {
            _unitOfWork = unitOfWork;
            _project = project; 
        }

        public async Task Create()
        {
            await _unitOfWork.Project.AddAsync(new Project
            {
                Name = _project.Name,
                Description = _project.Description,
                CreatedDate = DateTime.Now,
                ProjectStatus = _project.ProjectStatus,
                ProjectDeadline = _project.ProjectDeadline
            });

            await _unitOfWork.SaveAsync();
        }

        public async Task<ProjectDetail> Edit(string id)
        {
            var project = await _unitOfWork.Project.GetByIdAsync(id);

            if (project == null)
            {
                throw new ApplicationException($"Unable to load project with ID '{id}'.");

            }

            var users = await _unitOfWork.User.GetAllAsync();
            var usersView = users.Select(user => new UserDetail
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
                .Select(r => new ApplicationRoleDetail
                {
                    Id = r.Id,
                    RoleName = r.Name,
                    Description = r.Description
                });

            var model = new ProjectDetail
            {
                Id = id,
                Name = project.Name,
                Description = project.Description,
                ProjectUsers = new List<ProjectUserDetail>(),
                Users = usersView,
                Roles = rolesView,
                ProjectStatus = project.ProjectStatus,
                ProjectDeadline = project.ProjectDeadline
            };

            var userRoles = _unitOfWork.Project.GetProjectUserRoles(id);

            foreach (var userRole in userRoles)
            {
                var user = users.FirstOrDefault(x => x.Id == userRole.UserId);
                var role = roles.FirstOrDefault(x => x.Id == userRole.RoleId);

                var projectUser = new ProjectUserDetail
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
        private enum Role
        {
            Admin
        }
    }
   
}

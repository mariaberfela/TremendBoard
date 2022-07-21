using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.DTOs;
using TremendBoard.Infrastructure.Data.Enums;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateProject(ProjectDTO project)
        {
            await _unitOfWork.Project.AddAsync(new Project
            {
                Name = project.Name,
                Description = project.Description,
                CreatedDate = DateTime.Now,
                ProjectStatus = project.ProjectStatus,
                Deadline = project.Deadline
            });
            await _unitOfWork.SaveAsync();
            return true;
        }
        public async Task EditProject(string id)
        {
            
        }
        public async Task<ProjectDetailDTO> UpdateProject(ProjectDetailDTO model)
        {
            var projectId = model.Id;
            Project project = await _unitOfWork.Project.GetByIdAsync(projectId);
            
            project.Name = model.Name;
            project.Description = model.Description;

            var users = await _unitOfWork.User.GetAllAsync();
            var usersView = users.Select(user => new UserDetailDTO
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
                .Select(r => new RoleDetailViewDTO
                {
                    Id = r.Id,
                    RoleName = r.Name,
                    Description = r.Description
                });

            model.Roles = rolesView;
            model.Users = usersView;

            var userRoles = _unitOfWork.Project.GetProjectUserRoles(project.Id);

            model.ProjectUsers = new List<ProjectUserDetailViewDTO>();

            foreach (var userRole in userRoles)
            {
                var user = users.FirstOrDefault(x => x.Id == userRole.UserId);
                var role = roles.FirstOrDefault(x => x.Id == userRole.RoleId);
                var projectUser = new ProjectUserDetailViewDTO
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
            return model;
        }
    }
}

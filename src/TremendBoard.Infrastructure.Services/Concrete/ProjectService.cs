using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.DTOs;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Concrete
{
    public class ProjectService: IProjectService
    {

        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(ProjectDetailDTO model)
        {
            await _unitOfWork.Project.AddAsync(new Project
            {
                Name = model.Name,
                Description = model.Description,
                CreatedDate = DateTime.Now,
                ProjectStatus = model.ProjectStatus,
                Deadline = model.Deadline
            });

            await _unitOfWork.SaveAsync();
        }

        public Task<Project> GetByIdAsync(string id)
        {
            return _unitOfWork.Project.GetByIdAsync(id);
        }

        public IEnumerable<ApplicationUserRole> GetProjectUserRoles(string projectId)
        {
            return _unitOfWork.Project.GetProjectUserRoles(projectId);
        }

        public IEnumerable<ProjectUserDetailDTO> GetProjectUserDetails(string id, IEnumerable<ApplicationUserRole> userRoles, IEnumerable<ApplicationUser> users, IEnumerable<ApplicationRole> roles)
        {
            return userRoles
                .Select(userRole =>
                    {
                        var user = users.FirstOrDefault(x => x.Id == userRole.UserId);
                        var role = roles.FirstOrDefault(x => x.Id == userRole.RoleId);
                        return new ProjectUserDetailDTO(id, userRole, user, role);
                    });
        }

        public Project UpdateProjectFields(Project dest, ProjectDetailDTO src)
        {
            dest.Name = src.Name;
            dest.Description = src.Description;
            dest.ProjectStatus = src.ProjectStatus;
            dest.Deadline = src.Deadline;

            return dest;
        }

        public async Task<string> Update(Project project)
        {
            _unitOfWork.Project.Update(project);
            await _unitOfWork.SaveAsync();

            return $"{project.Name} project has been updated";
        }
    }
}

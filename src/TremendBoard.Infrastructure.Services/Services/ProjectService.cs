using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Interfaces; 
using TremendBoard.Infrastructure.Data.Models.Identity;
using System.Collections;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
       
        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _unitOfWork.Project.GetAllAsync();
        }

        public async Task CreateProject(Project project)
        {
         
            project.CreatedDate = DateTime.Now;
            await _unitOfWork.Project.AddAsync(project);

            await _unitOfWork.SaveAsync();

        }

        public async Task<IEnumerable<ApplicationRole>> GetAllAplicationRoles()
        {
            return await _unitOfWork.Role.GetAllAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _unitOfWork.User.GetAllAsync();
        }

        public async Task<Project> GetProjectById(string id)
        {

            return await _unitOfWork.Project.GetByIdAsync(id);    
        }

        public IEnumerable<ApplicationUserRole> GetApplicationUserRolesForProject(string projectId)
        {
            return  _unitOfWork.Project.GetProjectUserRoles(projectId);
        }

        public async Task UpdateProject(Project project)
        {
            _unitOfWork.Project.Update(project);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddUserRole(ApplicationUserRole userRole)
        {
            await _unitOfWork.UserRole.AddAsync(userRole);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveProject(Project project)
        {
            _unitOfWork.Project.Remove(project);
            await _unitOfWork.SaveAsync();
        }
    }


}


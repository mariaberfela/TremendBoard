using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;
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

        public async Task<IEnumerable<Project>> GetProjects()
        {
            var projects = await _unitOfWork.Project.GetAllAsync();
            return projects;
        }
        public async Task<Project> GetProjectById(string id)
        {
            var project = await _unitOfWork.Project.GetByIdAsync(id);
            if(project == null )
            {
                throw new ApplicationException($"Cannot retrieve the project for id {id}");
            }
            return project;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            var users = await _unitOfWork.User.GetAllAsync();
            return users;
        }
        public IEnumerable<ApplicationUserRole> GetAllApplicationRolesForProject(string projectId)
        {
            return _unitOfWork.Project.GetProjectUserRoles(projectId);
           
        }
        public async Task<IEnumerable<ApplicationRole>> GetAllAplicationRoles()
        {
            return await _unitOfWork.Role.GetAllAsync();
        }
        public async Task CreateProject (Project project)
        {
            project.CreatedDate = DateTime.Now;
            await _unitOfWork.Project.AddAsync(project);
            await _unitOfWork.SaveAsync();
        }
        public void UpdateProject(Project project)
        {
            _unitOfWork.Project.Update(project);
        }

        public async Task SaveProject()
        {
            await _unitOfWork.SaveAsync();
        }
        /*
        */

    }
}

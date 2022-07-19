using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IProjectService
    {
        /// <summary>
        /// Gets all the projects
        /// </summary>
        /// <returns>Returns the collection of projects</returns>
        Task <IEnumerable<Project>> GetProjects();
 
        /// <summary>
        /// Create a project 
        /// </summary>
        /// <param name="project"></param>
        /// <returns>Return and save the project created</returns>
        Task CreateProject(Project project);

        /// <summary>
        /// Gets project by id  
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>Returns the project</returns>
        Task <Project> GetProjectById(string id);

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Returns the collection of users</returns>
        Task <IEnumerable<ApplicationUser>> GetAllUsers();
        
        /// <summary>
        /// Gets all user's roles
        /// </summary>
        /// <returns>Returns the collection of aplication's roles</returns>
        Task <IEnumerable<ApplicationRole>> GetAllAplicationRoles();

        /// <summary>
        /// Gets application user roles
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns project user's role by id</returns>
        IEnumerable<ApplicationUserRole> GetApplicationUserRolesForProject(string projectId);

        /// <summary>
        /// Updates project with the modifications made
        /// </summary>
        /// <param name="project"></param>
        Task UpdateProject(Project project);

        Task AddUserRole(ApplicationUserRole userRole);

        Task RemoveProject(Project project);
    }
}

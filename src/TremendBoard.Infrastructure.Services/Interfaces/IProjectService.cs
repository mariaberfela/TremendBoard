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
        /// Gets projects from the database
        /// </summary>
        /// <returns>Return collection of projects/returns>
        Task<IEnumerable<Project>> GetProjects();
        
        /// <summary>
        /// Gets Project by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>project by id</returns>
        Task<Project> GetProjectById(string id);
        
        /// <summary>
        /// Create a project
        /// </summary>
        /// <param name="project"> </param>
        /// <returns></returns>
        Task CreateProject(Project project);

        /// <summary>
        /// gets all users
        /// </summary>
        /// <returns>a collection of users</returns>
        Task<IEnumerable<ApplicationUser>> GetAllUsers();

        /// <summary>
        /// Gets all users roles
        /// </summary>
        /// <returns> a collection of roles</returns>
        Task<IEnumerable<ApplicationRole>> GetAllAplicationRoles();

        /// <summary>
        /// gets application users roles for project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>a collection of application user roles </returns>
        IEnumerable<ApplicationUserRole> GetAllApplicationRolesForProject(string projectId);

        /// <summary>
        /// update project
        /// </summary>
        /// <param name="project"></param>
        void UpdateProject(Project project);

        /// <summary>
        /// save the project
        /// </summary>
        /// <returns>the saved project</returns>
        Task SaveProject();
    }
}

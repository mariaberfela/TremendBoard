using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.DTOs;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IProjectService
    {
        public Task AddAsync(ProjectDetailDTO projectDetailDTO);
        public Task<Project> GetByIdAsync(string id);
        public IEnumerable<ApplicationUserRole> GetProjectUserRoles(string projectId);
        public IEnumerable<ProjectUserDetailDTO> GetProjectUserDetails(string id, IEnumerable<ApplicationUserRole> userRoles, IEnumerable<ApplicationUser> users, IEnumerable<ApplicationRole> roles);
        public Project UpdateProjectFields(Project dest, ProjectDetailDTO src);
        public Task<string> Update(Project project);
    
    }
}

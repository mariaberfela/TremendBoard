using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;

namespace TremendBoard.Infrastructure.Services.Interfaces;

public interface IProjectService
{
    public Task AddAsync(Project project);
    
    public Task<Project> GetAsync(string id);
   
    public IEnumerable<ApplicationUserRole> GetProjectUserRoles(string projectId);
         
    public Task Update(Project project);
}

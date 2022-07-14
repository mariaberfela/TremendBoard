using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.DTO;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IProjectService
    {
        Task Create(Project project);
        Task<ProjectDTO> Edit(string id);
        Task<ProjectDTO> Edit(ProjectDTO model, Project project);
        
    }
}

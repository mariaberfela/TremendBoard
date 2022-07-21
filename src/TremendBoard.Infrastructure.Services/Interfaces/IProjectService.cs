using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.DTOs;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IProjectService
    {
        public async Task<bool> CreateProject(ProjectDTO project)
        {
            return true;
        }

        public async Task EditProject(string id)
        {
            
        }
        public async Task<ProjectDetailDTO> UpdateProject(ProjectDetailDTO model) 
        { 

            return model; 
        }  
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.ViewModels;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IProjectService
    {
        Task Create(Project project);
        Task<ProjectDetailsViewModel> Edit(string id);
        Task<ProjectDetailsViewModel> Edit(ProjectDetailsViewModel model, Project project);
    }
}

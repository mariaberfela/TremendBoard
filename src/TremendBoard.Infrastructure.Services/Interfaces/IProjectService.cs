using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IProjectService
    {
        Task Create(Project project);
        Task<ProjectDetails> Edit(string id);
        Task<ProjectDetails> Edit(ProjectDetails model);
    }
}

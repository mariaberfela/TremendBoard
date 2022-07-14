using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IProjectService : IGenericRepository<Project>
    {
        //public IEnumerable<Project> GetProjects();
        //Task<IEnumerable<Project>> GetProjects();
        Task<int> SaveAsync();
        //Task<IEnumerable<Project>> GetP;rojects()
        Task<IEnumerable<Project>> GetProjects();


    }
}

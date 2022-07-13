using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Entities;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IProjectService
    {
        public Task Create();
        public Task<ProjectDetail> Edit(string Id);
    }
}

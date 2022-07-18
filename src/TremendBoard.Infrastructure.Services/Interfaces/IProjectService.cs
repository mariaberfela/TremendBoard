using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.DTOs;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IProjectService
    {
        Task Create(ProjectDto project);
        Task<ProjectDto> Edit(string id);
        Task Edit(ProjectDto project);
    }
}

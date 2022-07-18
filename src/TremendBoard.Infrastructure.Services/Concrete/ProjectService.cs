using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Concrete;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(Project project)
    {
        await _unitOfWork.Project.AddAsync(project);
        await _unitOfWork.SaveAsync();
    }

    public Task<Project> GetAsync(string id)
    {
        return _unitOfWork.Project.GetByIdAsync(id);
    }

    public IEnumerable<ApplicationUserRole> GetProjectUserRoles(string projectId)
    {
        return _unitOfWork.Project.GetProjectUserRoles(projectId);
    }

    public async Task Update(Project project)
    {
        _unitOfWork.Project.Update(project);
        await _unitOfWork.SaveAsync();
    }
}

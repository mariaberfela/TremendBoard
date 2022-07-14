using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Context;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Concrete;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class ProjectService : GenericRepository<Project>, IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TremendBoardDbContext _context;
        //private readonly ProjectViewModel Projects;
        public ProjectService(TremendBoardDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            
        }
        public async Task<IEnumerable<Project>> GetProjects()
        {
            
            var projects = await _unitOfWork.Project.GetAllAsync();
            return projects;

        }
        //public async Task<IEnumerable<User>> GetUsers()
        //{

        //    var projects = await _unitOfWork.Project.GetAllAsync();
        //    return projects;

        //}

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        
    }
}

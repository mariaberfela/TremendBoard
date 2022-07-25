using System;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class JobTestService: IJobTestService
    {
        private readonly IUnitOfWork _unitOfWork;
        public IProjectService _projectService;
        public JobTestService(IUnitOfWork unitOfWork, IProjectService projectService)
        {
            _unitOfWork = unitOfWork;
            _projectService = projectService;
        }

        public void FireAndForgetJob()
        {
            Console.WriteLine("Hello from a Fire and Forget job!");
        }
        public void ReccuringJob()
        {
            var successful = _projectService.CreateProject(new Project
            {
                Name = "Proiect de task2",
                Description = "Acesta e un proiect facut cu un job reccuring",
                CreatedDate = DateTime.Now,
                ProjectStatus = "done",
                Deadline = DateTime.Parse("10/01/2022")
            }).Result;
            Console.WriteLine("Hello from a Scheduled job!");

        }
        public void DelayedJob()
        {
            Console.WriteLine("Hello from a Delayed job!");
        }
        public void ContinuationJob()
        {
            Console.WriteLine("Hello from a Continuation job!");
        }
    }
}

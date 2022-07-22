using System;
using TremendBoard.Infrastructure.Data.DTOs;
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
            var successful = _projectService.CreateProject(new ProjectDTO
            {
                Name ="Proiect de task",
                Description = "Acesta e un proiect facut cu un job fire and forget",
                CreatedDate = DateTime.Now,
                ProjectStatus = "done",
                Deadline = DateTime.Parse("10/01/2022")
            }).Result;
            Console.WriteLine("Hello from a FireAndForget job!");
        }
        public void ReccuringJob()
        {

            var successful = _projectService.CreateProject(new ProjectDTO
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
            var successful = _projectService.CreateProject(new ProjectDTO
            {
                Name = "Proiect de task3",
                Description = "Acesta e un proiect facut cu un job delayed",
                CreatedDate = DateTime.Now,
                ProjectStatus = "done",
                Deadline = DateTime.Parse("10/01/2022")
            }).Result;
            Console.WriteLine("Hello from a Delayed job!");
        }
        public void ContinuationJob()
        {
            var successful = _projectService.CreateProject(new ProjectDTO
            {
                Name = "Proiect de task4",
                Description = "Acesta e un proiect facut cu un job continuation",
                CreatedDate = DateTime.Now,
                ProjectStatus = "done",
                Deadline = DateTime.Parse("10/01/2022")
            }).Result;
            Console.WriteLine("Hello from a Continuation job!");
        }
    }
}

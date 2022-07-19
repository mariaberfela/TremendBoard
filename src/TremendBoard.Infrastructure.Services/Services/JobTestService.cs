using System;
using System.Collections.Generic;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class JobTestService: IJobTestService
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobTestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void FireAndForgetJob()
        {
            Console.WriteLine("Hello from a Fire and Forget job!");
        }
        public void RecurringJob()
        {
            Project projectDetails = new()
            {
                Name = "TestName",
                Description = "Test description",
                CreatedDate = DateTime.Now,
                CompletedDate = DateTime.Now,
                ProjectStatus = "Done",
                Deadline = DateTime.Now,
                UserRoles = new List<ApplicationUserRole>(),
                Tasks = new List<ProjectTask>()

            };
            _unitOfWork.Project.AddAsync(projectDetails);
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

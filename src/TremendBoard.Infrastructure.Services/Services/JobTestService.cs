using System;
using TremendBoard.Infrastructure.Data.Models;
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
        public void ReccuringJob()
        {
            Console.WriteLine("Hello from a Scheduled job!");

        }
        public void DelayedJob()
        {
            Console.WriteLine("Hello from a Delayed job!");
            Project project = new Project
            {
                Name = "Delayed Test",
                Description = "Delayed job test.",
                CreatedDate = DateTime.Now,
                ProjectStatus = "Status",
                Deadline = DateTime.Now.AddDays(1),
            };

            _unitOfWork.Project.AddAsync(project) ;
        }
        public void ContinuationJob()
        {
            Console.WriteLine("Hello from a Continuation job!");
        }
    }
}

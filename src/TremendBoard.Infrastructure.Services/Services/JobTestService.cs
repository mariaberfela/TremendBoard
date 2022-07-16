using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        }
        public void ContinuationJob()
        {
            Console.WriteLine("Hello from a Continuation job!");
        }

        public void CheckProjectsDeadline()
        {
            var projects = _unitOfWork.Project.GetAllAsync().Result;
            bool isAnyDeadlineExcedeed = false;
            if (projects == null)
            {
                Debug.WriteLine("There is no project to check!");
            }
            else
            {
                foreach (var project in projects)
                {
                    if (project.Deadline > DateTime.Now)
                    {
                        Debug.WriteLine("Project deadline excedeed: " + project.Name);
                        isAnyDeadlineExcedeed = true;
                    }
                }
            }
            if (!isAnyDeadlineExcedeed)
            {
                Debug.WriteLine("Deadline not reached for any project. That's great!");
            }
        }
    }
}

using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.HostedServices
{
    internal class CheckProjectsBackgroundService : BackgroundService
    {
        private const int MS = 1000;
        private const int SEC = 1 * MS;
        private const int MIN = 60 * SEC;
        private const int INTERVAL_MIN = 60 * MIN;

        private readonly IProjectRepository _projectRepository;

        public CheckProjectsBackgroundService(
            IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Run until cancellation is requested.
            while (!stoppingToken.IsCancellationRequested)
            {
                var projects = await _projectRepository.GetAllAsync();
                if (projects.Count() > 0)
                {
                    for (int i = 0; i < projects.Count(); i++)
                    {
                        if (projects.ElementAt(i).Deadline > DateTime.Now)
                        {
                            Console.WriteLine("Project deadline excedeed: " + projects.ElementAt(i).Name);
                        }
                    }
                }

                // Wait X time then repeat the execution
                await Task.Delay(INTERVAL_MIN);
            }
        }
    }
}

using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Mvc.Controllers
{
    public class JobTestController : Controller
    {
        private readonly IJobTestService _jobTestService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;

        public JobTestController(IJobTestService jobTestService, 
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager)
        {
            _jobTestService = jobTestService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }

        [HttpGet("/CreateAndCompareJobs")]
        public ActionResult CreateAndCompareJobs()
        {
            _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
            RecurringJob.AddOrUpdate("recurring job", () => _jobTestService.ReccuringJob(), "*/1 * * * *");
            var delayedJobId = _backgroundJobClient.Schedule(() => _jobTestService.DelayedJob(), TimeSpan.FromMinutes(1));
            BackgroundJob.ContinueJobWith(delayedJobId, () => _jobTestService.ContinuationJob());
            return Ok();
        }
    }
}

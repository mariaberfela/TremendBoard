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

        [HttpGet("/FireAndForgetJob")]
        public ActionResult CreateFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
            return Ok();
        }

        [HttpGet("/DelayedJob")]
        public ActionResult DelayedJob()
        {
            _backgroundJobClient.Schedule(() =>
                _jobTestService.DelayedJob(), 
                TimeSpan.FromSeconds(10));
            return Ok();
        }
        [HttpGet("/RecurringJob")]
        public ActionResult CreateRecurringJob()
        {
            RecurringJob.AddOrUpdate(() => 
                _jobTestService.ReccuringJob(), 
                Cron.Daily);
            return Ok();
        }

    }
}

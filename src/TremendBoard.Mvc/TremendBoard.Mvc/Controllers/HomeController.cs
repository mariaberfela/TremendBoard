using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Models;

namespace TremendBoard.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDateTime _dateTime;
        private readonly ITimeService _timeService1;
        private readonly ITimeService _timeService2;
        private readonly ILogger<HomeController> _logger;


        public HomeController(IDateTime dateTime, ITimeService ts1, ITimeService ts2, ILogger<HomeController> logger)
        {
            _dateTime = dateTime;
            _logger = logger;
            _timeService1 = ts1;
            _timeService2 = ts2;
        }

        public IActionResult Index()
        {
            ViewData["timeService1"] = _timeService1.GetCurrentTime();
            ViewData["timeService2"] = _timeService2.GetCurrentTime();

            var serverTime = _dateTime.Now;
            try
            {
                if (serverTime.Hour < 12)
                {
                    ViewData["Message"] = "It's morning here - Good Morning!";
                    _logger.LogInformation("The server says it's morning");
                }
                else if (serverTime.Hour < 17)
                {
                    ViewData["Message"] = "It's afternoon here - Good Afternoon!";
                    _logger.LogInformation("The server says it's afternoon");
                }
                else if (serverTime.Hour < 24)
                {
                    ViewData["Message"] = "It's evening here - Good Evening!";
                    _logger.LogWarning("In evening time no developer is working here");
                }
                else if (serverTime.Hour == null)
                {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("The server didn't provide a valid time");
            }

            return View();
        }

        public IActionResult About([FromServices] IDateTime dateTime)
        {
            ViewData["Message"] = "Currently on the server the time is " + dateTime.Now;

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

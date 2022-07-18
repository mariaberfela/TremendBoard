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
        private readonly ILogger<HomeController> _logger;
        private readonly IDateTime _dateTime;
        //private readonly ITimeService _timeService1;
        //private readonly ITimeService _timeService2;

        public HomeController(ILogger<HomeController> logger,
            IDateTime dateTime) // ITimeService timeService1, ITimeService timeService2
        {
            _logger = logger;
            _dateTime = dateTime;
            //_timeService1 = timeService1;
            //_timeService2 = timeService2;
        }

        public IActionResult Index()
        {
            //ViewData["timeService1"] = _timeService1.GetCurrentTime();
            //ViewData["timeService2"] = _timeService2.GetCurrentTime();

            var serverTime = _dateTime.Now;

            if (serverTime.Hour < 12)
            {
                ViewData["Message"] = "It's morning here - Good Morning!";
            }
            else if (serverTime.Hour < 17)
            {
                ViewData["Message"] = "It's afternoon here - Good Afternoon!";
            }
            else
            {
                ViewData["Message"] = "It's evening here - Good Evening!";
            }

            try
            {
                for (int i = 60; i < 80; i++)
                {
                    if (i == 69)
                    {
                        _logger.LogWarning("Very dangerous number");
                    }
                    else if (i == 79)
                    {
                        throw new Exception("End of for loop");
                    }
                    else
                    {
                        _logger.LogInformation("The value of i is {Variable}", i);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.StackTrace);
                _logger.LogError("An error was caught.");
            }

            return View();
        }

        public IActionResult About([FromServices] IDateTime dateTime)
        {
            ViewData["Message"] = "Currently on the server the time is " + dateTime.Now;

            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogWarning("You accessed the Privacy page!");
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

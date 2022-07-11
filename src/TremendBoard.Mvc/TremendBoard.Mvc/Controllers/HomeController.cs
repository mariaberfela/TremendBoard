using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Models;

namespace TremendBoard.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDateTime _dateTime;
        private readonly ITransientTimeService _timeService1;
        private readonly IScopedTimeService _timeService2;
        private readonly ISingletonTimeService _timeService3;

        public HomeController(
            IDateTime dateTime,
            ITransientTimeService transientTime, 
            IScopedTimeService scopedTime, 
            ISingletonTimeService singletonTime)
        {
            _dateTime = dateTime;
            _timeService1 = transientTime;
            _timeService2 = scopedTime;
            _timeService3 = singletonTime;

        }
        

        public IActionResult Index()
        {
            var serverTime = _dateTime.Now;
            ViewData["timeService1"] = _timeService1.GeneratedTime;
            ViewData["timeService2"] = _timeService2.GeneratedTime;
            ViewData["timeService3"] = _timeService3.GeneratedTime;


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

using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Enums;
using TremendBoard.Mvc.Models;

namespace TremendBoard.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDateTime _dateTime;
        private readonly ITimeService _timeService1;
        private readonly ITimeService _timeService2;


        public HomeController(IDateTime dateTime, ITimeService timeService1, ITimeService timeService2)
        {
            _dateTime = dateTime;
            _timeService1 = timeService1;
            _timeService2 = timeService2;            
        }

        public IActionResult Index()
        {
            var serverTime = _dateTime.Now;

            if (serverTime.Hour < 12)
                ViewData["Message"] = DayTime.Morning.ToString();
            else if (serverTime.Hour < 17)
                ViewData["Message"] = DayTime.Afternoon.ToString();
            else
                ViewData["Message"] = DayTime.Evening.ToString();

            ViewData["timeService1"] = _timeService1.GetCurrentTime();
            ViewData["timeService2"] = _timeService2.GetCurrentTime();

            Log.Information("HomeController.Index(): Returning View.");
            return View();
        }

        public IActionResult About([FromServices] IDateTime dateTime)
        {
            ViewData["Message"] = "Currently on the server the time is " + dateTime.Now;

            return View();
        }

        public IActionResult Error()
        {
            Log.Information(string.Format("HomeController.Error(): HttpContext.TraceIdentifier = {0}.", HttpContext.TraceIdentifier));
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public class DayTime: Enumeration
        {
            public static DayTime Morning => new(1, "It's morning here - Good Morning!");
            public static DayTime Afternoon => new(2, "It's afternoon here - Good Afternoon!");
            public static DayTime Evening => new(3, "It's evening here - Good Evening!");


            public DayTime(int id, string msg) : base(id, msg) { }

        }

    }
}

using Microsoft.AspNetCore.Mvc;
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

        public HomeController(IDateTime dateTime,
            ITimeService timeService1,
            ITimeService timeService2)
        {
            _dateTime = dateTime;
            _timeService1 = timeService1;
            _timeService2 = timeService2;
        }

        public IActionResult Index()
        {
            ViewData["timeService1"] = _timeService1.GetCurrentTime();
            ViewData["timeService2"] = _timeService2.GetCurrentTime();

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

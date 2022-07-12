using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Models;

namespace TremendBoard.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDateTime _dateTime;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IDateTime dateTime, ILogger<HomeController> logger)
        {
            _dateTime = dateTime;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var serverTime = _dateTime.Now;
            
            if (serverTime.Hour < 12)
            {
                ViewData["Message"] = "It's morning here - Good Morning!";
                _logger.LogInformation("Server time hour is {hour} - Good Morning!", serverTime.Hour);
            }
            else if (serverTime.Hour < 17)
            {
                ViewData["Message"] = "It's afternoon here - Good Afternoon!";
                _logger.LogInformation("Server time hour is {hour} - Good Afternoon!", serverTime.Hour);
            }
            else
            {
                ViewData["Message"] = "It's evening here - Good Evening!";
                _logger.LogInformation("Server time hour is {hour} - Good Evening!", serverTime.Hour);
            }

            List<string> items = new List<string>
            {
                "true",
                "false",
                "1",
                "0",
                "warning",
                "test value"
            };

            items.ForEach(item =>
            {
                if (item == "warning")
                {
                    _logger.LogWarning("The current item represents a warning point.");
                }
                if (bool.TryParse(item, out bool output))
                {
                    _logger.LogInformation("The value {currentItem} can be converted to bool!", item);
                }
                else
                {
                    _logger.LogError("Error! {currentItem} can not be converted to bool!", item);
                }
            });

            return View();
        }

        public IActionResult About([FromServices] IDateTime dateTime)
        {
            _logger.LogWarning("About page was accessed!");
            ViewData["Message"] = "Currently on the server the time is " + dateTime.Now;

            return View();
        }

        public IActionResult Error()
        {
            _logger.LogError("An error has occured.");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

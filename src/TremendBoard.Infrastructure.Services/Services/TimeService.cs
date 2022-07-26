using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class TimeService : ITimeService
    {
        private string _time { get; set; }
        public TimeService()
        {
             _time = DateTime.Now.ToString("hh:mm:ss.fff");
        }
        public string GetCurrentTime()
        {
           
            return _time;
        }
        public string GoodDay()
        {
            var hour = DateTime.Now.Hour;
            if (hour < 10)
                return "Good Morning!";
            else return "Hello!";
        }
    }
}

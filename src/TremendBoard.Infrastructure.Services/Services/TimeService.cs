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
        public string Time { get; set; }

        public TimeService()
        {
            Time = DateTime.Now.ToString("hh:mm:ss.fff");
        }

        public string GetCurrentTime() => Time;
    }
}

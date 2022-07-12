using System;

using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class TimeService : ITimeService
    {
        private string time { get; set; }

        public TimeService()
        {
            time = DateTime.Now.ToString("hh:mm:ss.fff");
        }

        public string GetCurrentTime()
        {
            return time;
        }
    }
}

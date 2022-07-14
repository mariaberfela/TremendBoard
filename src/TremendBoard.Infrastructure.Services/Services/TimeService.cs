using System;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class TimeService : ITimeService
    {
        private string currentTime;
        public TimeService() 
        {
            currentTime = DateTime.Now.ToString("hh:mm:ss.fff");
        }

        public string GetCurrentTime()
        {
            return currentTime;
        }
    }
}

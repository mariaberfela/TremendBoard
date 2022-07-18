using System;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class TimeMoqService : ITimeMoq
    {
        public DateTime Time { get; set; }

        public TimeMoqService()
        {
            Time = GetTime();
        }

        public DateTime GetTime()
        {
            return DateTime.Now;
        }
        
        
    }
}

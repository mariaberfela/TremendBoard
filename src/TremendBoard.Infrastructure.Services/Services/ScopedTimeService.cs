using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class ScopedTimeService : IScopedTimeService
    {
        public ScopedTimeService()
        {
            GeneratedTime = GetCurrentTime();
        }

        public DateTime GeneratedTime { get; set; }

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;

        }
    }
}

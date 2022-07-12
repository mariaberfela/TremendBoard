using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    internal class TimeService : ITimeService
    {
        public string GetCurrentTime()
        {
            string time = DateTime.Now.ToString("h:mm:ss tt");
            return time;
        }
       
    }
}

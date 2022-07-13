using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Concrete
{
    public class TimeService : ITimeService
    {
        private string Time { get; set; }

        public TimeService()
        {
            Time = DateTime.Now.ToString("HH:mm:ss:fffffff");
        }

        public string GetCurrentTime()
        {
            return Time;
        }
    }
}

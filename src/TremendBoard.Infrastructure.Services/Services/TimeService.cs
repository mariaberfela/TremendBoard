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
        private string time;

        public TimeService()
        {
            this.time = DateTime.Now.ToString("HH:mm:ss:ffff");
        }

        public string GetCurrentTime()
        {
            return this.time;
        }
    }
}

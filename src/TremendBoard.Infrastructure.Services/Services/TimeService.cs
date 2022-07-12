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
            _time = DateTime.Now.ToString("hh:mm:ss.fff tt");
        }
        public string GetCurrentTime()
        {
            
            return _time;
        }
    }
}

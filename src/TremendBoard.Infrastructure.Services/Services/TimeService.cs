using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class TimeService: ITimeService
    {
        private String _time;

        public TimeService()
        {
            
            _time = DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss.fff");
        }

        public String GetCurrentTime()
        {
            return _time;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class TimeService : ITimeService
    {
        private DateTime timenow;
        public TimeService()
        {
            DateTime v = DateTime.Now;
            timenow = v; ;
        }
        public DateTime GetCurrentTime
        {
            get { return timenow; }
        }
    }
}

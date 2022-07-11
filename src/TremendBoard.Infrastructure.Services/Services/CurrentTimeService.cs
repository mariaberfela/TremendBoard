using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class CurrentTimeService : ITimeService
    {
        public DateTime GetCurrentTime { get; private set; }
        public string GetGUID { get; private set; }

        public CurrentTimeService()
        {
            GetCurrentTime = DateTime.Now;
            GetGUID = Guid.NewGuid().ToString(); 
        }
    }
}

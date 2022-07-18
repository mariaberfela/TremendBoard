using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class CurrentTime : ITimeService
    {
        private string _currentTime;

        public CurrentTime()
        {
            _currentTime = DateTime.Now.ToString("HH:mm:ss:ffff");
        }


        String ITimeService.GetCurrentTime()
        {
            return _currentTime;
        }
    }
}

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
        public string currentTime;
        public string guid;

        public CurrentTimeService()
        {
            currentTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fffff");
            guid = Guid.NewGuid().ToString(); 
        }

        public string GetCurrentTime => currentTime;
        public string GetGUID => guid;
    }
}

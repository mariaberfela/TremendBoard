using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class MailingService
    {
        public Task<bool> NotifyTaskStopAsync()
        {
            return Task.FromResult(true);
        }

        public Task<bool> SendAsync()
        {
            return Task.FromResult(true);
        }
    }
}

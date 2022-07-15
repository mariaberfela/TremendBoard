using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class LowerCaseService
    {
        private IJobTestService _jobTestService;
        public LowerCaseService(IJobTestService jobTestService)
        {
            _jobTestService = jobTestService;
        }
        public string GetLoweCase()
        {
            var lowerString = _jobTestService.ToString().ToLower();
            return lowerString;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class WorkService : IWorkService
    {
        private string jobType;

        public WorkService()
        {
            this.jobType = "fulltime";
        }
        public WorkService(string jobType)
        {
            this.jobType = jobType;
        }

        public int CalculateRemainingWeekHours(int workedHours)
        {

            //try
            //{
                if (this.jobType == "parttime")
                {
                    return 20 - workedHours;
                }
                else if (this.jobType == "fulltime")
                {
                    return 40 - workedHours;
                }
                else
                {
                    throw new Exception();
                }
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine("nu ai job");
            //    return 0; 
            //}

        }
    }
}

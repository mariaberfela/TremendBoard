using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class DayService: IDay
    {
   
        private string _day;

        public DayService()
        {
            this._day = "00";
        }
        public DayService(string day)
        {
            this._day = day;
        }

        public int CalculateRemainingHoursInThisDay(int hours)
        {
                if (this._day == "Good Morning!")
                {
                    return 24 - hours;
                }
                else
                if (this._day == "Hello!")
                {
                    return 24 - hours;
                }
                else
                {
                    throw new Exception();
                }
           

        }
  
    }
}

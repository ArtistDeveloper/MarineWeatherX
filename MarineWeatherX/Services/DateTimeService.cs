using MarineWeatherX.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarineWeatherX.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime? GetCurrentTIme()
        {
            return DateTime.Now;
        }
    }
}

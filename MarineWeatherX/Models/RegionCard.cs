using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarineWeatherX.Models
{
    public class RegionCard
    {
        public string RegionName { get; set; }
        public string SignificantWaveHeight { get; set; }
        public string WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public string SeaSurfaceTemp { get; set; }
    }
}

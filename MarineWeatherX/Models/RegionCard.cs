using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MarineWeatherX.Models
{
    public class RegionCard
    {
        public string? RegionName { get; set; }
        public int? RegionID { get; set; }
        public double? WaveHeight { get; set; }
        public double? WindSpeed { get; set; }
        public int? WindDirection { get; set; }
        public double? SeaSurfaceTemp { get; set; }
        public RegionStatus Status { get; set; }
        public Brush? StatusBrush { get; set; }
    }
}

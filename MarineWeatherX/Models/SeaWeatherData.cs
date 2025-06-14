using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarineWeatherX.Models
{
    public class SeaWeatherData
    {
        public string TP { get; set; }      // 관측 종류
        public DateTime TM { get; set; }    // 관측 시간
        public int STN_ID { get; set; }     // 지점 ID
        public string STN_KO { get; set; }  // 지점명
        public double? WH { get; set; }     // 유의파고
        public int? WD { get; set; }        // 풍향
        public double? WS { get; set; }     // 풍속
        public double? WS_GST { get; set; } // 순간 최대 풍속
        public double? TW { get; set; }     // 해수면 온도
        public double? TA { get; set; }     // 기온
        public double? PA { get; set; }     // 해면기압
        public double? HM { get; set; }     // 상대습도
    }
}

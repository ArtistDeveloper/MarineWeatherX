using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MarineWeatherX.Models;

namespace MarineWeatherX.Services
{
    public class MarineWeatherService
    {
        public async Task<List<SeaWeatherData>> GetSeaWeatherDataAsync(List<string> regionNames)
        {
            DateTime now = DateTime.Now;
            DateTime targetTime = now.Minute < 30
                ? new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0)
                : new DateTime(now.Year, now.Month, now.Day, now.Hour, 30, 0);

            string tm = targetTime.ToString("yyyyMMddHHmm");
            string url = $"https://apihub.kma.go.kr/api/typ01/url/sea_obs.php?tm={tm}" +
                         $"&stn=0" +
                         $"&help=1" +
                         $"&authKey={App.Config["ApiKey"]}";

            using HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"서버 응답 실패 - 코드: {response.StatusCode}");

            var stream = await response.Content.ReadAsStreamAsync();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var encoding = Encoding.GetEncoding("euc-kr");

            using var reader = new System.IO.StreamReader(stream, encoding);
            string content = await reader.ReadToEndAsync();

            return ParseData(content, regionNames);
        }

        private List<SeaWeatherData> ParseData(string content, List<string> regionNames)
        {
            var lines = content.Split('\n');
            var dataList = new List<SeaWeatherData>();

            foreach (var line in lines)
            {
                if (line.StartsWith("#") || string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(',');
                if (parts.Length < 14) continue;

                try
                {
                    var data = new SeaWeatherData
                    {
                        TP = parts[ColumnIndex.TP].Trim(),
                        TM = DateTime.ParseExact(parts[ColumnIndex.TM].Trim(), "yyyyMMddHHmm", CultureInfo.InvariantCulture),
                        STN_ID = int.Parse(parts[ColumnIndex.STN_ID].Trim()),
                        STN_KO = parts[ColumnIndex.STN_KO].Trim(),
                        WH = ParseNullableDouble(parts[ColumnIndex.WH]),
                        WD = ParseNullableInt(parts[ColumnIndex.WD]),
                        WS = ParseNullableDouble(parts[ColumnIndex.WS]),
                        WS_GST = ParseNullableDouble(parts[ColumnIndex.WS_GST]),
                        TW = ParseNullableDouble(parts[ColumnIndex.TW]),
                        TA = ParseNullableDouble(parts[ColumnIndex.TA]),
                        PA = ParseNullableDouble(parts[ColumnIndex.PA]),
                        HM = ParseNullableDouble(parts[ColumnIndex.HM])
                    };
                    // 필터링: regionNames에 포함된 지역만 추가
                    if (regionNames == null || regionNames.Count == 0 || regionNames.Contains(data.STN_KO))
                        dataList.Add(data);
                }
                catch
                {
                    continue;
                }
            }
            return dataList;
        }

        private double? ParseNullableDouble(string s)
        {
            if (double.TryParse(s.Trim(), out double val) && val != -99.0) return val;
            return null;
        }

        private int? ParseNullableInt(string s)
        {
            if (int.TryParse(s.Trim(), out int val) && val != -99) return val;
            return null;
        }

        private static class ColumnIndex
        {
            public const int TP = 0;
            public const int TM = 1;
            public const int STN_ID = 2;
            public const int STN_KO = 3;
            public const int WH = 6;
            public const int WD = 7;
            public const int WS = 8;
            public const int WS_GST = 9;
            public const int TW = 10;
            public const int TA = 11;
            public const int PA = 12;
            public const int HM = 13;
        }
    }
}

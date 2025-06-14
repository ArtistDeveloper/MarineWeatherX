using System.Threading.Tasks;
using MarineWeatherX.Services;

namespace MarineWeatherX.Tests
{
    public static class MarineWeatherServiceTests
    {
        public static async Task PrintSeaWeatherDataAsync()
        {
            var service = new MarineWeatherService();
            //var result = await service.GetSeaWeatherDataAsync();

            //Console.WriteLine($"데이터 개수: {result.Count}");
            //foreach (var item in result)
            //{
            //    Console.WriteLine($"{item.TM:yyyy-MM-dd HH:mm} | {item.STN_KO} ({item.STN_ID}) | WH: {item.WH}m | WS: {item.WS}m/s | TA: {item.TA}°C | PA: {item.PA}hPa | HM: {item.HM}%");
            //}
        }
    }
}

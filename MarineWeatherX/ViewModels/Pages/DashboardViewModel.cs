using MarineWeatherX.Interfaces;
using MarineWeatherX.Models;
using MarineWeatherX.Services;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Media;

namespace MarineWeatherX.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        #region FIELDS

        private bool isInitialized = false;

        #endregion

        #region PROPERTIES
        [ObservableProperty]
        private List<SeaWeatherData>? regionDatas;

        [ObservableProperty]
        private IEnumerable<string?>? administrativeAgency;

        [ObservableProperty]
        private string? selectedAdministrativeAgency;

        [ObservableProperty]
        private int? selectedTotalPopulation;

        [ObservableProperty]
        private int? selectedMalePopulation;

        [ObservableProperty]
        private int? selectedFeMalePopulation;

        [ObservableProperty]
        private double? selectedSexRatio;

        [ObservableProperty]
        private int? selectedNumberOfHouseholds;

        [ObservableProperty]
        private string? currentTime = string.Empty;

        [ObservableProperty]
        private List<string?>? datas = new List<string?>();

        [ObservableProperty]
        private ObservableCollection<RegionCard> regionCards = new ObservableCollection<RegionCard>();

        #endregion

        #region CONSTRUCTOR
        public DashboardViewModel()
        {
            LoadSampleData();
        }

        #endregion

        #region COMMANDS

        [RelayCommand]
        private void GoToBlazorWasmLink()
        {
            string url = "https://inf.run/tptBE";

            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening web page: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region METHODS
        private async Task LoadSampleData()
        {
            List<string> filteredRegions = new List<string>
            {
                "울릉도", "덕적도", "칠발도", "거문도", "동해",
                "포항", "마라도", "외연도", "신안", "인천",
            };

            var service = new MarineWeatherService();
            regionDatas = await service.GetSeaWeatherDataAsync(filteredRegions);
            UpdateRegionCards(regionDatas);
        }

        private void UpdateRegionCards(List<SeaWeatherData> regionDatas)
        {
            RegionCards.Clear();


            foreach (var r in regionDatas)
            {
                var status = EvaluateStatus(r.WS, r.WH);
                var statusBrush = GetStatusBrush(status);

                RegionCards.Add(new RegionCard
                {
                    RegionName = r.STN_KO,
                    RegionID = r.STN_ID,
                    WaveHeight = r.WH,
                    WindSpeed = r.WS,
                    WindDirection = r.WD,
                    SeaSurfaceTemp = r.TA,
                    Status = status,
                    StatusBrush = statusBrush
                });
            }
        }

        public void OnNavigatedTo()
        {
            if (!isInitialized)
            {
                //InitializeViewModelAsync();
            }
        }

        public void OnNavigatedFrom()
        {
            
        }

        private static RegionStatus EvaluateStatus(double? windSpeed, double? waveHeight)
        {
            if (windSpeed >= 21 || waveHeight >= 5) return RegionStatus.Danger;
            if ((windSpeed is >= 14 and < 21) || (waveHeight is >= 3 and < 5))
                return RegionStatus.Caution;
            return RegionStatus.Safe;
        }

        private static SolidColorBrush GetStatusBrush(RegionStatus status) => status switch
        {
            RegionStatus.Danger => new(Color.FromRgb(220, 20, 60)),
            RegionStatus.Caution => new(Color.FromRgb(255, 140, 0)),
            RegionStatus.Safe => new(Color.FromRgb(78, 159, 117)),
            _ => Brushes.Transparent
        };
        #endregion
    }
}

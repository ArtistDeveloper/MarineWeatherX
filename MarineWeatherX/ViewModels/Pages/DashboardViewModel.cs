using MarineWeatherX.Interfaces;
using MarineWeatherX.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MarineWeatherX.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        #region FIELDS

        private readonly IDatabase<GangnamguPopulation> database;

        private bool isInitialized = false;

        #endregion

        #region PROPERTIES

        [ObservableProperty]
        private IEnumerable<GangnamguPopulation?>? gangnamguPopulations;

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
        public DashboardViewModel(IDatabase<GangnamguPopulation> database)
        {
            this.database = database;

            InitializeViewModelAsync();
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

        [RelayCommand]
        private void OnSearchDeatil()
        {
            var data = this.GangnamguPopulations?.Where(c => c.AdministrativeAgency == this.SelectedAdministrativeAgency).FirstOrDefault();

            this.SelectedTotalPopulation = data.TotalPopulation;
            this.SelectedMalePopulation = data.MalePopulation;
            this.SelectedFeMalePopulation = data.FemalePopulation;
            this.SelectedSexRatio = data.SexRatio;
            this.SelectedNumberOfHouseholds = data.NumberOfHouseholds;
        }

        #endregion

        #region METHODS

        public void OnNavigatedTo()
        {
            if (!isInitialized)
            {
                InitializeViewModelAsync();
            }
        }

        public void OnNavigatedFrom()
        {
            //
        }

        /// <summary>
        /// 샘플 데이터 사용
        /// </summary>
        public void LoadSampleData()
        {
            RegionCards.Clear();
            RegionCards.Add(new RegionCard
            {
                RegionName = "울릉도",
                SignificantWaveHeight = "2.5m",
                WindSpeed = "8m/s",
                WindDirection = "남동",
                SeaSurfaceTemp = "18.4°C"
            });

            RegionCards.Add(new RegionCard
            {
                RegionName = "부산",
                SignificantWaveHeight = "1.8m",
                WindSpeed = "5m/s",
                WindDirection = "북서",
                SeaSurfaceTemp = "20.1°C"
            });

            RegionCards.Add(new RegionCard
            {
                RegionName = "부산",
                SignificantWaveHeight = "1.8m",
                WindSpeed = "5m/s",
                WindDirection = "북서",
                SeaSurfaceTemp = "20.1°C"
            });

            RegionCards.Add(new RegionCard
            {
                RegionName = "부산",
                SignificantWaveHeight = "1.8m",
                WindSpeed = "5m/s",
                WindDirection = "북서",
                SeaSurfaceTemp = "20.1°C"
            });

            RegionCards.Add(new RegionCard
            {
                RegionName = "부산",
                SignificantWaveHeight = "1.8m",
                WindSpeed = "5m/s",
                WindDirection = "북서",
                SeaSurfaceTemp = "20.1°C"
            });

            RegionCards.Add(new RegionCard
            {
                RegionName = "부산",
                SignificantWaveHeight = "1.8m",
                WindSpeed = "5m/s",
                WindDirection = "북서",
                SeaSurfaceTemp = "20.1°C"
            });
        }

        /// <summary>
        /// 동적으로 Card를 채우는 함수
        /// </summary>
        /// <returns></returns>
        public void UpdateSelectedRegions(List<string> selectedRegions)
        {
            RegionCards.Clear();
            foreach (var region in selectedRegions)
            {
                RegionCards.Add(new RegionCard
                {
                    RegionName = region,
                    SignificantWaveHeight = "Dummy",
                    WindSpeed = "Dummy",
                    WindDirection = "Dummy",
                    SeaSurfaceTemp = "Dummy"
                });
            }
        }

        private async Task InitializeViewModelAsync()
        {
            // 비동기로 데이터를 가져오기
            this.GangnamguPopulations = await Task.Run(() => this.database?.Get());

            // 가져온 데이터를 가지고 필요한 작업 수행
            if (this.GangnamguPopulations != null)
            {
                this.AdministrativeAgency = this.GangnamguPopulations.Select(c => c.AdministrativeAgency).ToList();
            }

            isInitialized = true;
        }

        #endregion
    }
}

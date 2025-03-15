using MarineWeatherX.Interfaces;
using MarineWeatherX.Models;
using Microsoft.EntityFrameworkCore.Storage;
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
        private int _counter = 0;

        [ObservableProperty]
        private string? currentTime = string.Empty;

        [ObservableProperty]
        private List<string?>? datas = new List<string?>();

        #endregion

        #region CONSTRUCTOR
        public DashboardViewModel(IDatabase<GangnamguPopulation> database)
        {
            this.database = database;

            this.InitializeViewModelAsync();
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

using System.Windows.Media;
using MarineWeatherX.Models;
using Wpf.Ui.Abstractions.Controls;
using MarineWeatherX.Interfaces;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore.Storage;

namespace MarineWeatherX.ViewModels.Pages
{
    public partial class DataViewModel : ObservableObject, INavigationAware
    {
        #region FIELDS

        private bool _isInitialized = false;

        private readonly IDatabase<GangnamguPopulation> _database;

        #endregion

        #region PROPERTIES

        [ObservableProperty]
        private IEnumerable<GangnamguPopulation?>? _gangnamguPopulations;

        [ObservableProperty]
        private IEnumerable<string?>? _administrativeAgency;

        [ObservableProperty]
        private int? _selectedId;

        [ObservableProperty]
        private string? _selectedAdministrativeAgency;

        [ObservableProperty]
        private int? _selectedTotalPopulation;

        [ObservableProperty]
        private int? _selectedMalePopluation;

        [ObservableProperty]
        private int? _selectedFemalePopulation;

        [ObservableProperty]
        private double? _selectedSexRatio;

        [ObservableProperty]
        private int? _selectedNumberOfHouseholds;

        [ObservableProperty]
        private double? _selectedNumberOfPeoplePerHouseholds;

        #endregion

        #region CONSTRUCTOR
        public DataViewModel(IDatabase<GangnamguPopulation> database)
        {
            _database = database;
        }

        #endregion

        #region COMMANDS

        [RelayCommand]
        private void OnSelectAdministrativeAgency()
        {
            var selectedData = this.SelectedAdministrativeAgency;
        }

        [RelayCommand]
        private void CreateNewData()
        {
            GangnamguPopulation gangnamguPopulation = new GangnamguPopulation();

            gangnamguPopulation.AdministrativeAgency = this.SelectedAdministrativeAgency;
            gangnamguPopulation.TotalPopulation = this.SelectedTotalPopulation;
            gangnamguPopulation.MalePopulation = this.SelectedMalePopluation;
            gangnamguPopulation.FemalePopulation = this.SelectedFemalePopulation;
            gangnamguPopulation.SexRatio = this.SelectedSexRatio;
            gangnamguPopulation.NumberOfHouseholds = this.SelectedNumberOfHouseholds;
            gangnamguPopulation.NumberOfPeoplePerHousehold = this.SelectedNumberOfPeoplePerHouseholds;

            this._database.Create(gangnamguPopulation);
        }

        [RelayCommand]
        private void UpdateData()
        {
            var data = this._database.GetDetail(this.SelectedId);

            data.AdministrativeAgency = this.SelectedAdministrativeAgency;
            data.TotalPopulation = this.SelectedTotalPopulation;
            data.MalePopulation = this.SelectedMalePopluation;
            data.FemalePopulation = this.SelectedFemalePopulation;
            data.SexRatio = this.SelectedSexRatio;
            data.NumberOfHouseholds = this.SelectedNumberOfHouseholds;
            data.NumberOfPeoplePerHousehold = this.SelectedNumberOfPeoplePerHouseholds;

            this._database?.Update(data);
        }

        [RelayCommand]
        private void DeleteData()
        {
            this._database?.Delete(this.SelectedId);
        }

        [RelayCommand]
        private void ReadAllData()
        {
            this.GangnamguPopulations = this._database.Get();
        }

        [RelayCommand]
        private void ReadDetailData()
        {
            var data = this._database.GetDetail(this.SelectedId);

            this.SelectedAdministrativeAgency = data.AdministrativeAgency;
            this.SelectedTotalPopulation = data.TotalPopulation;
            this.SelectedMalePopluation = data.MalePopulation;
            this.SelectedFemalePopulation = data.FemalePopulation;
            this.SelectedSexRatio = data.SexRatio;
            this.SelectedNumberOfHouseholds = data.NumberOfHouseholds;
            this.SelectedNumberOfPeoplePerHouseholds = data.NumberOfPeoplePerHousehold;
        }

        #endregion

        #region METHODS
        public Task OnNavigatedToAsync()
        {
            if (!_isInitialized)
            {
                InitializeViewModelAsync();
            }

            return Task.CompletedTask;
        }

        public Task OnNavigatedFromAsync() => Task.CompletedTask;

        private async Task InitializeViewModelAsync()
        {
            // 비동기로 데이터를 가져오기
            GangnamguPopulations = await Task.Run(() => _database?.Get());

            // 가져온 데이터를 가지고 필요한 작업 수행
            if (GangnamguPopulations != null)
            {
                AdministrativeAgency = GangnamguPopulations?.Select(c => c.AdministrativeAgency).ToList();
            }

            _isInitialized = true;
        }

        #endregion
    }
}

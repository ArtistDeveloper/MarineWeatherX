using MarineWeatherX.Models;
using MarineWeatherX.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace MarineWeatherX.Views.Pages
{
    public partial class DataPage : INavigableView<DataViewModel>
    {
        public DataViewModel ViewModel { get; }

        public DataPage(DataViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            InitializeComponent();
        }

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "AdministrativeAgency":
                    searchGridLoadingControl.Visibility = Visibility.Collapsed;
                    searchGrid.Visibility = Visibility.Visible;
                    break;
                case "GangnamguPopulations":
                    dgGridLoadingContorl.Visibility = Visibility.Collapsed;
                    dgGrid.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}

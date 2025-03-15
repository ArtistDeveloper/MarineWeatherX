using MarineWeatherX.ViewModels.Pages;
using System.Windows.Media;
using Wpf.Ui.Abstractions.Controls;

namespace MarineWeatherX.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardPage(DashboardViewModel viewModel)
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
                    this.loadingGrid.Visibility = Visibility.Collapsed;
                    this.dashboardGrid.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}

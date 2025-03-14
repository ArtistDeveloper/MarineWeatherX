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

            InitializeComponent();
        }
    }
}

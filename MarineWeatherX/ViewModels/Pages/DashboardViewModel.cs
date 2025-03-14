using MarineWeatherX.Interfaces;

namespace MarineWeatherX.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IDateTime _iDateTIme;

        [ObservableProperty]
        private int _counter = 0;

        [ObservableProperty]
        private string? _currentTime = string.Empty;

        public DashboardViewModel(IDateTime dateTime)
        {
            this._iDateTIme = dateTime;
        }

        // RelayCommand: 사용자로부터 어떤 이벤트가 발생하면 이 함수를 호출해서 특정 로직을 수행하고 싶을 때 사용
        [RelayCommand]
        private void OnCounterIncrement()
        {
            Counter++;
        }

        [RelayCommand]
        private void OnTextChange()
        {
            this.CurrentTime = this._iDateTIme.GetCurrentTIme().ToString();
        }
    }
}

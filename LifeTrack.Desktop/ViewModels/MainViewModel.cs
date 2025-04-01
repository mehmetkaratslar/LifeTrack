using LifeTrack.Desktop.Commands;
using System.Windows.Input;

namespace LifeTrack.Desktop.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly ExpenseViewModel _expenseViewModel;
        private readonly NoteViewModel _noteViewModel;
        private readonly ReminderViewModel _reminderViewModel;
        private readonly SettingsViewModel _settingsViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateToViewCommand { get; }

        public MainViewModel(
            DashboardViewModel dashboardViewModel,
            ExpenseViewModel expenseViewModel,
            NoteViewModel noteViewModel,
            ReminderViewModel reminderViewModel,
            SettingsViewModel settingsViewModel)
        {
            _dashboardViewModel = dashboardViewModel;
            _expenseViewModel = expenseViewModel;
            _noteViewModel = noteViewModel;
            _reminderViewModel = reminderViewModel;
            _settingsViewModel = settingsViewModel;

            // Varsayılan görünümü Dashboard olarak ayarlayın
            CurrentViewModel = _dashboardViewModel;

            // Navigasyon komutu oluşturun
            NavigateToViewCommand = new RelayCommand(Navigate);
        }

        private void Navigate(object parameter)
        {
            if (parameter is string viewName)
            {
                switch (viewName)
                {
                    case "Dashboard":
                        CurrentViewModel = _dashboardViewModel;
                        break;
                    case "Expense":
                        CurrentViewModel = _expenseViewModel;
                        break;
                    case "Note":
                        CurrentViewModel = _noteViewModel;
                        break;
                    case "Reminder":
                        CurrentViewModel = _reminderViewModel;
                        break;
                    case "Settings":
                        CurrentViewModel = _settingsViewModel;
                        break;
                }
            }
        }
    }
}
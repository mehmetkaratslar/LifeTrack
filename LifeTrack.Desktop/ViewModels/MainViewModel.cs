using LifeTrack.Desktop.Commands;
using LifeTrack.Desktop.ViewModels;
using System;
using System.Windows.Input;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase _currentViewModel;

    // ViewModel örnekleri
    private readonly DashboardViewModel _dashboardViewModel;
    private readonly ExpenseViewModel _expenseViewModel;
    private readonly NoteViewModel _noteViewModel;
    private readonly ReminderViewModel _reminderViewModel;
    private readonly SettingsViewModel _settingsViewModel;

    public MainViewModel(
        DashboardViewModel dashboardViewModel,
        ExpenseViewModel expenseViewModel,
        NoteViewModel noteViewModel,
        ReminderViewModel reminderViewModel,
        SettingsViewModel settingsViewModel)
    {
        _dashboardViewModel = dashboardViewModel ?? throw new ArgumentNullException(nameof(dashboardViewModel));
        _expenseViewModel = expenseViewModel ?? throw new ArgumentNullException(nameof(expenseViewModel));
        _noteViewModel = noteViewModel ?? throw new ArgumentNullException(nameof(noteViewModel));
        _reminderViewModel = reminderViewModel ?? throw new ArgumentNullException(nameof(reminderViewModel));
        _settingsViewModel = settingsViewModel ?? throw new ArgumentNullException(nameof(settingsViewModel));

        // Başlangıçta Dashboard göster
        CurrentViewModel = _dashboardViewModel;

        // Komutları oluştur
        NavigateToDashboardCommand = new RelayCommand(() => CurrentViewModel = _dashboardViewModel);
        NavigateToExpensesCommand = new RelayCommand(() => CurrentViewModel = _expenseViewModel);
        NavigateToNotesCommand = new RelayCommand(() => CurrentViewModel = _noteViewModel);
        NavigateToRemindersCommand = new RelayCommand(() => CurrentViewModel = _reminderViewModel);
        NavigateToSettingsCommand = new RelayCommand(() => CurrentViewModel = _settingsViewModel);
    }

    public void Initialize()
    {
        // Alt viewmodelleri başlat
        (_dashboardViewModel as DashboardViewModel)?.Initialize();
        (_expenseViewModel as ExpenseViewModel)?.Initialize();
        (_noteViewModel as NoteViewModel)?.Initialize();
        (_reminderViewModel as ReminderViewModel)?.Initialize();
    }

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            SetProperty(ref _currentViewModel, value);
            // Debug için
            Console.WriteLine($"CurrentViewModel değişti: {value?.GetType().Name}");
        }
    }

    public ICommand NavigateToDashboardCommand { get; }
    public ICommand NavigateToExpensesCommand { get; }
    public ICommand NavigateToNotesCommand { get; }
    public ICommand NavigateToRemindersCommand { get; }
    public ICommand NavigateToSettingsCommand { get; }
}
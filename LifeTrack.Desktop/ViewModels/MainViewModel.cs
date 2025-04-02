<<<<<<< HEAD
﻿using LifeTrack.Desktop.Commands;
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
=======
﻿using LifeTrack.Desktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace LifeTrack.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            // Başlangıçta Dashboard'ı göster
            var dashboardView = new Views.DashboardView();
            dashboardView.DataContext = App.ServiceProvider.GetRequiredService<DashboardViewModel>();
            contentPresenter.Content = dashboardView;
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new Views.DashboardView();
            dashboardView.DataContext = App.ServiceProvider.GetRequiredService<DashboardViewModel>();
            contentPresenter.Content = dashboardView;
        }

        private void Expense_Click(object sender, RoutedEventArgs e)
        {
            var expenseView = new Views.ExpenseView();
            expenseView.DataContext = App.ServiceProvider.GetRequiredService<ExpenseViewModel>();
            contentPresenter.Content = expenseView;
        }

        private void Note_Click(object sender, RoutedEventArgs e)
        {
            var noteView = new Views.NoteView();
            noteView.DataContext = App.ServiceProvider.GetRequiredService<NoteViewModel>();
            contentPresenter.Content = noteView;
        }

        private void Reminder_Click(object sender, RoutedEventArgs e)
        {
            var reminderView = new Views.ReminderView();
            reminderView.DataContext = App.ServiceProvider.GetRequiredService<ReminderViewModel>();
            contentPresenter.Content = reminderView;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settingsView = new Views.SettingsView();
            settingsView.DataContext = App.ServiceProvider.GetRequiredService<SettingsViewModel>();
            contentPresenter.Content = settingsView;
>>>>>>> 70f5e287882ba226133052ee4d6f6266b64fb919
        }
    }
}
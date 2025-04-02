using LifeTrack.Desktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LifeTrack.Desktop
{
    public partial class MainWindow : Window
    {
        private readonly Views.NoteView _noteView;
        private readonly Views.DashboardView _dashboardView;
        private readonly Views.ExpenseView _expenseView;
        private readonly Views.ReminderView _reminderView;
        private readonly Views.SettingsView _settingsView;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
<<<<<<< HEAD

            // View'ları önceden oluştur
            _dashboardView = new Views.DashboardView();
            _noteView = new Views.NoteView();
            _expenseView = new Views.ExpenseView();
            _reminderView = new Views.ReminderView();
            _settingsView = new Views.SettingsView();

            // DataContext'leri ata
            _dashboardView.DataContext = App.ServiceProvider.GetRequiredService<DashboardViewModel>();
            _noteView.DataContext = App.ServiceProvider.GetRequiredService<NoteViewModel>();
            _expenseView.DataContext = App.ServiceProvider.GetRequiredService<ExpenseViewModel>();
            _reminderView.DataContext = App.ServiceProvider.GetRequiredService<ReminderViewModel>();
            _settingsView.DataContext = App.ServiceProvider.GetRequiredService<SettingsViewModel>();

            // Başlangıçta Dashboard'ı göster
            contentPresenter.Content = _dashboardView;
=======

            // Başlangıçta Dashboard'ı göster
            var dashboardView = new Views.DashboardView();
            dashboardView.DataContext = App.ServiceProvider.GetRequiredService<DashboardViewModel>();
            contentPresenter.Content = dashboardView;
>>>>>>> 70f5e287882ba226133052ee4d6f6266b64fb919
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            contentPresenter.Content = _dashboardView;
=======
            var dashboardView = new Views.DashboardView();
            dashboardView.DataContext = App.ServiceProvider.GetRequiredService<DashboardViewModel>();
            contentPresenter.Content = dashboardView;
        }

        private void Expense_Click(object sender, RoutedEventArgs e)
        {
            var expenseView = new Views.ExpenseView();
            expenseView.DataContext = App.ServiceProvider.GetRequiredService<ExpenseViewModel>();
            contentPresenter.Content = expenseView;
>>>>>>> 70f5e287882ba226133052ee4d6f6266b64fb919
        }

        private void Note_Click(object sender, RoutedEventArgs e)
        {
            contentPresenter.Content = _noteView;
        }

        private void Reminder_Click(object sender, RoutedEventArgs e)
        {
            contentPresenter.Content = _reminderView;
        }

        private void Expense_Click(object sender, RoutedEventArgs e)
        {
            contentPresenter.Content = _expenseView;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            contentPresenter.Content = _settingsView;
=======
            var settingsView = new Views.SettingsView();
            settingsView.DataContext = App.ServiceProvider.GetRequiredService<SettingsViewModel>();
            contentPresenter.Content = settingsView;
>>>>>>> 70f5e287882ba226133052ee4d6f6266b64fb919
        }
    }
}
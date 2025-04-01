using LifeTrack.Desktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

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
        }
    }
}
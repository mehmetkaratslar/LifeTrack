using LifeTrack.Desktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace LifeTrack.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            // Debug için
            if (viewModel != null)
            {
                MessageBox.Show("ViewModel başarıyla enjekte edildi!");

                // Komutları kontrol et
                if (viewModel.NavigateToDashboardCommand != null)
                    MessageBox.Show("Dashboard komutu mevcut");
                else
                    MessageBox.Show("Dashboard komutu NULL!");
            }
            else
            {
                MessageBox.Show("ViewModel NULL!");
            }

            DataContext = viewModel;


        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.CurrentViewModel = App.ServiceProvider.GetService<DashboardViewModel>();
            }
        }

        private void Expense_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.CurrentViewModel = App.ServiceProvider.GetService<ExpenseViewModel>();
            }
        }
    }
}
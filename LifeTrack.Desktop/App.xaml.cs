using LifeTrack.Desktop.ViewModels;
using LifeTrack.Services;
using LifeTrack.Services.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace LifeTrack.Desktop
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Veritabanını oluştur/güncelle
            try
            {
                using (var context = ServiceProvider.GetRequiredService<LifeTrackDbContext>())
                {
                    context.Database.EnsureCreated();
                }

                // Ana pencereyi doğrudan oluştur
                var viewModel = ServiceProvider.GetRequiredService<MainViewModel>();
                var mainWindow = new MainWindow(viewModel);
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            base.OnStartup(e);
        }


        private void ConfigureServices(IServiceCollection services)
        {
            // DbContext
            services.AddDbContext<LifeTrackDbContext>();

            // Servisler
            services.AddTransient<ExpenseService>();
            services.AddTransient<CategoryService>();
            services.AddTransient<NoteService>();
            services.AddTransient<ReminderService>();

            // ViewModels
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<ExpenseViewModel>();
            services.AddTransient<NoteViewModel>();
            services.AddTransient<ReminderViewModel>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<MainViewModel>();

            // MainWindow özel factory ile kaydediliyor
            services.AddTransient(provider => new MainWindow(provider.GetRequiredService<MainViewModel>()));
        }
    }
}
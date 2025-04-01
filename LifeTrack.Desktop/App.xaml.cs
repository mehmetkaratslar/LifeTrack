using LifeTrack.Core.Models;
using LifeTrack.Desktop.ViewModels;
using LifeTrack.Services;
using LifeTrack.Services.Data;
using LifeTrack.Services.Repositories;
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

            try
            {
                using (var context = ServiceProvider.GetRequiredService<LifeTrackDbContext>())
                {
                    context.Database.EnsureCreated();
                }

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
            services.AddScoped<IRepository<Expense>, ExpenseService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRepository<Note>, NoteService>();
            services.AddScoped<IRepository<Reminder>, ReminderService>();

            // ViewModels - Scoped kullanıyoruz (bir oturum için tek instance)
            services.AddScoped<DashboardViewModel>();
            services.AddScoped<ExpenseViewModel>();
            services.AddScoped<NoteViewModel>();
            services.AddScoped<ReminderViewModel>();
            services.AddScoped<SettingsViewModel>();
            services.AddScoped<MainViewModel>();
        }
    }
}
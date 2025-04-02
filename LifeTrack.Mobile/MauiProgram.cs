using LifeTrack.Mobile.ViewModels;
using LifeTrack.Services;
using LifeTrack.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeTrack.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddDbContext<LifeTrackDbContext>(options =>
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "lifetrack.db");
                options.UseSqlite($"Filename={dbPath}");
            });

            builder.Services.AddScoped<ExpenseService>();
            builder.Services.AddTransient<ExpenseListViewModel>();
            builder.Services.AddTransient<AddExpenseViewModel>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

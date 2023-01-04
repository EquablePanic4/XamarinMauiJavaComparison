using CenyWPolsce.Data;

using CommunityToolkit.Maui;

using Microsoft.Extensions.Logging;

using SkiaSharp.Views.Maui.Controls.Hosting;

namespace CenyWPolsce.MAUI.Native
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseSkiaSharp(true)
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            //Przygotowywanie bazy danych...
            var appDir = FileSystem.Current.AppDataDirectory;
            ApplicationDbContext.DatabasePath = Path.Combine(appDir, "ceny.db3");

            if (!File.Exists(ApplicationDbContext.DatabasePath))
            {
                File.WriteAllBytes(ApplicationDbContext.DatabasePath, Properties.Resources.ceny);
            }

            return builder.Build();
        }
    }
}
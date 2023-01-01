using CenyWPolsce.Data;

using CommunityToolkit.Maui;

using Microsoft.Extensions.Logging;

namespace CenyWPolsce.MAUI.Native
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
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

            //Rejestrowanie singletonów
            builder.Services.AddSingleton<ApplicationDbContext>();

            return builder.Build();
        }
    }
}
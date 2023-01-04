using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace CenyWPolsce.MAUI.Native
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            LiveCharts.Configure(conf =>
            {
                conf.AddSkiaSharp();
                conf.AddDefaultMappers();
                conf.AddLightTheme();
            });
        }
    }
}
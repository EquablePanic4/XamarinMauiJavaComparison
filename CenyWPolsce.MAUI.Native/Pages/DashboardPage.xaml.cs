using CenyWPolsce.Data;

using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;

using SkiaSharp;

namespace CenyWPolsce.MAUI.Native.Pages;

public partial class DashboardPage : ContentPage
{
    private readonly ApplicationDbContext _db = ApplicationDbContext.Instance;

    #region Statystki

    private readonly string _productsCount;
    private readonly string _recordsCount;
    private readonly string _highestRise;
    private readonly string _highestDecrease;

    #endregion

    public DashboardPage()
	{
        //Przygotowywanie bazy danych...
        var appDir = FileSystem.Current.AppDataDirectory;
        ApplicationDbContext.DatabasePath = Path.Combine(appDir, "ceny.db3");

#if DEBUG
        if (File.Exists(ApplicationDbContext.DatabasePath))
            File.Delete(ApplicationDbContext.DatabasePath);
#endif

        if (!File.Exists(ApplicationDbContext.DatabasePath))
        {
            File.WriteAllBytes(ApplicationDbContext.DatabasePath, Properties.Resources.ceny);
        }

        //Pobieranie niezb�dnych danych
        _recordsCount = _db.Products.Count().ToString();

        //Najwi�kszy wzrost / spadek ceny
        var products = _db.Products.GroupBy(g => g.Name)
            .ToList().Select(s => new
            {
                Name = s.Key,
                First = s.MinBy(x => x.Date).Price,
                Last = s.MaxBy(x => x.Date).Price,
            }).Select(x => new
            {
                Name = x.Name,
                Difference = (x.Last - x.First) / x.First * 100
            }).OrderBy(x => x.Difference).ToList();

        _productsCount = products.Count.ToString();
        _highestRise = products.MaxBy(x => x.Difference).Name.ToString();
        _highestDecrease = products.MinBy(x => x.Difference).Name.ToString();

        InitializeComponent();

        //Statystki bazodanowe
        RecordsCountLabel.Text = _recordsCount;
        ProductsNumberLabel.Text = _productsCount;
        HighestPriceRiseLabel.Text = _highestRise;
        HighestPriceDeacreaseLevel.Text = _highestDecrease;

        Chart.Series = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
                Fill = null
            }
        };

        Chart.Title = new LabelVisual
        {
            Text = "My chart title",
            TextSize = 25,
            Padding = new LiveChartsCore.Drawing.Padding(15),
            Paint = new SolidColorPaint(SKColors.DarkSlateGray)
        };
    }
}
using CenyWPolsce.Data;

namespace CenyWPolsce.MAUI.Native.Pages;

public partial class DashboardPage : ContentPage
{
    private readonly ApplicationDbContext _db = ApplicationDbContext.Instance;

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

        InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        //Statystki bazodanowe
        RecordsCountLabel.Text = _db.Products.Count().ToString();

        //Najwiêkszy wzrost / spadek ceny
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

        ProductsNumberLabel.Text = products.Count.ToString();
        HighestPriceRiseLabel.Text = products.MaxBy(x => x.Difference).Name.ToString();
        HighestPriceDeacreaseLevel.Text = products.MinBy(x => x.Difference).Name.ToString();
    }
}
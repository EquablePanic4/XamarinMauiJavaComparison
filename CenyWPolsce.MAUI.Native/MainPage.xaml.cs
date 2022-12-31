using CenyWPolsce.Data;

using SQLite;

using System.Diagnostics;

namespace CenyWPolsce.MAUI.Native
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            //Przygotowywanie bazy danych...
            var appDir = FileSystem.Current.AppDataDirectory;
            ApplicationDbContext.DatabasePath = Path.Combine(appDir, "ceny.db3");
            if (!File.Exists(ApplicationDbContext.DatabasePath))
            {
                File.WriteAllBytes(ApplicationDbContext.DatabasePath, Properties.Resources.ceny);
            }

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (var db = new ApplicationDbContext())
            {
                try
                {
                    ProductsNumberLabel.Text = db.Products.Select(s => s.Name).Distinct().Count().ToString();
                    RecordsCountLabel.Text = db.Products.Count().ToString();
                }
                
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        //protected override void OnAppearing()
        //{
        //base.OnAppearing();



        //var statusRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        //if (statusRead == PermissionStatus.Denied)
        //{
        //    await DisplayAlert("Błąd", "Do korzystania z aplikacji potrzebne jest uprawnienie odczytywania danych z pamięci.", "Rozumiem");
        //    Application.Current.Quit();
        //}

        //await Permissions.RequestAsync<Permissions.StorageRead>();

        //var statusWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
        //if (statusWrite == PermissionStatus.Denied)
        //{
        //    await DisplayAlert("Błąd", "Do korzystania z aplikacji potrzebne jest uprawnienie odczytywania danych z pamięci.", "Rozumiem");
        //    Application.Current.Quit();
        //}

        //await Permissions.RequestAsync<Permissions.StorageWrite>();

        ////Sprawdzamy czy baza danych istnieje
        //if (!File.Exists(ApplicationDbContext.DatabasePath))
        //{
        //    var directory = ApplicationDbContext.DatabasePath.Replace(Path.GetFileName(ApplicationDbContext.DatabasePath), String.Empty);
        //    if (!Directory.Exists(directory))
        //    {
        //        Directory.CreateDirectory(directory);
        //    }

        //    File.WriteAllBytes(ApplicationDbContext.DatabasePath, Properties.Resources.ceny);
        //}

        ////Wypełniamy dane na ekranie głównym
        //using (var db = new ApplicationDbContext())
        //{
        //    ProductsCount.Text = db.Products.Count().ToString();
        //}
        //}

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}
    }
}
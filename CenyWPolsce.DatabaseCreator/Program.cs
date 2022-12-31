using CenyWPolsce.DatabaseCreator.Services;
using CenyWPolsce.Data.Tables;
using CenyWPolsce.Data;
using Microsoft.EntityFrameworkCore;

//Pobieranie danych ze wszystkich plików CSV...
Console.WriteLine("Pobieranie plików CSV...");
var files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Resources"), "*.csv");
Console.WriteLine($"Znaleziono {files.Length} pliki!");

Console.WriteLine("Przetwarzanie plików...");
var productsAndErrs = files.AsParallel().Select(file =>
{
    var reader = new CSVReader(file);

    var obj = new
    {
        Products = reader.ReadFile(),
        Errors = reader.Errors
    };

    Console.WriteLine($"Plik {Path.GetFileName(file)} został wczytany! | Rekordy: {obj.Products.Count}");
    return obj;
}).ToList(); //Deparallize query

Console.WriteLine($"Wczytane produkty: {productsAndErrs.Sum(s => s.Products.Count)} | Błędy: {productsAndErrs.Sum(x => x.Errors)}");
Console.WriteLine("Tworzenie bazy danych...");
using (var db = new ApplicationDbContext())
{
    db.Database.EnsureCreated();
    db.Database.Migrate();

    Console.WriteLine("Baza danych jest gotowa!");
    Console.WriteLine("Rozpoczynam zapisywanie danych...");

    foreach (var dataPart in productsAndErrs.SelectMany(s => s.Products).Chunk(500))
    {
        await db.Products.AddRangeAsync(dataPart);
        await db.SaveChangesAsync();
    }

    Console.WriteLine("Dane zostały zapisane do bazy!");
}

Console.WriteLine("Program zakończył swoją pracę.");
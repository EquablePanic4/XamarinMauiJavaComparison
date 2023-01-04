using CenyWPolsce.Data;

using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView;

using SkiaSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenyWPolsce.MAUI.Native.ViewModels
{
    class DashboardViewModel
    {
        //Serie danych
        public ISeries[] RiseSeries { get; set; }
        public ISeries[] DecreaseSeries { get; set; }

        public List<Axis> RiseXAxes { get; set; }

        //Statystyki
        public int RecordsCount { get; set; }
        public int ProductsCount { get; set; }
        public string HighestRise { get; set; }
        public string HighestDecrease { get; set; }

        //Pola ukryte
        private readonly ApplicationDbContext _db = ApplicationDbContext.Instance;

        const string PRIMARY_COLOR = "#512BD4";

        public DashboardViewModel()
        {
            //Pobieranie niezbędnych danych
            RecordsCount = _db.Products.Count();

            //Największy wzrost / spadek ceny
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

            ProductsCount = products.Count;
            HighestRise = products.MaxBy(x => x.Difference).Name;
            HighestDecrease = products.MinBy(x => x.Difference).Name;

            //Statystki bazodanowe

            var risingProduct = _db.Products.Where(x => x.Name == HighestRise).OrderBy(o => o.Date).ToList();
            RiseSeries = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = risingProduct.Select(s => s.Price).ToList(),
                    Fill = new SolidColorPaint(SKColor.Parse(PRIMARY_COLOR)),
                    GeometrySize = 0,
                    LineSmoothness = 1,
                }
            };

            RiseXAxes = new()
            {
                new()
                {
                    Name = "Data",
                    Labels = risingProduct.Select(s => s.Date.ToString("MM-yyyy")).ToList(),
                    TextSize = 30,
                    NameTextSize = 30,
                    
                }
            };

            DecreaseSeries = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = _db.Products.Where(x => x.Name == HighestDecrease).OrderBy(o => o.Date).Select(s => s.Price).ToList(),
                    Fill = new SolidColorPaint(SKColor.Parse(PRIMARY_COLOR))
                }
            };
        }
    }
}

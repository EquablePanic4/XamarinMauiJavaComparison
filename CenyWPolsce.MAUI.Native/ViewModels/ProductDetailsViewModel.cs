using CenyWPolsce.Data;
using CenyWPolsce.Data.Tables;
using CenyWPolsce.MAUI.Native.Models;

using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;

using Microsoft.EntityFrameworkCore;

using SkiaSharp;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CenyWPolsce.MAUI.Native.ViewModels
{
    public class ProductDetailsViewModel
    {
        const string PRIMARY_COLOR = "#512BD4";

        public string ProductName { get; set; }

        public List<Product> Products { get; set; }

        public List<ProductPricingModel> Pricings { get; set; }

        public ISeries[] Series { get; set; }

        public List<Axis> XAxes { get; set; }

        public List<Axis> YAxes { get; set; }

        public LabelVisual ChartTitle { get; set; }

        private readonly ApplicationDbContext _db = ApplicationDbContext.Instance;

        public ProductDetailsViewModel(string productName)
        {
            ProductName = productName;

            //Nic nie stoi na przeszkodzie żeby dane do wykresu wygenerować na poziomie ViewModelu
            Products = _db.Products.Where(x => x.Name == productName).OrderBy(o => o.Date).ToList();
            Series = new[]
            {
                new LineSeries<double>()
                {
                    Values = Products.Select(s => s.Price).ToArray(),
                    Fill = new SolidColorPaint(SKColor.Parse(PRIMARY_COLOR)),
                    GeometrySize = 0,
                    LineSmoothness = 1,
                    Stroke = new SolidColorPaint(SKColor.Parse(PRIMARY_COLOR))
                }
            };

            ChartTitle = new()
            {
                Text = "Przebieg zmian cen",
                TextSize = 40,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };

            XAxes = new()
            {
                new()
                {
                    Name = "Data",
                    Labels = Products.Select(s => s.Date.ToString("MM.yyyy")).ToList(),
                    TextSize = 30,
                    NameTextSize = 35,
                    LabelsAlignment = LiveChartsCore.Drawing.Align.Middle,
                    LabelsRotation = 45
                }
            };

            YAxes = new()
            {
                new()
                {
                    Labeler = x => $"{Math.Round(x, 1)} zł",
                    TextSize = 40,
                }
            };

            //Oraz generujemy dane do historii cen
            Pricings = Products.ConvertAll(c => new ProductPricingModel(c));
        }
    }
}

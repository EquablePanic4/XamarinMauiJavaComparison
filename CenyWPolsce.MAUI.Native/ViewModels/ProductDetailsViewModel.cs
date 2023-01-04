using CenyWPolsce.Data;
using CenyWPolsce.Data.Tables;

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
    public class ProductDetailsViewModel : INotifyPropertyChanged
    {
        public string ProductName { get; set; }

        public List<Product> Products { get; set; }

        public ISeries[] Series { get; set; } = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
                Fill = null
            }
        };
        public LabelVisual ChartTitle { get; set; }

        private readonly ApplicationDbContext _db = ApplicationDbContext.Instance;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Initialize(string productName)
        {
            ProductName = productName;
            OnPropertyChanged("ProductName");

            //Nic nie stoi na przeszkodzie żeby dane do wykresu wygenerować na poziomie ViewModelu
            Products = _db.Products.Where(x => x.Name == productName).OrderBy(o => o.Date).ToList();
            Series = new[]
            {
                new LineSeries<double>()
                {
                    Values = Products.Select(s => s.Price).ToArray(),
                    Fill = null
                }
            };
            OnPropertyChanged("Series");

            ChartTitle = new()
            {
                Text = "Przebieg zmian cen",
                TextSize = 25,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };
            OnPropertyChanged("ChartTitle");
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

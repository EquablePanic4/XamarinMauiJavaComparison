using CenyWPolsce.Data.Tables;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenyWPolsce.MAUI.Native.Models
{
    public class ProductPricingModel
    {
        public ProductPricingModel(Product product)
        {
            Date = product.Date;
            Price = product.Price;
        }

        public DateTime Date { get; init; }
        public double Price { get; set; }

        public string Month { get => Date.ToString("MM.yyyy"); }
        public string PriceText { get => $"{Math.Round(Price, 2)} zł"; }
    }
}

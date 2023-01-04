using CenyWPolsce.Data;
using CenyWPolsce.Data.Tables;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenyWPolsce.MAUI.Native.ViewModels
{
    class ProductsListViewModel
    {
        public List<string> Products { get; init; }

        public ProductsListViewModel()
        {
            var db = ApplicationDbContext.Instance;
            Products = db.Products.Select(s => s.Name).Distinct().OrderBy(o => o).ToList();
        }
    }
}

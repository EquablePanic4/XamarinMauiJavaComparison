using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenyWPolsce.MAUI.Native.ViewModels
{
    class ProductDetailsViewModel
    {
        public string ProductName { get; set; }

        public ProductDetailsViewModel(string productName)
        {
            ProductName = productName;
        }
    }
}

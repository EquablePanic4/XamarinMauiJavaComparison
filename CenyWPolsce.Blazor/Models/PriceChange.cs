using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ceny.Models
{
    public class PriceChange
    {
        public PriceChange()
        {

        }

        public PriceChange(string id, string nazwa, double startPrice, double endPrice)
        {
            ID = id;
            Nazwa = nazwa;
            StartPrice = startPrice;
            EndPrice = endPrice;
        }

        public string ID { get; set; }
        public string Nazwa { get; set; }
        public double StartPrice { get; set; }
        public double EndPrice { get; set; }

        private double _change;
        private bool _changeCalculated = false;
        public double Change
        {
            get
            { 
                if (_changeCalculated is false)
                {
                    _change = ((EndPrice - StartPrice) / StartPrice) * 100;
                    _changeCalculated = true;
                }
                    
                return _change;
            }
        }
    }
}

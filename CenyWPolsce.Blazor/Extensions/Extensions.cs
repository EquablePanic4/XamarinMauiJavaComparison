using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ceny.Extensions
{
    public static class Extensions
    {
        public static int MonthToInt(this string month)
            => month switch
            {
                "styczeń" => 1,
                "luty" => 2,
                "marzec" => 3,
                "kwiecień" => 4,
                "maj" => 5,
                "czerwiec" => 6,
                "lipiec" => 7,
                "sierpień" => 8,
                "wrzesień" => 9,
                "październik" => 10,
                "listopad" => 11,
                "grudzień" => 12,
                _ => throw new Exception($"Nieznana nazwa miesiąca: {month}")
            };
    }
}

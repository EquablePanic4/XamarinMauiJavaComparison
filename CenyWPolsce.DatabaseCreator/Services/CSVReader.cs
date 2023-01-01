using CenyWPolsce.Data.Tables;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenyWPolsce.DatabaseCreator.Services
{
    internal class CSVReader
    {
        public int Errors { get; private set; }
        public int Year { get; private set; }

        private readonly string _filePath;

        public CSVReader(string filePath)
        {
            _filePath = filePath;
            Year = Int32.Parse(Path.GetFileNameWithoutExtension(filePath));
        }

        public List<Product> ReadFile()
            => File.ReadLines(_filePath).Skip(1).Select(ReadCSVLine).Where(x => x.Price > 0).ToList();

        private Product ReadCSVLine(string line)
        {
            try
            {
                var arr = line.Split(';');
                return new()
                {
                    Date = new DateTime(Year, GetMonthNumber(arr[2]), 1),
                    Name = arr[3].Replace("\"", String.Empty),
                    Price = Double.Parse(arr[6]),
                };
            }

            catch
            {
                Errors++;
                return null;
            }
        }

        private int GetMonthNumber(string name)
            => name.Replace("\"", String.Empty) switch
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
                "grudzień" => 12
            };
    }
}

using Ceny.Models;
using Ceny.Services;
using ChartJs.Blazor.Common.Axes;
using ChartJs.Blazor.Common.Enums;
using ChartJs.Blazor.LineChart;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ceny.Pages
{
    partial class ProductDetails : ComponentBase
    {
        [Inject] PricesProvider pricesProvider { get; init; }

        [Parameter] public string productId { get; set; }

        private List<Produkt> produkty;
        private string nazwaProduktu;
        private LineConfig _config;

        protected override async Task OnInitializedAsync()
        {
            nazwaProduktu = (await pricesProvider.GetAll()).Where(e => e.Id == productId).FirstOrDefault().Nazwa;
            produkty = (from e in (await pricesProvider.GetAll()) where e.Nazwa == nazwaProduktu select e).ToList();

            _config = Config;
            AppendDataToChart();
        }

        private LineConfig Config
        {
            get => new LineConfig
            {
                Options = new()
                {
                    Title = new()
                    {
                        Display = true,
                        Text = $"Przebieg zmian cen dla produktu: {nazwaProduktu}"
                    },

                    Tooltips = new()
                    {
                        Mode = InteractionMode.Nearest,
                        Intersect = true
                    },

                    Hover = new()
                    {
                        Mode = InteractionMode.Nearest,
                        Intersect = true
                    },

                    Scales = new()
                    {
                        XAxes = new List<CartesianAxis>
                        {
                            new CategoryAxis
                            {
                                ScaleLabel = new()
                                {
                                    LabelString = "Miesiąc",
                                    Display = true
                                }
                            }
                        },

                        YAxes = new List<CartesianAxis>
                        {
                            new LinearCartesianAxis
                            {
                                ScaleLabel = new()
                                {
                                    LabelString = "Cena",
                                    Display = true
                                }
                            }
                        }
                    }
                }
            };
        }

        private void AppendDataToChart()
        {
            var values = new List<double>();

            produkty.ForEach(e =>
            {
                values.Add(e.Cena);
                _config.Data.Labels.Add(e.Data.ToString("MMM yyyy"));
            });

            _config.Data.Datasets.Add(new LineDataset<double>(values)
            {
                Label = nazwaProduktu,
                BackgroundColor = "#1A237E",
                BorderColor = "#1A237E",
                Fill = FillingMode.Disabled
            });
        }
    }
}
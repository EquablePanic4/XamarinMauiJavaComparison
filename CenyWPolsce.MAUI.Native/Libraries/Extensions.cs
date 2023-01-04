using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView.Maui;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenyWPolsce.MAUI.Native.Libraries
{
    public static class Extensions
    {
        public static void ConfigureXAxis(this CartesianChart chart, Action<ICartesianAxis> action)
        {
            var ax = chart.XAxes.First();
            action(ax);
        }
    }
}

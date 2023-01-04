using CenyWPolsce.Data;
using CenyWPolsce.MAUI.Native.ViewModels;

using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;

using SkiaSharp;

namespace CenyWPolsce.MAUI.Native.Pages;

public partial class DashboardPage : ContentPage
{
    private readonly DashboardViewModel _ctx = new();

    public DashboardPage()
	{
        InitializeComponent();

        BindingContext = _ctx;
    }
}
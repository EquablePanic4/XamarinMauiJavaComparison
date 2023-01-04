using CenyWPolsce.Data;
using CenyWPolsce.MAUI.Native.ViewModels;

namespace CenyWPolsce.MAUI.Native.Pages;

public partial class ProductDetails : ContentPage
{
	private  readonly ProductDetailsViewModel _ctx;

	public string Name { get; set; }

	public ProductDetails(string name)
	{
		InitializeComponent();

		_ctx = new(name);
		this.BindingContext = _ctx;
	}
}
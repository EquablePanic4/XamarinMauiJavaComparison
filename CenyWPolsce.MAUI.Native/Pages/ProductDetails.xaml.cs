namespace CenyWPolsce.MAUI.Native.Pages;

public partial class ProductDetails : ContentPage
{
	public ProductDetails(string name)
	{
		InitializeComponent();

		ProductName.Text = name;
	}
}
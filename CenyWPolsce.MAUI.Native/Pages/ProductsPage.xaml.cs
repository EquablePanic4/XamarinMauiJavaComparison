using CenyWPolsce.Data;
using CenyWPolsce.MAUI.Native.ViewModels;

using CommunityToolkit.Maui.Alerts;

namespace CenyWPolsce.MAUI.Native.Pages;

public partial class ProductsPage : ContentPage
{
	public ProductsPage()
	{
		BindingContext = new ProductsListViewModel();

		InitializeComponent();
	}

    private async void ProductsList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		var name = e.Item as string;
		//var toast = Toast.Make($"Wybrany produkt: {name}");
		//await toast.Show();

		await Navigation.PushAsync(new ProductDetails(name), true);
    }
}
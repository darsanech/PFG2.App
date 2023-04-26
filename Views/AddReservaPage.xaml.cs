namespace PFG2.Views;

using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using PFG2.Models;
using PFG2.ViewModel;
using System.Collections.ObjectModel;

public partial class AddReservaPage : ContentPage
{
    public AddReservaPage(AddReservaViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as AddReservaViewModel).OnLoad();
    }
    private void DeleteProd(object sender, EventArgs e)
    {
        var a = 2;
    }
    private void AddProd_Button(object sender, EventArgs e)
    {
        Label label = new Label()
        {
            Text = (BindingContext as AddReservaViewModel).ProducteName(),
            VerticalOptions= LayoutOptions.Center,
            HorizontalOptions= LayoutOptions.StartAndExpand,
        };
        Entry entry = new Entry()
        {
            Placeholder = "Codigo",
            WidthRequest = 100,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,

        };
        Button btn = new Button()
        {
            Text = "Delete",
            HorizontalOptions = LayoutOptions.EndAndExpand,
            VerticalOptions = LayoutOptions.Center,
        };
        btn.Clicked += new EventHandler(DeleteProd);
        StackLayout HSL = new StackLayout();
        HSL.Orientation = StackOrientation.Horizontal;
        HSL.VerticalOptions = LayoutOptions.Start;
        HSL.HorizontalOptions = LayoutOptions.Fill;
        HSL.HeightRequest = 60;
        HSL.Spacing = 10;
        HSL.Children.Add(label);
        HSL.Children.Add(entry);
        HSL.Children.Add(btn);

        ProductosStack.Children.Add(HSL);
        Productto.SelectedIndex = -1;
    }
}

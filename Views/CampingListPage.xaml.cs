using PFG2.Models;
using PFG2.ViewModel;
using System.Collections.ObjectModel;

namespace PFG2.Views;

public partial class CampingListPage : ContentPage
{
	public CampingListPage(CampingListModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        /*
        BEntregar.BackgroundColor = Color.Parse("Green");
        BOtros.BackgroundColor = Color.Parse("Gray");
        BRecoger.BackgroundColor = Color.Parse("Gray");
        */
        await (BindingContext as CampingListModel).OnLoad();
        ToggleEstado.IsVisible = (BindingContext as CampingListModel).Estado == null;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        BEntregar.BackgroundColor = Color.Parse("Gray");
        BOtros.BackgroundColor = Color.Parse("Gray");
        BRecoger.BackgroundColor = Color.Parse("Gray");
        var btn = (Button)sender;
        btn.BackgroundColor = Color.Parse("Green");
        await(BindingContext as CampingListModel).Filter(btn.Text);

    }
}
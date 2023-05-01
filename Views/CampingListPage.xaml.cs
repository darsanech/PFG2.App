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
        await (BindingContext as CampingListModel).OnLoad();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        BEntregar.BackgroundColor = Color.Parse("Gray");
        BOtros.BackgroundColor = Color.Parse("Gray");
        BRecoger.BackgroundColor = Color.Parse("Gray");

        BEntregar.IsEnabled = true;
        BOtros.IsEnabled = true;
        BRecoger.IsEnabled = true;
        var btn = (Button)sender;
        btn.BackgroundColor = Color.Parse("Green");
        btn.IsEnabled = false;
        await(BindingContext as CampingListModel).Filter(btn.Text);

    }
}
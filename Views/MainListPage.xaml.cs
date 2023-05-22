using PFG2.Models;
using PFG2.ViewModel;
using System.Collections.ObjectModel;

namespace PFG2.Views;

public partial class MainListPage : ContentPage
{
	public MainListPage(MainListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as MainListViewModel).OnLoad();
    }

}
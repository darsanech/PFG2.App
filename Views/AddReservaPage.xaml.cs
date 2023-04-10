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
}
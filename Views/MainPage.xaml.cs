using PFG2.Models;
using PFG2.Repositories;
using PFG2.Services;
using PFG2.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace PFG2;

public partial class MainPage : ContentPage
{

    public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm; 
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as MainPageViewModel).OnLoad();
    }

}


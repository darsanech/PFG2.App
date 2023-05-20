
using PFG2.ViewModel;
using System.Diagnostics;
using System.Reflection;

namespace PFG2.Views;

public partial class MapPage : ContentPage
{
    public MapPage(MapViewModel vm)
	{

        InitializeComponent();
        BindingContext = vm;

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as MapViewModel).OnLoad();
    }


}
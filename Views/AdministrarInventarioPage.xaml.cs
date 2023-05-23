using PFG2.ViewModel;

namespace PFG2.Views;

public partial class AdministrarInventarioPage : ContentPage
{
    public AdministrarInventarioPage(AdministrarInventarioViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as AdministrarInventarioViewModel).OnLoad();
    }

}
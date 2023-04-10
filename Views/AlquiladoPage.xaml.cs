using PFG2.ViewModel;

namespace PFG2.Views;

public partial class AlquiladoPage : ContentPage
{
	public AlquiladoPage(AlquiladoViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as AlquiladoViewModel).OnLoad();
    }
}
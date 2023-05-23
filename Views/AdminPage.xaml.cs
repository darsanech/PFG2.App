using PFG2.ViewModel;

namespace PFG2.Views;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

    }
}
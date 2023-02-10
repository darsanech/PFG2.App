using PFG2.ViewModel;

namespace PFG2;

public partial class MainListPage : ContentPage
{
	public MainListPage(MainListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
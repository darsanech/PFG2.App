using PFG2.ViewModel;

namespace PFG2;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm; 
	}

	
}


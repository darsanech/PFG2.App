namespace PFG2;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(MainListPage), typeof(MainListPage));
	}
}

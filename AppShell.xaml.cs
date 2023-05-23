using PFG2.Services;
using PFG2.Views;

namespace PFG2;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MainListPage), typeof(MainListPage));
        Routing.RegisterRoute(nameof(CampingListPage), typeof(CampingListPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(AddReservaPage), typeof(AddReservaPage));
        Routing.RegisterRoute(nameof(AlquiladoPage), typeof(AlquiladoPage));
        Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
        Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));
        Routing.RegisterRoute(nameof(AdministrarInventarioPage), typeof(AdministrarInventarioPage));
    }
}

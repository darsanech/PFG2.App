using PFG2.Views;
using PFG2.Services;
using PFG2.ViewModel;
using CommunityToolkit.Maui;

namespace PFG2;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


        builder.Services.AddSingleton<DataBaseService>();
        builder.Services.AddSingleton<LoginService>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();

        builder.Services.AddSingleton<MainListPage>();
		builder.Services.AddSingleton<MainListViewModel>();

        builder.Services.AddSingleton<CampingListPage>();
        builder.Services.AddSingleton<CampingListModel>();

        builder.Services.AddTransient<AlquiladoPage>();
        builder.Services.AddTransient<AlquiladoViewModel>();

		builder.Services.AddTransient<AddReservaPage>();
		builder.Services.AddTransient<AddReservaViewModel>();

        


        return builder.Build();
	}
}

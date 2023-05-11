using PFG2.Services;
using PFG2.Views;
using SQLite;
using System.Reflection;

namespace PFG2;

public partial class App : Application
{
    public static DataBaseService SetDataBaseService { get; set; }
    public static LoginService SetLoginService { get; set; }
    public static string Token { get; set; }
    public static int Userid { get; set; }

    public App(DataBaseService _dataBaseService, LoginService _loginService)
	{
		InitializeComponent();
        SetDataBaseService = _dataBaseService;
        SetLoginService = _loginService;

        MainPage = new AppShell();
    }
    
}

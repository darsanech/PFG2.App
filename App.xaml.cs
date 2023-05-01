using PFG2.Services;
using PFG2.Views;
using SQLite;
using System.Reflection;

namespace PFG2;

public partial class App : Application
{
    public static DataBaseService SetDataBaseService { get; set; }
    public App(DataBaseService _dataBaseService)
	{
		InitializeComponent();
        SetDataBaseService = _dataBaseService;
        
        MainPage = new AppShell();
    }
    
}

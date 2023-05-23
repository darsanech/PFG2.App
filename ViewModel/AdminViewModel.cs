namespace PFG2.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PFG2.Models;
using PFG2.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public partial class AdminViewModel : ObservableObject
{
	public AdminViewModel()
	{
		
	}
    [ICommand]
    async Task AdministrarUsuarios()
    {

    }
    [ICommand]
    async Task LocalizarRepartidores()
    {
        await Shell.Current.GoToAsync($"MapPage?Campingid=-1");
    }

    [ICommand]
    async Task AdministrarInventario()
    {
        await Shell.Current.GoToAsync($"AdministrarInventarioPage");

    }
}
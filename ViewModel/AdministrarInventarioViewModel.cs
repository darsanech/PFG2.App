using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic.FileIO;
using PFG2.Models;
using PFG2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace PFG2.ViewModel;

public partial class AdministrarInventarioViewModel : ObservableObject
{
    public ObservableCollection<Producto> ProductosList { get; } = new();
    public ObservableCollection<ProductoPH> ProductosPHList { get; } = new();

    bool newPage = true;

    public AdministrarInventarioViewModel()
	{
		
	}
	[ICommand]
    public async Task OnLoad()
    {
        ProductosPHList.Clear();
        await DataBaseService.UpdateProductosList();
        var prods = await DataBaseService.GetProductosList();
        foreach (var prod in prods)
        {
            ProductosPHList.Add((ProductoPH)prod);
        }
    }
    [ICommand]
    public async Task Modificar(ProductoPH ph)
    {
        var a = 2;
        await DataBaseService.ModProductos((Producto)ph,ph.mod);
        await Shell.Current.GoToAsync("..");

    }
}
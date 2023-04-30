using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic.FileIO;
using PFG2.Models;
using PFG2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.ViewModel
{
    public partial class AddReservaViewModel : ObservableObject
    {
        //Un puñao de variables
        #region
        public ObservableCollection<Estado> EstadosList { get; } = new();
        [ObservableProperty]
        int estado= -1;
        public ObservableCollection<Camping> CampingsList { get; } = new();
        [ObservableProperty]
        int camping = -1;
        public ObservableCollection<Producto> ProductosList { get; } = new();

        [ObservableProperty]
        int producto = -1;
        [ObservableProperty]
        string cliente;
        [ObservableProperty]
        string parcela;
        [ObservableProperty]
        int preu;
        [ObservableProperty]
        string extra;
        [ObservableProperty]
        DateTime dataIni= DateTime.Today;
        [ObservableProperty]
        DateTime dataFi= DateTime.Today;

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        bool newPage=true;

        List<string> ListaProductos=new List<string>();
        #endregion

        [ICommand]
        public async Task OnLoad()
        {
            if (newPage)
            {
                
                var camps = await DataBaseService.GetCampingsList();
                foreach (var camp in camps)
                {
                    CampingsList.Add(camp);
                }
                var prods = await DataBaseService.GetProductosList();
                foreach (var prod in prods)
                {
                    ProductosList.Add(prod);
                }
                var estados = await DataBaseService.GetEstadosList();
                foreach (var estado in estados)
                {
                    EstadosList.Add(estado);
                }
                newPage = false;
            }               
            
        }
        [ICommand]
        public async Task Upload()
        {
            
            #region "Compara las entradas"
            List<string> a = new List<String>();
            if (camping == -1)
            {
                a.Add("Camping no seleccionat");
            }
            if (estado== -1)
            {
                a.Add("Estado no seleccionat");
            }
            if (ListaProductos.Count()<=0)
            {
                a.Add("Producto no seleccionat");
            }
            if (DataIni > DataFi)
            {
                a.Add("Data incorrecta");
            }
            if (cliente == null)
            {
                a.Add("Falta nom del client");
            }
            if (parcela == null)
            {
                a.Add("Falta numero de parcela");
            }
            if (preu == 0)
            {
                a.Add("Falta el preu");
            }
            if (extra == null)
            {
                extra = "";
            }
            if (a.Count()>0)
            {
                await Application.Current.MainPage.DisplayAlert("A", string.Join(System.Environment.NewLine, a), "C");

            }
            #endregion
            else
            {

                Reserva newItem = new Reserva()
                {
                    clientename = cliente,
                    numeroparcela = parcela,
                    campingid = camping + 1,
                    productes = String.Join(", ", ListaProductos),
                    productescodes = "",
                    datainici = dataIni.ToString("dd/MM/yyyy"),
                    datafinal = dataFi.ToString("dd/MM/yyyy"),
                    Preu = preu,
                    estadoid = estado+1,
                    Extra = extra,
                    userid = 1,
                };
                try
                {
                    await DataBaseService.AddReserva(newItem);

                }
                catch(Exception ex) { 
                    var exp = ex; }
                await Shell.Current.GoToAsync("..");
                
            }


        }
        public void ModProductList(string productoname, bool suma)
        {
            if(suma)
                ListaProductos.Add(productoname);
            else
                ListaProductos.Remove(productoname);
        }
        public void RemoveProductList(string producte)
        {
            ListaProductos.Remove(producte);
        }
        public string ProducteName()
        {
            
            if (producto!=-1)
                return ProductosList[producto].productoname;
            else
                return "None";

        }

    }
}

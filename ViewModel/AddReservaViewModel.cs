using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        [ICommand]
        public async Task OnLoad()
        {
            if (newPage)
            {
                var prods = await DataBaseService.GetProductosList();
                foreach (var prod in prods)
                {
                    ProductosList.Add(prod);
                }
                var camps = await DataBaseService.GetCampingsList();
                foreach (var camp in camps)
                {
                    CampingsList.Add(camp);
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
            List<string> a = new List<String>();
            if (camping == -1)
            {
                a.Add("Camping no seleccionat");
            }
            if (estado== -1)
            {
                a.Add("Estado no seleccionat");
            }
            if (producto == -1)
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
            if(a.Count()>0)
            {
                await Application.Current.MainPage.DisplayAlert("A", string.Join(System.Environment.NewLine, a), "C");

            }
            else
            {
                Reserva newItem = new Reserva()
                {
                    Camping = CampingsList[camping].NomCamping,
                    Estado = EstadosList[estado].TipoEstado,
                    Producte = ProductosList[producto].NomProducte,
                    DataIni = dataIni.ToString("dd/MM/yyyy"),
                    DataFi = dataFi.ToString("dd/MM/yyyy"),
                    Cliente = cliente,
                    Parcela = parcela,
                    Preu = preu,
                    Extra = extra,
                };
                await DataBaseService.AddReserva(newItem);
                await Shell.Current.GoToAsync("..");
            }


        }
    }
}

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
    [QueryProperty("Campingid", "Campingid")]
    [QueryProperty("Estado", "Estado")]
    [QueryProperty("Parcela", "Parcela")]
    [QueryProperty("Datainici", "Datainici")]
    [QueryProperty("Datafinal", "Datafinal")]

    public partial class CampingListModel : ObservableObject
    {

        public ObservableCollection<ReservasLista> ReservasList { get; } = new();
        public ObservableCollection<ReservasLista> ReservasListFiltered { get; } = new();

        [ObservableProperty]
        string estado=null;
        [ObservableProperty]
        string parcela = null;
        [ObservableProperty]
        string datainici = null;
        [ObservableProperty]
        string datafinal = null;

        [ObservableProperty]
        int campingid;
        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string listaActual= "Entregar";
        [ObservableProperty]
        string nextStep = "Alquilado";
        [ObservableProperty]
        bool newPage;

        [ICommand]
        public async Task OnLoad()
        {
            if (estado!=null)
            {
                
                var reservas = await DataBaseService.GetReservasFilterKnownEstadoList(campingid, parcela, 2, datainici, datafinal);
                ReservasListFiltered.Clear();
                foreach (var reserva in reservas)
                {
                    ReservasListFiltered.Add(reserva);
                }
            }
            else if (ReservasList.Count == 0 || ReservasList.First().campingid != campingid)
            {
                newPage = true;
                await Refresh();

            }
        }

        [ICommand]
        public async Task Refresh()
        {
            //que el refresh lo elimine todo me parece xd
            //podria mirar si se ha updateado la general, (el numero ha cambiado)
            //pero puede que tambien cambiara el nuestro
            newPage = true;

            if (ReservasList.Count != 0)
            {
                ReservasList.Clear();
            }
            var reservas = await DataBaseService.GetReservasList(campingid);
            foreach(var reserva in reservas)
            {
                ReservasList.Add(reserva);
            }
            await Filter(listaActual);
            IsRefreshing = false;
            newPage = false;

        }
        [ICommand]
        public async Task Filter(string Filtro)
        {
            if(newPage || listaActual != Filtro)
            {
                listaActual = Filtro;
                switch (Filtro)
                {
                    case "Recoger":
                        nextStep = "Finalizado";
                        break;
                    default:
                        nextStep = "Alquilado";
                        break;
                }
                ReservasListFiltered.Clear();
                var reservas = ReservasList.Where(x => x.estadoname == listaActual);
                foreach (var reserva in reservas)
                {
                    ReservasListFiltered.Add(reserva);
                }
                isRefreshing = true;
            }
        }
        [ICommand]
        async Task AddButton()
        {
            await Shell.Current.GoToAsync("AddReservaPage");
            //no le da al refresh de golpe
        }
        [ICommand]
        public async void ChangeType(ReservasLista aReserva)
        {
            //primero me gustaria tener una manera mejor de crear la db y editarla antes de ir moviendo cosas.
            Reserva res = await DataBaseService.GetReservabyId(aReserva.idreserva);
            await DataBaseService.NextStep(res);
            await Refresh();
        }

    }

}

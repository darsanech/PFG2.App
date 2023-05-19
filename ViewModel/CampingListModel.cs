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
        string camping;
        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string listaActual= "Entregar";
        [ObservableProperty]
        string nextStep = "Alquilado";
        [ObservableProperty]
        string nextStepL = "Pagar";

        [ObservableProperty]
        bool campingList;

        [ICommand]
        public async Task OnLoad()
        {
            CampingList = true;
            Camping = await DataBaseService.GetCampingName(campingid);
            //Check si hace falta update
            var ok = await DataBaseService.CheckUpdate(campingid);
            if (estado!=null)
            {
                CampingList = false;
                var reservas = await DataBaseService.GetReservasFilterKnownEstadoList(campingid, parcela, Int32.Parse(estado), datainici, datafinal);
                ReservasListFiltered.Clear();
                foreach (var reserva in reservas)
                {
                    ReservasListFiltered.Add(reserva);
                }
            }
            else
            {
                await Refresh();
            }
        }

        [ICommand]
        public async Task Refresh()
        {
            //que el refresh lo elimine todo me parece xd
            //podria mirar si se ha updateado la general, (el numero ha cambiado)
            //pero puede que tambien cambiara el nuestro
            if (ReservasList.Count != 0)
            {
                ReservasList.Clear();
            }
            var reservas = await DataBaseService.GetReservasListEntregaRecogerOtros(campingid);
            foreach(var reserva in reservas)
            {
                ReservasList.Add(reserva);
            }
            await Filter(listaActual);
            IsRefreshing = false;

        }
        [ICommand]
        public async Task Filter(string Filtro)
        {
            listaActual = Filtro;
            switch (Filtro)
            {
                case "Recoger":
                    NextStep = "Finalizado";
                    NextStepL = "Finalizado";
                    break;
                case "Otros":
                    NextStep = "Alquilado";
                    NextStepL = "Alquilado";
                    break;
                default:
                    NextStep = "Alquilado";
                    NextStepL = "Pagar";
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
        [ICommand]
        async Task AddButton()
        {
            await Shell.Current.GoToAsync("AddReservaPage");
        }
        [ICommand]
        public async void SiguientePaso(ReservasLista aReserva)
        {
            Reserva res = await DataBaseService.GetReservabyId(aReserva.idreserva);
            await DataBaseService.ChangeStep(res, false);
            await Refresh();
        }
        [ICommand]
        public async void APagar(ReservasLista aReserva)
        {
            Reserva res = await DataBaseService.GetReservabyId(aReserva.idreserva);
            await DataBaseService.ChangeStep(res, listaActual == "Entregar");
            await Refresh();
        }
        [ICommand]
        public async void EditProd(ReservasLista aReserva)
        {
            await Shell.Current.GoToAsync($"AddReservaPage?Idres={aReserva.idreserva}");
        }
        public string stringtest(string str)
        {
            return str + "!";
        }
    }

}

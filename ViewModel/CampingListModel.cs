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
        [ObservableProperty]
        bool loading;
        [ObservableProperty]
        bool loaded;

        [ICommand]
        public async Task OnLoad()
        {
            CampingList = true;
            Loading = true;
            Loaded = false;
            Camping = await DataBaseService.GetCampingName(campingid);
            //Check si hace falta update
            if (estado!=null)
            {
                CampingList = false;
                var ok = await DataBaseService.CheckUpdate(campingid);
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
            Loaded = true;
            Loading = false;

        }
        [ICommand]
        public async Task Refresh()
        {
            Loading = true;
            Loaded = false;
            var ok = await DataBaseService.CheckUpdate(campingid);
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
            Loaded = true;
            Loading = false;
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
            IEnumerable<ReservasLista> reservas;
            if (listaActual == "Otros")
            {
                reservas = ReservasList.Where(x => x.estadoid>4);
            }
            else
            {
                reservas = ReservasList.Where(x => x.estadoname == listaActual);
            }
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
        async Task MapButton()
        {
            await Shell.Current.GoToAsync($"MapPage?Campingid={campingid}");
        }
        [ICommand]
        public async void SiguientePaso(ReservasLista aReserva)
        {
            Loading = true;
            Loaded = false;
            Reserva res = await DataBaseService.GetReservabyId(aReserva.idreserva);
            await DataBaseService.ChangeStep(res, false);
            await Refresh();
            Loaded = true;
            Loading = false;
        }
        [ICommand]
        public async void APagar(ReservasLista aReserva)
        {
            Loading = true;
            Loaded = false;
            Reserva res = await DataBaseService.GetReservabyId(aReserva.idreserva);
            await DataBaseService.ChangeStep(res, listaActual == "Entregar");
            await Refresh();
            Loaded = true;
            Loading = false;
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

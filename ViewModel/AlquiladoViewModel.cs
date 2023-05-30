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
    public partial class AlquiladoViewModel : ObservableObject
    {

        public ObservableCollection<Camping> CampingsList { get; } = new();
        public ObservableCollection<Estado> EstadoList { get; } = new();


        public static ObservableCollection<ReservasLista> ReservasList;

        [ObservableProperty]
        string titulo;
        [ObservableProperty]
        int camping = -1;
        [ObservableProperty]
        int estado = -1;
        [ObservableProperty]
        bool newPage = true;

        [ObservableProperty]
        string parcela;
        [ObservableProperty]
        string cliente;
        [ObservableProperty]
        DateTime dataIni = DateTime.Today;
        [ObservableProperty]
        DateTime dataFi = DateTime.Today;
        [ObservableProperty]
        bool iniCheck=false;
        [ObservableProperty]
        bool fiCheck = false;

        public static ObservableCollection<ReservasLista> GetReservasList()
        {
            return ReservasList;
        }

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
                var ests = await DataBaseService.GetEstadosList();
                foreach (var est in ests)
                {
                    EstadoList.Add(est);
                }
                newPage = false;
            }

        }
        [ICommand]
        public async Task Search()
        {
            if (camping == -1 || estado==-1)
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Es necesita minim el Camping y el Esatdo", "Ok!");
            }
            else
            {
                string dataIniStr = null, dataFiStr=null;
                if (iniCheck)
                {
                    dataIniStr = DataIni.ToString("dd/MM/yyyy");
                }
                if (fiCheck)
                {
                    dataFiStr = DataFi.ToString("dd/MM/yyyy");
                }
                if (parcela == "")
                {
                    parcela = null;
                }
                if (cliente == "")
                {
                    cliente = null;
                }
                //var ReservasList = await DataBaseService.GetReservasFilterKnownEstadoList(CampingsList[camping].campingid, parcela, estado, dataIniStr, dataFiStr);
                await Shell.Current.GoToAsync($"CampingListPage?Estado={EstadoList[estado].estadoid}&Parcela={parcela}&Cliente={cliente}&Datafinal={dataFiStr}&Datainici={dataIniStr}&Campingid={CampingsList[camping].campingid}");
            }
        }
    }
}

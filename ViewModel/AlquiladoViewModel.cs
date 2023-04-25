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
        [ObservableProperty]
        int camping = -1;
        [ObservableProperty]
        bool newPage = true;

        [ObservableProperty]
        string parcela;
        [ObservableProperty]
        DateTime dataIni = DateTime.Today;
        [ObservableProperty]
        DateTime dataFi = DateTime.Today;
        [ObservableProperty]
        bool iniCheck=false;
        [ObservableProperty]
        bool fiCheck = false;

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
                newPage = false;
            }

        }
        [ICommand]
        public async Task Search()
        {
            if (camping == -1)
            {
                await Application.Current.MainPage.DisplayAlert("A", "Es necesita minim el Camping", "C");
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
                var res = await DataBaseService.GetReservasFilterKnownEstadoList(CampingsList[camping].campingid, parcela, 2, dataIniStr, dataFiStr);
                var a = 2;
            }


        }
    }
}

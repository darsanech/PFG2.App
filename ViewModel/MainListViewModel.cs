using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PFG2.Models;
using PFG2.Repositories;
using PFG2.Services;

namespace PFG2.ViewModel
{
    

    public partial class MainListViewModel : ObservableObject
    {
        public ObservableCollection<Reserva> ReservasList { get; }=new();
        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string textaco;

        public string Camping { get; set; }


        public MainListViewModel()
        {

        }
        [ICommand]
        async Task BackButton()
        {
            await SecureStorage.SetAsync("JwtToken", "");
            await Shell.Current.GoToAsync("..");
        }
        [ICommand]
        async Task ClickedButton(string camping)
        {
            await Shell.Current.GoToAsync($"CampingListPage?Campingid={camping}");
        }
        [ICommand]
        async Task ClickedAlquilado(string estado)
        {
            await Shell.Current.GoToAsync($"AlquiladoPage?Estado={Int32.Parse(estado)}");
        }
        [ICommand]
        async Task ClickedMap()
        {
            await Shell.Current.GoToAsync($"MapPage");
        }


    }
}

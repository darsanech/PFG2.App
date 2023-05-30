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
        bool isAdmin;
        [ObservableProperty]
        bool newPage = true;
        public string Camping { get; set; }


        public MainListViewModel()
        {

        }
        [ICommand]
        public async Task OnLoad()
        {
            if (newPage)
            {
                IsAdmin = await SecureStorage.GetAsync("Rol") == "0";
                newPage = false;
            }

        }
        [ICommand]
        async Task BackButton()
        {
            newPage = true;
            await SecureStorage.SetAsync("Expiration", "");
            await Shell.Current.GoToAsync("..");
        }
        [ICommand]
        async Task ClickedButton(string camping)
        {

            await Shell.Current.GoToAsync($"CampingListPage?Campingid={camping}");
        }
        [ICommand]
        async Task ClickedAlquilado()
        {
            await Shell.Current.GoToAsync($"AlquiladoPage");
        }
        [ICommand]
        async Task ClickedAdmin()
        {
            await Shell.Current.GoToAsync($"AdminPage");
        }


    }
}

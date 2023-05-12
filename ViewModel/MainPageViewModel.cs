using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PFG2.Services;
using System.Diagnostics;

namespace PFG2.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;
        [ICommand]

        public async Task OnLoad()
        {
            try
            {
                var session = await SecureStorage.GetAsync("JwtToken");
                if (session != null && session != "")
                {
                    await Shell.Current.GoToAsync("MainListPage");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
            }

        }

        [ICommand]
        async Task CheckCredentials()
        {
            var res= await LoginService.Login(username, password);
            if(res!=null)
            {
                await DataBaseService.GetDB();
                await DataBaseService.InitDatabase();
                //var reservas = await DataBaseService.GetReservasList();
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
                await Shell.Current.GoToAsync("MainListPage");

            }

        }
    }
}

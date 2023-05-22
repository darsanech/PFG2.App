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

        [ObservableProperty]
        bool loading;
        [ObservableProperty]
        bool loaded;

        [ICommand]
        public async Task OnLoad()
        {
            try
            {
                Loading = true;
                Loaded = false;
                var session = await SecureStorage.GetAsync("JwtToken");
                Loading = false;
                Loaded = true;
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
            Loading = true;
            Loaded = false;
            var res= await LoginService.Login(username, password);
            if(res!=null)
            {
                await DataBaseService.GetDB();
                await DataBaseService.InitDatabase();
                //var reservas = await DataBaseService.GetReservasList();
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData2.db");
                Loading = false;
                Loaded = true;
                await Shell.Current.GoToAsync("MainListPage");
            }

        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PFG2.Services;

namespace PFG2.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        [ICommand]
        async Task CheckCredentials()
        {
            await DataBaseService.GetDB();
            await DataBaseService.InitDatabase();
            //var reservas = await DataBaseService.GetReservasList();
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            await Shell.Current.GoToAsync("MainListPage");
            
            if (username == "A")
            {                
            }

        }
    }
}

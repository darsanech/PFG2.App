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
                string expira = await SecureStorage.GetAsync("Expiration");
                if (expira != null && expira != "")
                {
                    Loading = false;
                    Loaded = true;
                    if (DateTime.Now.CompareTo(DateTime.Parse(expira)) > 0)
                    {
                        await SecureStorage.SetAsync("Expiration", "");
                    }
                    else
                    {
                        await Shell.Current.GoToAsync("MainListPage");
                    }
                }
                Loading = false;
                Loaded = true;
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
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData2.db");
                if (!File.Exists(databasePath))
                {
                    await DataBaseService.GetDB();
                    await DataBaseService.InitDatabase();
                }
                Loading = false;
                Loaded = true;
                await Shell.Current.GoToAsync("MainListPage");
            }

        }
    }
}

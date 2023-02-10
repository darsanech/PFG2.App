using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            if (username == "A")
            {                
                await Shell.Current.GoToAsync(nameof(MainListPage));
            }

        }
    }
}

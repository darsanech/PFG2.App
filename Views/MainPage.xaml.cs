using PFG2.Models;
using PFG2.Repositories;
using PFG2.Services;
using PFG2.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace PFG2;

public partial class MainPage : ContentPage
{
	int count = 0;
    public ObservableCollection<Camping> ReservasList { get; } = new();


    public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();
        /*
		var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
		using (Stream stream = assembly.GetManifestResourceStream("PFG2.Models.User.cs"))
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				File.WriteAllBytes(DataBaseService.databasePath, memoryStream.ToArray());

            }
		}
		*/
        BindingContext = vm; 
	}

	
}


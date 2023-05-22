using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PFG2.Models;
using PFG2.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.ViewModel
{
    [QueryProperty("Campingid", "Campingid")]

    public partial class MapViewModel : ObservableObject
    {
        [ObservableProperty]
        string salsa;
        [ObservableProperty]
        int campingid;
        public ObservableCollection<Parcela> ParcelaList { get; } = new();

        IGeolocation geolocation;

        public MapViewModel(IGeolocation geolocation)
        {
            this.geolocation = geolocation;
        }
        [ICommand]
        public async void OnLoad()
        {
            var marker = "";
            try
            {
                var location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
                marker = "var marker = L.marker([" + location.Latitude.ToString().Replace(',', '.') + ", " + location.Longitude.ToString().Replace(',', '.') + "]).addTo(map);" + Environment.NewLine;

            }
            catch (Exception ex) { 
                
            }
            string addon = "";
            var parc = await DataBaseService.ShowMap(campingid);
            int i = 0;
            foreach (var par in parc)
            {
                addon += "var polygon" +par.parcelaid+"=L.polygon(";
                addon += par.geometryy;
                addon += ",{color:'"+estadotocolor(par.estadoid);
                addon += "'}).addTo(map).bindPopup('"+par.numeroparcela+"');" + Environment.NewLine;
                i++;
            }
            addon += marker;
            Salsa = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
<meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' name='viewport' />
<meta name=""viewport"" content=""width=device-width"" />

    <link rel=""stylesheet"" href=""https://unpkg.com/leaflet@1.9.4/dist/leaflet.css""/>
    <title>Mapa</title>
</head>
<style>
    #map{
        height: 800px;
        width: 800px;
    }
</style>
<body>
    <div id=""map""></div>
    <script src=""https://unpkg.com/leaflet@1.9.4/dist/leaflet.js""></script>
    <script>
        var map = L.map('map').setView([42.189371, 3.107249], 18);
        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href=""http://www.openstreetmap.org/copyright"">OpenStreetMap</a>'
        }).addTo(map);
";
            Salsa += addon;
            Salsa += @"
    </script>

</body>
</html";

        }
        public string estadotocolor(int estadoid)
        {
            string color = "";
            switch (estadoid)
            {

                case 1:
                    color = "green";
                    break;
                case 2:
                    color = "grey";
                    break;
                case 3:
                    color = "red";
                    break;
                case 4:
                    color = "purple";
                    break;
                case 5:
                    color = "yellow";
                    break;
                case 6:
                    color = "purple";
                    break;
                case 7:
                    color = "purple";
                    break;
            }
            return color;
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.ViewModel
{
    public partial class MapViewModel : ObservableObject
    {
        [ObservableProperty]
        string salsa;
        [ICommand]
        public void OnLoad()
        {
            Salsa = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <link rel=""stylesheet"" href=""https://unpkg.com/leaflet@1.9.4/dist/leaflet.css""/>
    <title>Mapa</title>
</head>
<style>
    #map{
        height: 800px;
        width: 800px;
        margin:5rem auto
    }
</style>
>";
            Salsa += @"<body>
    <p>Hola como estas</p>
    <div id=""map""></div>


    <script src=""https://unpkg.com/leaflet@1.9.4/dist/leaflet.js""></script>
    <script>
        var map = L.map('map').setView([42.189371, 3.107249], 18);
        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href=""http://www.openstreetmap.org/copyright"">OpenStreetMap</a>'
        }).addTo(map);
        let a=[3.117239,3.004259,2.907759];
        for(let i=0;i<3;i++){
        var marker=L.marker([42.189371, a[i]]).addTo(map);
        }
        var polygon = L.polygon([
            [42.18939, 3.117239],
            [42.129371, 3.117239],
            [42.189971, 3.017239]
        ]).addTo(map);
    </script>

</body>
</html";

        }
    }
}

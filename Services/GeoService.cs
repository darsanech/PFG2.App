using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Services
{
    public class GeoService
    {

        public static IGeolocation geolocation { get; set; }
        public GeoService(IGeolocation _geolocation) {
            geolocation = _geolocation;
        }

        public static async Task<string> GetUbicacionString()
        {
            try
            {
                var location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
                return "[" + location.Latitude.ToString().Replace(',', '.') + ", " + location.Longitude.ToString().Replace(',', '.') + "]";

            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }

}

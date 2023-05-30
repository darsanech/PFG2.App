using Newtonsoft.Json;
using PFG2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Services
{
    public class LoginService
    {
        static string BaseUrl = "https://pfgws.azurewebsites.net";

        static HttpClient client;
        public LoginService()
        {
            try
            {
                client = new HttpClient
                {
                    BaseAddress = new Uri(BaseUrl)
                };
            }
            catch
            {

            }
        }
        
        public static async Task<string> Login(string username, string password)
        {
            try
            {
                var conn = Connectivity.NetworkAccess;
                if (conn == NetworkAccess.Internet)
                {
                    var response = await client.GetAsync(BaseUrl+$"/api/Login?usuario={username}&pass={password}");
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var bt = JsonConvert.DeserializeObject<BearerToken>(json);
                        await SecureStorage.SetAsync("JwtToken", bt.token);
                        await SecureStorage.SetAsync("Userid", bt.userId.ToString());
                        await SecureStorage.SetAsync("Rol", bt.Rol.ToString());
                        await SecureStorage.SetAsync("Expiration", DateTime.Now.AddMinutes(bt.expirationInMinutes).ToString());
                        return "Ok";
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Atencion!", "Usuario o contraseña incorrectos", "Salir");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Atencion!", "No hay conexion", "Salir");
                }
                return null;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error {ex}");
                return null;
            }
        }
    }
}

using PFG2.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.MySql;
using Dotmim.Sync.SqlServer;
using Dotmim.Sync;
using Microsoft.Data.Sqlite;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.Maui.Graphics.Text;
using Newtonsoft.Json.Linq;
using Azure;
using PFG2.Views;
using SQLiteNetExtensionsAsync.Extensions;

namespace PFG2.Services
{
    public class DataBaseService
    {
        static SQLiteAsyncConnection db;
        static SQLiteAsyncConnection dbp;

        public static string databasePath { get; set; }


        static string BaseUrl = "https://pfgws.azurewebsites.net";
        static HttpClient client;

        public DataBaseService()
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
            Engage();
        }
        public static async void Engage()
        {
            await Init();
            
;        }
        static async Task Init()
        {
            if (db != null && dbp!=null)
                return;

            // Get an absolute path to the database file
            
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData2.db");
            var databasePendiente = Path.Combine(FileSystem.AppDataDirectory, "MyDataPendiente.db");
            try
            {
                if (!File.Exists(databasePendiente))
                {
                    var fileInfo = new FileInfo(databasePendiente);
                }
                dbp = new SQLiteAsyncConnection(databasePendiente);
                await dbp.CreateTableAsync<ReservaPendiente>();
                db = new SQLiteAsyncConnection(databasePath);
                await db.CreateTableAsync<Reserva>();
                await db.CreateTableAsync<Camping>();
                await db.CreateTableAsync<Estado>();
                await db.CreateTableAsync<Producto>();
                await db.CreateTableAsync<User>();
                await db.CreateTableAsync<Suscripcion>();
                string text = File.ReadAllText(databasePendiente);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
            }
        }
        public static async Task InitDatabase()
        {
            await Init();
        }
        public static async Task GetDB()
        {
            await AuthHeader();

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData2.db");
            try
            {
                var Response = await client.GetAsync(BaseUrl + $"/api/Sync");
                using (var stream = await Response.Content.ReadAsStreamAsync())
                {
                    var fileInfo = new FileInfo(Path.Combine(FileSystem.AppDataDirectory, "MyData2.db"));
                    using (var fileStream = fileInfo.OpenWrite())
                    {
                        await stream.CopyToAsync(fileStream);
                    }
                }
                string text = File.ReadAllText(databasePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
            }
        }
        
        public static async Task<IEnumerable<User>> GetUsersList()
        {
            await Init();

            var query = await db.Table<User>().ToListAsync();
            return query;
        }
        public static async Task<Reserva> GetReservabyId(int reservaId)
        {
            await Init();

            var query = await db.QueryAsync<Reserva>("select * from Reserva Where idreserva=" + reservaId + ";");

            //var query = await db.Table<Reserva>().Where(x => x.campingid == campingid).ToListAsync();

            return query.FirstOrDefault();
        }
        public static async Task<ReservasLista> GetReservaListabyId(int reservaId)
        {
            await Init();
            var query = await db.QueryAsync<ReservasLista>("select r.idreserva, r.clientename, r.numeroparcela, r.campingid, c.campingname, r.productes, " +
                "r.productescodes, r.datainici, r.datafinal, r.preu, r.estadoid, e.estadoname, r.extra " +
                "from Reserva as r inner join Camping as c on c.campingid=r.campingid inner join Estado as e on e.estadoid = r.estadoid Where r.idreserva=" + reservaId.ToString());

            return query.FirstOrDefault();
        }
        public static async Task<IEnumerable<ReservasLista>> GetReservasListEntregaRecogerOtros(int campingid)
        {
            await Init();

            var query= await db.QueryAsync<ReservasLista>("select r.idreserva, r.clientename, r.numeroparcela, r.campingid, c.campingname, r.productes, " +
                "r.productescodes, r.datainici, r.datafinal, r.preu, r.estadoid, e.estadoname, r.extra " +
                "from Reserva as r inner join Camping as c on c.campingid=r.campingid inner join Estado as e on e.estadoid = r.estadoid " +
                "Where r.campingid=" + campingid.ToString()+ " and (r.estadoid=1 or r.estadoid=3 or r.estadoid=5 or r.estadoid=6)");
            

            //var query = await db.Table<Reserva>().Where(x => x.campingid == campingid).ToListAsync();

            return query;
        }
        public static async Task<IEnumerable<ReservasLista>> GetReservasFilterKnownEstadoList(int campingid,string parcela, int estadoid, string cliente, string dataini, string datafi)
        {
            await Init();

            //var query = await db.Table<Reserva>().Where(x => x.Camping == camping && x.Estado==estado).ToListAsync();
            var query = await db.QueryAsync<ReservasLista>("select r.idreserva, r.clientename, r.numeroparcela, r.campingid, c.campingname, r.productes, " +
                "r.productescodes, r.datainici, r.datafinal, r.preu, r.estadoid, e.estadoname, r.extra " +
                "from Reserva as r inner join Camping as c on c.campingid=r.campingid inner join Estado as e on e.estadoid = r.estadoid " +
                "Where r.campingid=" + campingid.ToString()+" and r.estadoid="+estadoid.ToString());

            if (parcela!="")
            {
                query = query.Where(x => x.numeroparcela == parcela).ToList();
            }
            if (dataini != "")
            {
                query = query.Where(x => x.datainici == dataini).ToList();
            }
            if (datafi != "")
            {
                query = query.Where(x => x.datafinal == datafi).ToList();
            }
            if (cliente != "")
            {
                query = query.Where(x => x.clientename.Contains(cliente)).ToList();
            }
            return query;
        }
        public static async Task<IEnumerable<Camping>> GetCampingsList()
        {
            await Init();

            var query = await db.Table<Camping>().ToListAsync();
            return query;
        }
        public static async Task<string> PutUbi()
        {

            await AuthHeader();
            string ownubi = await GeoService.GetUbicacionString();
            if(ownubi != "") {
                var response = await client.PutAsync(BaseUrl + "/api/User/PutUbi?newUbi=" + ownubi, null);
            }
            return ownubi;
        }
        public static async Task<bool> CheckUpdate(int campid)
        {
            try {
                await Init();
                var conn = Connectivity.NetworkAccess;

                if (conn == NetworkAccess.Internet)
                {
                    //await PutUbi();
                    await AuthHeader();
                    await UploadPendiente();
                    var query = await dbp.Table<ReservaPendiente>().ToListAsync();
                    if (query.Count() == 0)
                    {
                        string needed = await GetUpdate(campid);
                        if (needed == "true")
                        {
                            var queryB = await client.GetAsync(BaseUrl + $"/api/Reserva/GetCamping?campid=" + campid.ToString());
                            var contents = queryB.Content.ReadAsStringAsync().Result;
                            IEnumerable<Reserva> res = JsonConvert.DeserializeObject<IEnumerable<Reserva>>(contents);
                            await db.InsertOrReplaceAllWithChildrenAsync(res);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                var a = ex;
                return false;
            }


        }
        public static async Task<bool> UpdateProductosList()
        {
            try
            {
                await Init();
                var conn = Connectivity.NetworkAccess;

                if (conn == NetworkAccess.Internet)
                {
                    await AuthHeader();
                    await UploadPendiente();
                    var query = await dbp.Table<ReservaPendiente>().ToListAsync();
                    if (query.Count() == 0)
                    {
                        var queryB = await client.GetAsync(BaseUrl + $"/api/Producto");
                        var contents = queryB.Content.ReadAsStringAsync().Result;
                        IEnumerable<Producto> res = JsonConvert.DeserializeObject<IEnumerable<Producto>>(contents);
                        await db.InsertOrReplaceAllWithChildrenAsync(res);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                var a = ex;
                return false;
            }
        }
        public static async Task<IEnumerable<Producto>> GetProductosList()
        {
            await Init();
            var query = await db.Table<Producto>().ToListAsync();
            return query;
        }
        public static async Task ModProductos(Producto p, int mod)
        {
            await Init();
            await AuthHeader();
            var response = await client.PutAsync(BaseUrl + "/api/Reserva?producteid="+p.producteid+ "&mod="+mod+"&total=true&sale=true", null);
            await db.UpdateAsync(p);
        }
        public static async Task<IEnumerable<Estado>> GetEstadosList()
        {
            await Init();
            var query = await db.Table<Estado>().ToListAsync();
            return query;
        }
        public static async Task AddReserva(Reserva nReserva)
        {
            try
            {
                await Init();
                var conn = Connectivity.NetworkAccess;
                if (conn == NetworkAccess.Internet)
                {
                    await AuthHeader();
                    var data = JsonConvert.SerializeObject(nReserva);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(BaseUrl + "/api/Reserva", content);
                    var nid = response.Content.ReadAsStringAsync().Result;
                    nReserva.idreserva = Int32.Parse(nid);
                    await db.InsertAsync(nReserva); 
                }
                else
                {
                    var id = await db.InsertAsync(nReserva);
                    await dbp.InsertAsync((ReservaPendiente)nReserva);
                    var query = await dbp.Table<ReservaPendiente>().ToListAsync();
                }


            }
            catch (Exception ex)
            {
                var a = ex;
            }
        }
        public static async Task UpdateReserva(Reserva nReserva)
        {
            try
            {
                await Init();
                var conn = Connectivity.NetworkAccess;
                if (conn == NetworkAccess.Internet){
                    await AuthHeader();
                    var data = JsonConvert.SerializeObject(nReserva);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(BaseUrl + "/api/Reserva", content);
                    var id = await db.UpdateAsync(nReserva);
                }
                else
                {
                    var id = await db.UpdateAsync(nReserva);
                    ReservaPendiente np = (ReservaPendiente)nReserva;
                    var res = await dbp.Table<ReservaPendiente>().Where(x => x.idreserva == nReserva.idreserva).ToListAsync();
                    if (res.Count()==0)
                    {
                        np.type = true;
                        await dbp.InsertAsync(np);
                    }
                    else
                    {
                        await dbp.UpdateAsync(np);
                    }
                    var query = await dbp.Table<ReservaPendiente>().ToListAsync();
                }

            }
            catch (System.Exception ex)
            {
                var a = ex;
            }
        }
        public static async Task UploadPendiente()
        {
            var query = await dbp.Table<ReservaPendiente>().ToListAsync();
            var conn = Connectivity.NetworkAccess;
            Reserva up, old;
            while (query.Count()> 0 && conn == NetworkAccess.Internet)
            {
                up = (Reserva)query[0];
                old= (Reserva)query[0];
                if (query[0].type) //Put
                {
                    await UpdateReserva(up);
                }
                else //Post
                {
                    var a=await db.DeleteAsync(old);
                    up.idreserva = 0;
                    await AddReserva(up);
                }
                var b = await dbp.DeleteAsync(query[0]);
                query.RemoveAt(0);
                conn = Connectivity.NetworkAccess;
            }
        }
        public static async Task ChangeStep(Reserva nReserva, bool pagar)
        {
            await Init();

            if (pagar)
            {
                nReserva.estadoid = 6;

            }
            else if (nReserva.estadoid == 5)
            {
                nReserva.estadoid = 2;
            }
            else
            {
                nReserva.estadoid += 1;
            }
            await UpdateReserva(nReserva);
        }
        //
        public static async Task<IEnumerable<Parcela>> ShowMap(int campingId)
        {
            await AuthHeader();
            var conn = Connectivity.NetworkAccess;
            if(conn == NetworkAccess.Internet) { }
            var queryB = await client.GetAsync(BaseUrl + $"/api/Parcela?campingid=" + campingId.ToString()); 
            var contents = queryB.Content.ReadAsStringAsync().Result;
            IEnumerable<Parcela> res = JsonConvert.DeserializeObject<IEnumerable<Parcela>>(contents);
            //await db.InsertOrReplaceAllWithChildrenAsync(res);
            return res;
        }
        public static async Task<IEnumerable<User>> ShowRep()
        {
            try
            {
                await AuthHeader();
                var queryB = await client.GetAsync(BaseUrl + $"/api/User/GetUbi");
                var contents = queryB.Content.ReadAsStringAsync().Result;
                IEnumerable<User> res = JsonConvert.DeserializeObject<IEnumerable<User>>(contents);
                //await db.InsertOrReplaceAllWithChildrenAsync(res);
                return res;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }
        public static async Task<string> CampingUbi(int campingId)
        {
            await Init();
            var res = await db.Table<Camping>().Where(x => x.campingid == campingId).FirstAsync();

            return res.Ubicacion;

        }
        public static async Task<string> GetCampingName(int campingId)
        {
            await Init();

            var res = await db.Table<Camping>().Where(x => x.campingid == campingId).FirstAsync();
            return res.campingname;
        }
        public static async Task<string> GetEstadoName(int estadoId)
        {
            await Init();

            var res = await db.Table<Estado>().Where(x => x.estadoid == estadoId).FirstAsync();
            return res.estadoname;
        }

        public static async Task<string> GetUpdate(int campingid)
        {
            await Init();

            try
            {
                await AuthHeader();
                var Response = await client.GetAsync(BaseUrl + $"/api/Suscripcion?campid=" + campingid.ToString());
                return Response.Content.ReadAsStringAsync().Result;

            }
            catch (Exception ex)
            {
                var a = ex;
                return ex.ToString();
            }
        }

        public static async Task<bool> AuthHeader()
        {
            var token = await SecureStorage.GetAsync("JwtToken");
            if (token == null)
            {
                return false;
            }
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return true;
        }

    }
}

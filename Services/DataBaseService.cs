﻿using PFG2.Models;
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

namespace PFG2.Services
{
    public class DataBaseService
    {
        static SQLiteAsyncConnection db;
        public static string databasePath { get; set; }

        private static string sqlConString = "Server=tcp:pfg.database.windows.net,1433;database=PFG;User ID=almata;Password=vH3Q7v29H!v;MultipleActiveResultSets=True;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";
        private static string sqlConString2 = "Data Source=TU-MADRE\\SQLEXPRESS;Initial Catalog=Test;User ID=almata;Password=vH3Q7v29H!v";

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
            if (db != null)
                return;

            // Get an absolute path to the database file
            
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            try
            {
                db = new SQLiteAsyncConnection(databasePath);
                /*
                await db.CreateTableAsync<Camping>();
                await db.CreateTableAsync<Reserva>();
                await db.CreateTableAsync<Cliente>();
                await db.CreateTableAsync<Estado>();
                await db.CreateTableAsync<Producto>();
                await db.CreateTableAsync<User>();
                */
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
            }
        }
        public static async Task InitDatabase()
        {
            Init();
        }
        public static async Task GetDB()
        {

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            try
            {
                var Response = await client.GetAsync(BaseUrl + $"/api/Sync");
                using (var stream = await Response.Content.ReadAsStreamAsync())
                {
                    var fileInfo = new FileInfo(Path.Combine(FileSystem.AppDataDirectory, "MyData.db"));
                    using (var fileStream = fileInfo.OpenWrite())
                    {
                        await stream.CopyToAsync(fileStream);
                    }
                }

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
            var query2 = await db.QueryAsync<Reserva>("select * from Reserva");
            var query= await db.QueryAsync<ReservasLista>("select r.idreserva, r.clientename, r.numeroparcela, r.campingid, c.campingname, r.productes, " +
                "r.productescodes, r.datainici, r.datafinal, r.preu, r.estadoid, e.estadoname, r.extra " +
                "from Reserva as r inner join Camping as c on c.campingid=r.campingid inner join Estado as e on e.estadoid = r.estadoid " +
                "Where r.campingid=" + campingid.ToString()+ " and (r.estadoid=1 or r.estadoid=3 or r.estadoid=5)");
            

            //var query = await db.Table<Reserva>().Where(x => x.campingid == campingid).ToListAsync();

            return query;
        }
        public static async Task<IEnumerable<ReservasLista>> GetReservasFilterKnownEstadoList(int campingid,string parcela, int estadoid, string dataini, string datafi)
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
            else
            {
                if (dataini != "")
                {
                    query = query.Where(x => x.datainici == dataini).ToList();
                }
                if (datafi != "")
                {
                    query = query.Where(x => x.datafinal == datafi).ToList();
                }
            }
            return query;
        }
        public static async Task<IEnumerable<Camping>> GetCampingsList()
        {
            await Init();
            var query = await db.Table<Camping>().ToListAsync();
            return query;
        }
        public static async Task<IEnumerable<Producto>> GetProductosList()
        {
            await Init();
            var query = await db.Table<Producto>().ToListAsync();
            return query;
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
                Init();
                var id = db.InsertAsync(nReserva);
                var data = JsonConvert.SerializeObject(nReserva);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(BaseUrl + "/api/Reserva", content);
                
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
                Init();
                var id = db.UpdateAsync(nReserva);
                var data = JsonConvert.SerializeObject(nReserva);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(BaseUrl + "/api/Reserva", content);

            }
            catch (Exception ex)
            {
                var a = ex;
            }
        }
        public static async Task NextStep(Reserva nReserva)
        {
            await Init();
            nReserva.estadoid += 1;
            await db.UpdateAsync(nReserva);
        }


    }
}

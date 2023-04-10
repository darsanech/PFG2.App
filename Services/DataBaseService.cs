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

namespace PFG2.Services
{
    public class DataBaseService
    {
        static SQLiteAsyncConnection db;
        public static string databasePath { get; set; }

        private static string sqlConString = "Server=tcp:pfg.database.windows.net,1433;database=PFG;User ID=almata;Password=vH3Q7v29H!v;MultipleActiveResultSets=True;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";
        private static string sqlConString2 = "Data Source=TU-MADRE\\SQLEXPRESS;Initial Catalog=Test;User ID=almata;Password=vH3Q7v29H!v";

        public DataBaseService()
        {
            Engage();
        }
        public static async void Engage()
        {
            await Init();
            
;        }
        static async Task Init()
        {
            if (db != null) { return; }

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            
            try
            {

                db = new SQLiteAsyncConnection(databasePath);
                var clientProvider = new SqliteSyncProvider(databasePath);
                var serverProvider = new SqlSyncChangeTrackingProvider(sqlConString);

                var setup = new SyncSetup("Reserva");
                SyncOptions options = new SyncOptions() { BatchSize = 102400 };

                var agent = new SyncAgent(clientProvider, serverProvider);
                var s1 = await agent.SynchronizeAsync(setup);
                Console.WriteLine(s1);
                //string text = File.ReadAllText(databasePath);
                Console.WriteLine(s1);


                //Populate();
            }
            catch (Exception ex) {
                Debug.WriteLine($"Unable to get information from server {ex}");
            }
        }
        public async static void Populate()
        {
            bool camping = true;
            bool cliente = true;
            bool estado = true;
            bool producto = true;
            bool reserva = true;
            bool user = true;

            if (camping)
            {
                var camp1 = new Camping { NomCamping = "Laguna" };
                var insertcamp = await db.InsertAsync(camp1);

                var camp2 = new Camping { NomCamping = "Gaviota" };
                insertcamp = await db.InsertAsync(camp2);

                var camp3 = new Camping { NomCamping = "Almata" };
                insertcamp = await db.InsertAsync(camp3);

                var camp4 = new Camping { NomCamping = "Un nombre muy largo a proposito asi tal vez no me entre en la pantalla" };
                insertcamp = await db.InsertAsync(camp4);

                var camp5 = new Camping { NomCamping = "Mas Nou" };
                insertcamp = await db.InsertAsync(camp5);

            }
            if (cliente)
            {
                var cli1 = new Cliente { NomClient = "Uncle Boobs" };
                var insertcli = await db.InsertAsync(cli1);

                var cli2 = new Cliente { NomClient = "Rachel DaHubbahubba" };
                insertcli = await db.InsertAsync(cli2);

                var cli3 = new Cliente { NomClient = "Denise Fat" };
                insertcli = await db.InsertAsync(cli3);
            }
            if(estado)
            {
                var est1 = new Estado { TipoEstado = "Entregar" };
                var insertest = await db.InsertAsync(est1);

                var est2 = new Estado { TipoEstado = "Recoger" };
                insertest = await db.InsertAsync(est2);

                var est3 = new Estado { TipoEstado = "Alquilado" };
                insertest = await db.InsertAsync(est3);

                var est4 = new Estado { TipoEstado = "Reparar" };
                insertest = await db.InsertAsync(est4);

                var est5 = new Estado { TipoEstado = "Finalizado" };
                insertest = await db.InsertAsync(est5);

                var est6 = new Estado { TipoEstado = "Otros" };
                insertest = await db.InsertAsync(est6);


            }
            if (producto)
            {
                var producto1 = new Producto { NomProducte = "Combi  1,85"};
                var insertest = await db.InsertAsync(producto1);

                var producto2 = new Producto { NomProducte = "Nevera 1,75" };
                insertest = await db.InsertAsync(producto2);

                var producto3 = new Producto { NomProducte = "Nevera 1,70" };
                insertest = await db.InsertAsync(producto3);

                var producto4 = new Producto { NomProducte = "Nevera 1,60" };
                var producto5 = new Producto { NomProducte = "Nevera 1,45" };
                var producto6 = new Producto { NomProducte = "Nevera 1,25" };
                var producto7 = new Producto { NomProducte = "Nevera 0,85" };
                insertest = await db.InsertAsync(producto7);

                var producto8 = new Producto { NomProducte = "Maquina de agua" };
                insertest = await db.InsertAsync(producto8);

                var producto9 = new Producto { NomProducte = "Microondas" };
            }
            if (reserva)
            {
                List<Reserva> reservas= new List<Reserva>();
                //Laguna
                reservas.Add(new Reserva
                {
                    Camping = "Laguna",
                    Parcela = "A12",
                    Cliente = "Reserva 1",
                    DataIni = "2023-08-11",
                    DataFi = "2023-08-11",
                    Estado = "Alquilado",
                    Preu = 40,
                    Producte = "Nevera 1,75",
                    ProducteId = "175-00001"
                });
                reservas.Add(new Reserva
                {
                    Camping = "Laguna",
                    Parcela = "A12",
                    Cliente = "Reserva 1",
                    DataIni = "2023-08-11",
                    DataFi = "2023-08-11",
                    Estado = "Alquilado",
                    Preu = 45,
                    Producte = "Nevera 1,75",
                    ProducteId = "175-00002"
                });
                reservas.Add(new Reserva
                {
                    Camping = "Laguna",
                    Cliente = "Reserva 1",
                    DataIni = "2023-08-11",
                    DataFi = "2023-08-11",
                    Parcela = "A12",
                    Estado = "Recoger",
                    Preu = 46,
                    Producte = "Nevera 1,75",
                    ProducteId = "175-00003"
                });
                reservas.Add(new Reserva
                {
                    Camping = "Laguna",
                    Cliente = "Reserva 1",
                    DataIni = "2023-08-11",
                    DataFi = "2023-08-11",
                    Estado = "Recoger",
                    Preu = 50,
                    Parcela = "A12",
                    Producte = "Nevera 1,75",
                    ProducteId = "175-00004"
                });
                reservas.Add(new Reserva
                {
                    Camping = "Laguna",
                    Cliente = "Reserva 1",
                    DataIni = "2023-08-11",
                    DataFi = "2023-08-11",
                    Estado = "Recoger",
                    Parcela = "A12",
                    Preu = 100,
                    Producte = "Combi 1,85",
                    ProducteId = "185-00001"
                });
                reservas.Add(new Reserva
                {
                    Camping = "Laguna",
                    Cliente = "Reserva 1",
                    DataIni = "2023-08-11",
                    DataFi = "2023-08-11",
                    Estado = "Entregar",
                    Parcela = "A12",
                    Preu = 100,
                    Producte = "Nevera 1,70",
                    ProducteId = "170-00001"
                });
                reservas.Add(new Reserva
                {
                    Camping = "Laguna",
                    Cliente = "Reserva 1",
                    DataIni = "2023-08-11",
                    DataFi = "2023-08-11",
                    Estado = "Entregar",
                    Preu = 100,
                    Producte = "Nevera 1,70",
                    Parcela = "A12",
                    ProducteId = "170-00002"
                });
                reservas.Add(new Reserva
                {
                    Camping = "Laguna",
                    Cliente = "Reserva 1",
                    DataIni = "2023-08-11",
                    DataFi = "2023-08-11",
                    Parcela = "A12",
                    Estado = "Entregar",
                    Preu = 100,
                    Producte = "Nevera 1,70",
                    ProducteId = "170-00003"
                });
                
                //Almata
                reservas.Add(new Reserva
                {
                    Camping = "Almata",
                    Parcela = "A12",
                    Cliente = "Reserva Almata",
                    DataIni = "2023-08-11",
                    DataFi = "2023-08-11",
                    Estado = "Reparar",
                    Preu = 110,
                    Producte = "Combi 1,85",
                    ProducteId = "185-00002"
                });
                reservas.Add(new Reserva
                {
                    Camping = "Almata",
                    Parcela = "A12",
                    Cliente = "Reserva Almata",
                    DataIni = "2023-08-11",
                    DataFi = "2023-08-11",
                    Estado = "Otros",
                    Preu = 120,
                    Producte = "Combi 1,85",
                    ProducteId = "185-00003"
                });
                reservas.Add(new Reserva
                {
                    Camping = "Almata",
                    Parcela = "A12",
                    Cliente = "Reserva Almata",
                    DataIni = "2023-08-11",
                    DataFi = "2023-08-11",
                    Estado = "Otros",
                    Preu = 10,
                    Producte = "Combi 1,85",
                    ProducteId = "185-00010"
                });                

                //Insert
                foreach(var res in reservas)
                {
                    await db.InsertAsync(res);
                }




            }
            if (user) {
                var user1 = new User { Username = "test1", password = "111" };
                var user2 = new User { Username = "test2", password = "222" };
                var id = await db.InsertAsync(user1);
                var id2 = await db.InsertAsync(user2);
            }
            
        }
        public static async Task<IEnumerable<User>> GetUsersList()
        {
            await Init();
            var query = await db.Table<User>().ToListAsync();
            return query;
        }
        public static async Task<IEnumerable<Reserva>> GetReservasList(string camping)
        {
            await Init();
            var query = await db.Table<Reserva>().Where(x => x.Camping == camping).ToListAsync();

            return query;
        }
        public static async Task<IEnumerable<Reserva>> GetReservasFilterKnownEstadoList(string camping,string parcela, string estado, string dataini, string datafi)
        {
            await Init();
            var query = await db.Table<Reserva>().Where(x => x.Camping == camping && x.Estado==estado).ToListAsync();
            if(parcela!=null)
            {
                query = query.Where(x => x.Parcela == parcela).ToList();
            }
            else
            {
                if (dataini != null)
                {
                    query = query.Where(x => x.DataIni == dataini).ToList();
                }
                if (datafi != null)
                {
                    query = query.Where(x => x.DataFi == datafi).ToList();
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
            await Init();
            var insertest = await db.InsertAsync(nReserva);
        }
        public static async Task NextStep(Reserva nReserva)
        {
            await Init();
            string nextStep="Alquilado";
            if (nReserva.Estado == "Recoger")
            {
                nextStep = "Finalizado";
            }
            nReserva.Estado = nextStep;
            await db.UpdateAsync(nReserva);
        }


    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Animations;
using Microsoft.VisualBasic.FileIO;
using PFG2.Models;
using PFG2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.ViewModel
{

    [QueryProperty("Idres", "Idres")]

    public partial class AddReservaViewModel : ObservableObject
    {
        //Un puñao de variables
        #region

        [ObservableProperty]
        int idres;
        public ObservableCollection<Estado> EstadosList { get; } = new();
        [ObservableProperty]
        int estado = -1;
        public ObservableCollection<Camping> CampingsList { get; } = new();
        [ObservableProperty]
        int camping = -1;
        public ObservableCollection<Producto> ProductosList { get; } = new();
        [ObservableProperty]
        int producto = -1;

        public ObservableCollection<ReservaProductoPH> ProductosPHList { get; } = new();

        [ObservableProperty]
        string textoBoton;
        [ObservableProperty]
        bool añadirProd=true;

        [ObservableProperty]
        string cliente;
        [ObservableProperty]
        string parcela;
        [ObservableProperty]
        int preu;
        [ObservableProperty]
        string extra;
        [ObservableProperty]

        DateTime dataIni = DateTime.Today;
        [ObservableProperty]
        DateTime dataFi = DateTime.Today;
        [ObservableProperty]

        bool isRefreshing;
        bool newPage=true;
        ReservasLista reservaEditar = new ReservasLista();


        #endregion
        [ICommand]
        public async Task OnLoad()
        {
            if (newPage)
            {
                var camps = await DataBaseService.GetCampingsList();
                foreach (var camp in camps)
                {
                    CampingsList.Add(camp);
                }
                var prods = await DataBaseService.GetProductosList();
                foreach (var prod in prods)
                {
                    ProductosList.Add(prod);
                }
                var estados = await DataBaseService.GetEstadosList();
                foreach (var estado in estados)
                {
                    EstadosList.Add(estado);
                }
                TextoBoton = "Añadir reserva";
                if (Idres > 0)
                {
                    reservaEditar = await DataBaseService.GetReservaListabyId(idres);
                    Cliente = reservaEditar.clientename;
                    Parcela = reservaEditar.numeroparcela;
                    Camping = reservaEditar.campingid - 1;
                    Estado = reservaEditar.estadoid - 1;
                    DataIni = DateTime.ParseExact(reservaEditar.datainici, "dd/MM/yyyy", null);
                    DataFi = DateTime.ParseExact(reservaEditar.datafinal, "dd/MM/yyyy", null);
                    Preu = reservaEditar.preu;
                    Extra = reservaEditar.extra;
                    QuerytoPPH();
                    TextoBoton = "Modificar reserva";
                }
                newPage = false;
            }
        }

        [ICommand]
        public async Task AddProducto()
        {
            ReservaProductoPH niu = new ReservaProductoPH();
            niu.quantitat = 1;
            niu.productoname = ProductosList[producto].productoname;
            niu.producteid = producto + 1;
            if (idres != null)
            {
                niu.idreserva = idres;
            }
            ProductosPHList.Add(niu);
            Producto = -1;
            if (ProductosPHList.Count() >= 5)
            {
                AñadirProd = false;
            }
        }

        [ICommand]
        public async Task Upload()
        {
            
            #region "Compara las entradas"
            List<string> a = new List<String>();
            if (camping == -1)
            {
                a.Add("Camping no seleccionat");
            }
            if (estado== -1)
            {
                a.Add("Estado no seleccionat");
            }
            if (ProductosPHList.Count()<=0)
            {
                a.Add("Producto no seleccionat");
            }
            if (dataIni > dataFi)
            {
                a.Add("Data incorrecta");
            }
            if (preu == 0)
            {
                a.Add("Falta el preu");
            }
            if (extra == null)
            {
                extra = "";
            }
            if (a.Count()>0)
            {
                await Application.Current.MainPage.DisplayAlert("Atencion!", string.Join(System.Environment.NewLine, a), "Salir");

            }
            #endregion
            else
            {
                if(cliente==null || cliente == "")
                {
                    cliente = "???";
                }
                if (parcela == null || parcela == "")
                {
                    parcela = "???";
                }
                Reserva newItem = new Reserva()
                {
                    clientename = cliente,
                    numeroparcela = parcela,
                    campingid = camping+1,
                    datainici = dataIni.ToString("dd/MM/yyyy"),
                    datafinal = dataFi.ToString("dd/MM/yyyy"),
                    Preu = preu,
                    estadoid = estado+1,
                    Extra = extra,
                    userid = 1,
                };

                try
                {
                    if (idres != 0)
                    {
                        newItem.idreserva= idres;
                        await DataBaseService.UpdateReserva(newItem, PPHtoQuery());
                    }
                    else
                    {
                        await DataBaseService.AddReserva(newItem, PPHtoQuery());
                    }
                }
                catch(Exception ex) { 
                    var exp = ex; }
                await Shell.Current.GoToAsync("..");
                
            }
        }
        private List<ReservaProducto> PPHtoQuery()
        {
            List <ReservaProducto> LRP= new List<ReservaProducto>();
            foreach (ReservaProductoPH RP in ProductosPHList)
            {
                LRP.Add((ReservaProducto)RP);
            }
            return LRP;
        }
        private async void QuerytoPPH()
        {
            var LRP= await DataBaseService.GetProductosReserva(Idres);
            foreach (ReservaProductoPH RP in LRP)
            {
                ProductosPHList.Add(RP);
            }
        }
        [ICommand]
        public void SumProduct(ReservaProductoPH pph)
        {
            var i=ProductosPHList.IndexOf(pph);
            pph.quantitat++;
            ProductosPHList[i]=pph;
        }
        [ICommand]
        public void ResProduct(ReservaProductoPH pph)
        {
            var i = ProductosPHList.IndexOf(pph);
            pph.quantitat--;
            if(pph.quantitat != 0)
                ProductosPHList[i]=pph;
            else
            {
                AñadirProd = true;
                ProductosPHList.RemoveAt(i);
            }
        }
    }
}
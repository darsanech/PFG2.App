using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        public ObservableCollection<ProductoPH> ProductosPHList { get; } = new();



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
                newPage = false;
            }
            if (Idres > 0)
            {
                reservaEditar = await DataBaseService.GetReservaListabyId(idres);
                Cliente = reservaEditar.clientename;
                Parcela = reservaEditar.numeroparcela;
                Camping = reservaEditar.campingid-1;
                Estado = reservaEditar.estadoid-1;
                DataIni = DateTime.ParseExact(reservaEditar.datainici, "dd/MM/yyyy", null);
                DataFi = DateTime.ParseExact(reservaEditar.datafinal, "dd/MM/yyyy", null);
                Preu = reservaEditar.preu;
                Extra = reservaEditar.extra;
                StringtoPPH();
            }

        }
        [ICommand]
        public async Task Refresh()
        {
            isRefreshing = false;
        }

        [ICommand]
        public async Task AddProducto()
        {
            ProductoPH niu = new ProductoPH();
            niu.cantidad = 1;
            niu.productonombre= ProductosList[producto].productoname;
            ProductosPHList.Add(niu);
            Producto = -1;
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
            if (Cliente == null)
            {
                a.Add("Falta nom del client");
            }
            if (parcela == null)
            {
                a.Add("Falta numero de parcela");
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
                Reserva newItem = new Reserva()
                {
                    clientename = cliente,
                    numeroparcela = parcela,
                    campingid = camping+1,
                    productes = PPHtoString(),
                    productescodes = "",
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
                        await DataBaseService.UpdateReserva(newItem);
                    }
                    else
                    {
                        await DataBaseService.AddReserva(newItem);
                    }
                }
                catch(Exception ex) { 
                    var exp = ex; }
                await Shell.Current.GoToAsync("..");
                
            }
        }
        private string PPHtoString()
        {
            string res="";
            int count = ProductosPHList.Count();
            foreach (ProductoPH pph in ProductosPHList)
            {
                res += pph.productonombre;
                if (pph.cantidad > 1)
                {
                    res += " x" + pph.cantidad;
                }
                count--;
                if(count != 0)
                    res += ", ";
            }
            return res;
        }
        private void StringtoPPH()
        {
            string[] Productes=reservaEditar.productes.Split(", ");
            foreach (string s in Productes)
            {
                string[] Producte=s.Split(" x");
                ProductoPH niu=new ProductoPH();
                niu.productonombre = Producte[0];
                if (Producte.Length==1)
                {
                    niu.cantidad= 1;
                }
                else
                {
                    niu.cantidad=Int32.Parse(Producte[1]);
                }
                ProductosPHList.Add(niu);
            }
        }
        [ICommand]
        public void SumProduct(ProductoPH pph)
        {
            ProductosPHList.Remove(pph);
            pph.cantidad++;
            ProductosPHList.Add(pph);
        }
        [ICommand]
        public void ResProduct(ProductoPH pph)
        {
            ProductosPHList.Remove(pph);
            pph.cantidad--;
            if(pph.cantidad!=0)
                ProductosPHList.Add(pph);
        }
    }
}
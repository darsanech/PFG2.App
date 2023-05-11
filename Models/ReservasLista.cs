using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Models
{
    public class ReservasLista
    {
        public int idreserva { get; set; }
        public string clientename { get; set; }
        public string numeroparcela { get; set; }
        public int campingid { get; set; }
        public string campingname { get; set; }
        public string productes { get; set; }
        public string productestranslated
        {
            get => productes + "!";
        }
        public string productescodes { get; set; }
        public string datainici { get; set; }
        public string datafinal { get; set; }
        public int preu { get; set; }
        public int estadoid { get; set; }
        public string estadoname { get; set; }
        public string extra { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Models
{
    public class ReservaProductoPH
    {
        public int idreserva { get; set; }
        public int producteid { get; set; }
        public int quantitat { get; set; }
        public string productoname { get; set; }

        public static explicit operator ReservaProducto(ReservaProductoPH obj)
        {
            return JsonConvert.DeserializeObject<ReservaProducto>(JsonConvert.SerializeObject(obj));
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Models
{
    public class ProductoPH
    {
        public string productoname { get; set; }
        public int cantidad { get; set; }
        public int total { get; set; }
        public int producteid { get; set; }
        public int disponible { get; set; }
        public int mod { get; set; }

        public static explicit operator Producto(ProductoPH obj)
        {
            return JsonConvert.DeserializeObject<Producto>(JsonConvert.SerializeObject(obj));
        }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PFG2.Models
{
    public class ReservaProducto
    {
        [Key]
        [Column("idreserva")]
        public int idreserva { get; set; }
        [Key]
        [Column("producteid")]
        public int producteid { get; set; }
        [Column("quantitat")]
        public int quantitat { get; set; }

        public static explicit operator ReservaProductoPH(ReservaProducto obj)
        {
            return JsonConvert.DeserializeObject<ReservaProductoPH>(JsonConvert.SerializeObject(obj));
        }
    }
}

using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Models
{
    public class ReservaPendiente
    {
        [PrimaryKey, Column("idreserva")]
        public int idreserva { get; set; }

        [Column("clientename")]
        public string clientename { get; set; }
        [Column("numeroparcela")]
        public string numeroparcela { get; set; }
        [Column("campingid")]
        public int campingid { get; set; }
        [Column("productes")]
        public string productes { get; set; }
        [Column("productescodes")]
        public string productescodes { get; set; }
        [Column("datainici")]
        public string datainici { get; set; }
        [Column("datafinal")]
        public string datafinal { get; set; }
        [Column("preu")]
        public int Preu { get; set; }
        [Column("estadoid")]
        public int estadoid { get; set; }
        [Column("extra")]
        public string Extra { get; set; }
        [Column("userid")]
        public int userid { get; set; }
        [Column("type")]
        public bool type { get; set; } = false; //false=Post true=Put

        public static explicit operator Reserva(ReservaPendiente obj)
        {
            return JsonConvert.DeserializeObject<Reserva>(JsonConvert.SerializeObject(obj));
        }
    }
}

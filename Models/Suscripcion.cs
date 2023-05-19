using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Models
{
    public class Suscripcion
    {
        [Key]
        [Column("userid")]
        public int userid { get; set; }
        [Key]
        [Column("campingid")]
        public int campingid { get; set; }
        [Column("needupdate")]
        public bool update { get; set; }
    }
}

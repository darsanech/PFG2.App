using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Models
{
    public class Producto
    {
        [PrimaryKey, Column("nom")]
        public string NomProducte { get; set; }
    }
}

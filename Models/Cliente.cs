using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Models
{
    public class Cliente
    {
        [PrimaryKey, Column("nom")]
        public string NomClient { get; set; }
    }
}

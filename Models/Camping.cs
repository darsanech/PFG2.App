using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Models
{
    public class Camping
    {
        [PrimaryKey, Column("nom")]
        public string NomCamping { get; set; }
    }
}

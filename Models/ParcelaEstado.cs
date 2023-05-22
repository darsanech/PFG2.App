using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Models
{
    public class ParcelaEstado
    {
        [PrimaryKey, AutoIncrement, Column("parcelaestadoid")]
        public int parcelaestadoid { get; set; }
        [Column("campingid")]
        public int campingid { get; set; }
        [Column("numeroparcela")]
        public string numeroparcela { get; set; }
        [Column("estadoid")]
        public int estadoid { get; set; }
    }
}

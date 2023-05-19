using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Models
{
    public class SuscripcionPlus
    {
        public int campingid { get; set; }
        public string campingname { get; set; }
        public bool update { get; set; } = false;
    }
}

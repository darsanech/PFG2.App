using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Models
{
    public class BearerToken
    {
        public BearerToken() { }
        public string token { get; set; }
        public int userId { get; set; }
        public int expirationInMinutes { get; set; }
        public int Rol { get; set; }
    }
}

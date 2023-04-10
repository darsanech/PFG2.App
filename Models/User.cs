using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Models
{
    [SQLite.Table("Users")]
    public class User
    {
        [PrimaryKey,AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("username")]
        public string Username { get; set; }     
        [Column("password")]
        public string password { get; set; }
    }
}

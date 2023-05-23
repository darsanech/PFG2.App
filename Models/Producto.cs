using Newtonsoft.Json;
using SQLite;

namespace PFG2.Models
{
    public class Producto
    {
        [PrimaryKey, AutoIncrement, Column("producteid")]
        public int producteid { get; set; }
        [Column("productoname")]
        public string productoname { get; set; }
        [Column("total")]
        public int total { get; set; }
        [Column("disponible")]
        public int disponible { get; set; }



        public static explicit operator ProductoPH(Producto obj)
        {
            return JsonConvert.DeserializeObject<ProductoPH>(JsonConvert.SerializeObject(obj));
        }
    }
}

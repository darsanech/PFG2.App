﻿using SQLite;

namespace PFG2.Models
{
    public class Camping
    {
        [PrimaryKey, AutoIncrement, Column("campingid")]
        public int campingid { get; set; }
        [Column("campingname")]
        public string campingname { get; set; }
        [Column("ubicacion")]
        public string Ubicacion { get; set; }
    }
}

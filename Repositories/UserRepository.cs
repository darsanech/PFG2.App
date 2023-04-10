using PFG2.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFG2.Repositories
{
    public class UserRepository
    {
        private SQLiteConnection _database;
        public static string DbPath { get; } =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PFGDB.db");

        public UserRepository()
        {
            _database = new SQLiteConnection(DbPath);
        }
        public List<User> GetList()
        {            
            return _database.Table<User>().ToList();
        }
    }
}

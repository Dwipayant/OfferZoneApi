using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfferZoneAPI.Common
{
    public class DbContext
    {
        public string ConnectionString { get; set; }
        public DbContext (string connecttionString)
        {
            this.ConnectionString = connecttionString;
        }
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}

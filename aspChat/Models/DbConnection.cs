using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Npgsql;

namespace aspChat.Models
{
    public class DbConnection
    {
        public NpgsqlConnection Connection => new NpgsqlConnection(ConfigurationManager.ConnectionStrings["awsPGS"].ToString());
    }
}
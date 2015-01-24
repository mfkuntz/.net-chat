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
        public NpgsqlConnection Connection {
            get {
                if (! String.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["awsPGS"].ToString()))
                {
                    return new NpgsqlConnection(ConfigurationManager.ConnectionStrings["awsPGS"].ToString());
                }
                else
                {
                    return new NpgsqlConnection(Environment.GetEnvironmentVariable("conString-awsPGS"));
                }
                
            }
        }
    }
}
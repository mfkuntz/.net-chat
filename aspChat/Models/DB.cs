using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace aspChat.Models
{
    public class DB :DbContext
    {
        public DB(string connection) : base(nameOrConnectionString: connection)
        {
        }

        public DbSet<ChatMessage> Messages { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using aspChat.Controllers.Custom;
using aspChat.Models;
using Dapper;
using Newtonsoft.Json;
using Npgsql;

//using Npgsql;

namespace aspChat.Controllers
{
    public class ChatController : ApiController
    {
        private NpgsqlConnection dbcon = new DbConnection().Connection;
        // GET api/chat
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/chat/5
        public List<ChatMessage> Get(string id)
        {

            //Entity Framework implementation. 

            //            var db = new DB("awsPGS");
            //            var query = from b in db.Messages
            //                where b.Reciever == id
            //                select b;
            //            return query.ToList();


            //Dapper implentation

            dbcon.Open();
            var list = dbcon.Query<ChatMessage>("select * from \"Messages\" where reciever = @rec", new {rec = id}).ToList();
            dbcon.Close();
            return list;
        }

        // POST api/chat
        public ChatMessage Post(ChatMessage message)
        {
            message.CreatedAt = DateTime.Now;
//            var db = new DB("awsPGS");
//            db.Messages.Add(message);
//            db.SaveChangesAsync();

            dbcon.Open();
            dbcon.Execute("insert into \"Messages\"(message,sender,reciever,createdAt values {@a, @b, @c, @d})",
                new {a = message.Message, b = message.Sender, c = message.Reciever, d = message.CreatedAt});
            dbcon.Close();

            return message;
        }

        // PUT api/chat/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/chat/5
        public void Delete(int id)
        {
        }
    }
}

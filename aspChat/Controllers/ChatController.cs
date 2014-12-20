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
            var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["awsPGS"].ToString());
            con.Open();
            var list =  con.Query<ChatMessage>("select * from \"Messages\" where reciever = @rec", new {rec = id}).ToList();
            con.Close();
            return list;
        }

        // POST api/chat
        public ChatMessage Post(ChatMessage message)
        {
            message.CreatedAt = DateTime.Now;
            var db = new DB("awsPGS");
            db.Messages.Add(message);
            db.SaveChangesAsync();
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

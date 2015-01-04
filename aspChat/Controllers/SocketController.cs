using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;
using aspChat.Controllers.Custom;
using aspChat.Models;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Npgsql;

namespace aspChat.Controllers
{
    public class SocketController : PersistentConnection
    {
        private NpgsqlConnection dbcon = new DbConnection().Connection;

        //        protected override Task OnConnected(IRequest request, string connectionId)
        //        {
        //            return Connection.Broadcast("Connection: " + connectionId);
        //        }


        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            var room = ChangeRoomRequest(data, connectionId);
            if (room != null) return Groups.Send(room.DesiredRoom, "HI");

            var message = JsonConvert.DeserializeObject<ChatMessage>(data);

            if (message != null)
            {
                message.CreatedAt = DateTime.Now;

                message.CreatedAt = DateTime.Now;
                //            var db = new DB("awsPGS");
                //            db.Messages.Add(message);
                //            db.SaveChangesAsync();

                dbcon.OpenAsync();
                dbcon.ExecuteAsync("insert into \"Messages\"(message,sender,reciever,\"createdAt\") values (@a, @b, @c, @d) ",
                    new { a = message.Message, b = message.Sender, c = message.Reciever, d = message.CreatedAt });
                dbcon.Close();

                return Groups.Send(message.Reciever, JsonConvert.SerializeObject(message, JsonSettings()));
            }

            return Connection.Broadcast(data);
        }


        private ChangeRoomRequest ChangeRoomRequest(string data, string connectionId)
        {
            ChangeRoomRequest room = JsonConvert.DeserializeObject<ChangeRoomRequest>(data);
            
            if (!String.IsNullOrEmpty(room.CurrentRoom))
            {
                Groups.Remove(connectionId, room.CurrentRoom);
            }
            if (!String.IsNullOrEmpty(room.DesiredRoom))
            {
                Groups.Add(connectionId, room.DesiredRoom);
            }
            else
            {
                return null;
            }

            return room;
        }

        private JsonSuccess Success(string message = "Success")
        {
            return new JsonSuccess {Success = true, Message = message};
        }

        private JsonSerializerSettings JsonSettings()
        {
            return new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace aspChat.Models
{
    [Table("Messages", Schema = "public")]
    public class ChatMessage
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }
        [Column("message")]
        public string Message { get; set; }
        [Column("sender")]
        public string Sender { get; set; }
        [Column("reciever")]
        public string Reciever { get; set; }
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }

    }


}
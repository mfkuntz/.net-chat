using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspChat.Models
{
    public class ChangeRoomRequest
    {
        public string CurrentRoom { get; set; }
        public string DesiredRoom { get; set; }
    }
}
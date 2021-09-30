using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public class Routes
    {
        public int Id { get; set; }
        public int Telco { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string TelcoName { get; set; }
        public string Receiver { get; set; }
        public string Sender { get; set; }
        public string MsgData { get; set; }
        public int Status { get; set; }
        public int DTN { get; set; }
        public int RemoteIP { get; set; }
        public int Network { get; set; }
        public string Route { get; set; }
        public string Operator { get; set; }
        public string CreatedDatetime { get; set; }
    }
}
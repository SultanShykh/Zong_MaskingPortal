using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public class DashBoard
    {
        public int Today { get; set; }
        public int Month { get; set; }
        public int Week { get; set; }
        public int Total { get; set; }
        public string CreatedDateTime { get; set; }

    }
}
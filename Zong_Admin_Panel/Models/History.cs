using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public class History
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Activitytype { get; set; }
        public string MaskingId { get; set; }
        public string Activity { get; set; }
        public string Date { get; set; }
    }
}
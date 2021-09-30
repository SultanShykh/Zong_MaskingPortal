using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public class Sender
    {
        public int Id { get; set; }

        public string AccountNumber { get; set; }
        public string Masking { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }

        public string NTN_FTN { get; set; }
        public bool NTN_FTN_Status { get; set; }

        public string NTN_FTN_Statuses { get; set; }
        public string Operators { get; set; }
        public int Priority { get; set; }

        public string Attachment { get; set; }

        public string Comments { get; set; }

        public int userId { get; set; }
       
    }
}
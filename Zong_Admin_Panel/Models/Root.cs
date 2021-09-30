using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public class Root
    {
        public string AccountNumber { get; set; }
        public string MaskingID { get; set; }
        public string NTN_FTN { get; set; }
        public string Operator { get; set; }
        public string NTN_FTN_Status { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedTo { get; set; }
        public string Attachment { get; set; }
    }
}
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
        public string MaskingID { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
        public int CreatorType { get; set; }

        public string NTN_FTN { get; set; }
        public bool NTN_FTN_Status { get; set; }

        public string NTN_FTN_Statuses { get; set; }
        public string Operators { get; set; }
        public int Priority { get; set; }
        public string Priorities { get; set; }

        public string Attachment { get; set; }
        public string TatStatus { get; set; }
        public string DaysElapsed { get; set; }
        public string CurrentStatus { get; set; }

        public string Comments { get; set; }

        public int userId { get; set; }
        public int user { get; set; }
        public int Usertypess { get; set; }
        public string op { get; set; }
        public int Requestedto { get; set; }
        public string RequestedToo { get; set; }
        public string Reason { get; set; }
        public int Approveby { get; set; }
        public int Rejectedby { get; set; }
        public string ApprovedByyy { get; set; }
        public int Reverteby { get; set; }
        public int request { get; set; }
        public string RequestedBY { get; set; }
        public int? approved { get; set; }
        public int? rejected { get; set; }

        public string RevertedByyy { get; set; }
        public string RejectedByyy { get; set; }
        public string CreatedDate { get; set; }
      

        public bool? Zong { get; set; }
        public string z { get; set; }
        public bool? Warid { get; set; }
        public string w { get; set; }
        public bool? Telenor { get; set; }
        public string t { get; set; }
        public bool? Ufone { get; set; }
        public string u { get; set; }
        public bool? Jazz { get; set; }
        public string j { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }


    }
}
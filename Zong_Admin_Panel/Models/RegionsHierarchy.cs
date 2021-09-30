using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public class RegionsHierarchy
    {
        public int Id { get; set; }
        public int RegionRequestbyId { get; set; }
        public string Requestedby { get; set; }
        public string Requestedto{ get; set; }
        public int RegionRequesttoId{ get; set; }
    }
}
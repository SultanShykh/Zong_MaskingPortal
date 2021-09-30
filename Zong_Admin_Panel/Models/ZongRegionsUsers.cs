using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public class ZongRegionsUsers
    {
        public int Id { get; set; }
        public int UsersId { get; set; }
        public string Username { get; set; }
        public int RegionId { get; set; }
        public string Regionname { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public class Permissions
    {
        public int FormMasterId { get; set; }
        public string FormMasterName { get; set; }
        public string MenuName { get; set; }
        public int MenuId { get; set; }
        public string MenuParentId { get; set; }
        public string MenuUrl { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string PermissionId { get; set; }
        public bool AllowCreate { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowView { get; set; }



          
    }
}
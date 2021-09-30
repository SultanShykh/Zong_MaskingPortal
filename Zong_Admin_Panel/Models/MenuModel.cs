using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public class MenuModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
        public string IconClass { get; set; }

        public string MenuTree { get; set; }
        public IEnumerable<MenuModel> List { get; set; }
    }
}
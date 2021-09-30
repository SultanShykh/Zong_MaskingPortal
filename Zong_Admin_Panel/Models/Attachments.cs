using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public class Attachments
    {
        public int Id { get; set; }

        public string URL { get; set; }

        public int SenderId { get; set; }
    }
}
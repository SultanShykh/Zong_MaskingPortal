using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public class LoginModel

    {
        public string Username { get; set; }
        public int UserId { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public string email { get; set; }
        public string UserGroup { get; set; }
        public DateTime otptime { get; set; }
        public string otp { get; set; }
        public string UserType { get; set; }
        public int Counter { get; set; }


    }

}
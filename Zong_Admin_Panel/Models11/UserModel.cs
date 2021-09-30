using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string username { get; set; }

        [Required]
        public string password { get; set; }

        [NotMapped]
        [Compare("password", ErrorMessage = "Passwords must match")]
        public string confirmpassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string email { get; set; }

        public string mobileNumber { get; set; }

        public int userType { get; set; }
        public DateTime DOB { get; set; }
        public bool Gender { get; set; }
        public int stateId { get; set; }

        public virtual byte[] image { get; set; }

        public string GroupName { get;set; }

        public List<GroupMasterModel> GroupMasters { get; set; }
    }
}

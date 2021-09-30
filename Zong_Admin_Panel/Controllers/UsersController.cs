using Zong_Admin_Panel;
using Zong_Admin_Panel.Codebase;
using Zong_Admin_Panel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zong_Admin_Panel.Interaces;
using System.Net;
using System.Net.Mail;

namespace Zong_Admin_Panel.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        IZongRegions zongs;
       public UsersController(IZongRegions zongs)
        {
            this.zongs = zongs;
        }
        public ActionResult Index()
        {
            List<GroupMasterModel> groups;
            CommonProcessing.GetAllGroups(out groups);

            var group = groups.Where(x => x.applicationId == 8).ToList();
            ViewBag.userGroup = new SelectList(group, "Id", "Name");
           
            var ss = zongs.GetAll();
            ViewBag.zongusers = new SelectList(ss, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult CheckUsername(string username)
        {
            bool result;
            UserProcessing.CheckUser(username, out result);
            return Json(result);
        }
        public ActionResult CreateUser()
        {
            try
            {
                List<GroupMasterModel> groups;
                CommonProcessing.GetAllGroups(out groups);
                var group = groups.Where(x => x.applicationId == 8).ToList();
                ViewBag.userGroup = new SelectList(group, "Id", "Name");
                 return View();

            }
            catch (Exception ex)
            {
                return Json(false);
            }


        }

        [HttpPost]
        public ActionResult CreateUser(FormCollection model)
        {
            try
            {

                bool result;
                string msg;
                UserModel userModel = new UserModel();
                userModel.FirstName = model["FirstName"].ToString();
                userModel.LastName = model["LastName"].ToString();
                userModel.username = model["username"].ToString();
                userModel.password = model["password"].ToString();
                userModel.Region =Convert.ToInt32(model["Regions"].ToString());
                userModel.confirmpassword = model["confirmpassword"].ToString();
                userModel.email = model["email"].ToString();
                //userModel.userType = Convert.ToInt32(model["userType"]);
                var groupId = model["userGroup"].ToString();
               
                List<GroupMasterModel> groups;
                 CommonProcessing.GetAllGroups(out groups);
                var users = groups.Where(x => x.id ==Convert.ToInt32(groupId)).ToList();

                if (users[0].Name == "USERS")
                {
                    userModel.userType = 3;
                }
                else if (users[0].Name == "SUPER ADMIN")
                {
                    userModel.userType = 1;

                }
                else
                {
                    userModel.userType = 2;
                }

                if (userModel.confirmpassword != userModel.password)
                {
                    return Json("Password And Confirm Password Are Different");
                }

               
                UserProcessing.CreateUser(userModel, groupId, out result, out msg);
               
               
                return Json(new { status = result, message = msg });
            }
            catch (Exception ex)
            {
                string msg = "Error, Please try again.";
                return Json(new { status = false, message = msg });
            }
        }


        [HttpPost]
        public ActionResult RenderList(DTParameters param) 
        {
            DTResult<UserModel> result;
            UserProcessing.Get(param, out result);
            return Json(result);
        }

        [HttpPost]
        public ActionResult UpdateUserGroup(string GroupId, string UserId ) {

            try
            {
                bool result;
                UserProcessing.UpdateUserGroup(UserId, GroupId, out result);
                return Json(result);
            }
            catch(Exception e)
            {
                return Json("Error");
            }

           
           
        }

        [HttpPost]
        public ActionResult DeleteUser(string Id)
        {
            try
            {
                bool result;
                string msg;
                UserProcessing.DeleteUser(Id,out result, out msg);
                return Json(true);

            }
            catch(Exception e)
            {
                return Json("Error");
            }
        }

        [HttpPost]
        public ActionResult UpdateUser(FormCollection model)
        {
            try
            {
                bool result;

                UserModel userModel = new UserModel();
                userModel.Id = Convert.ToInt32(model["userId"]);
                userModel.FirstName = model["FirstName"].ToString();
                userModel.LastName = model["LastName"].ToString();
                userModel.username = model["username"].ToString();
                userModel.Region = Convert.ToInt32(model["Regions"].ToString());
                userModel.email = model["email"].ToString();
               // userModel.userType = Convert.ToInt32(model["userType"]);
               
                var groupId = model["userGroup"].ToString();
                List<GroupMasterModel> groups;
                CommonProcessing.GetAllGroups(out groups);
                var users = groups.Where(x => x.id == Convert.ToInt32(groupId)).ToList();

                if (users[0].Name == "USERS")
                {
                    userModel.userType = 3;
                }
                else if (users[0].Name == "SUPER ADMIN")
                {
                    userModel.userType = 1;

                }
                else
                {
                    userModel.userType = 2;
                }
                UserProcessing.UpdateUser(userModel, groupId, out result);

                return Json(new { status = result, message = "Successfully Updated!!!" });
            }
            catch (Exception e)
            {
                return Json("Error");
            }

        }
    }
}
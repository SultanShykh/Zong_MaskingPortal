using Zong_Admin_Panel.Models;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Zong_Admin_Panel.Codebase
{
    public class UserProcessing
    {
        static dynamic AppDB = Database.OpenNamedConnection("MainDB");
        public static void Get(DTParameters param, out DTResult<UserModel> result)
        {
            result = new DTResult<UserModel>();
            Dictionary<string, object> data = new Dictionary<string, object>();
            dynamic Records = AppDB.COR_GetUserWithGroups(PageIndex: param.Start, PageSize: param.Length == 0 ? 25 : param.Length);
           
            List<GroupMasterModel> grouplist = new List<GroupMasterModel>();
            if (Records.FirstOrDefault() != null)
            {
                foreach (var v in Records)
                {
                    GroupMasterModel groupMaster = new GroupMasterModel();
                    groupMaster.id = v.Id;
                    groupMaster.Name = v.Name;

                    grouplist.Add(groupMaster);
                }
            }
            Records.NextResult();
            var SingleRecord = Records.FirstOrDefault();
            int Total = 0;
            if (Records.FirstOrDefault() != null)
            {
                foreach (var rec in Records)
                {

                    result.data.Add(new UserModel()
                    {
                        Id = rec.Id,
                        username = rec.Username,
                        FirstName = rec.FirstName,
                        LastName = rec.LastName,
                        email = rec.Email,
                        GroupName = rec.GroupName,
                        userType = rec.userType,
                        GroupMasters = grouplist
                        
                    });

                    Total = SingleRecord.Count;

                }
            }

          
           
           

            result.draw = param.Draw;
            result.recordsTotal = Total;
            result.recordsFiltered = Total;

           
        }

        public static void CheckUser(string username, out bool result)
        {
            result = false;

            dynamic Records = AppDB.COR_USP_CheckUser( Username: username);
            if (Records.FirstOrDefault() != null && Records.FirstOrDefault().Message == "NOT EXIST")
            {
                result = true;
            }
            
        }


        public static void CreateUser(UserModel model,string groupId, out bool result, out string msg)
        {
            result = false;
            msg = null;
            dynamic Records = AppDB.COR_USP_CreateUser(FirstName: model.FirstName, LastName: model.LastName, Username: model.username, Password: model.password , Email: model.email,userType :model.userType, ExpiryDateTime:DateTime.Now.AddYears(2),userGroupId: groupId);
            if (Records.FirstOrDefault() != null && Records.FirstOrDefault().Message == "User Created")
            {
                result = true;
            }
           else if (Records.FirstOrDefault() != null && Records.FirstOrDefault().Message == "Username Already Taken")
            {
                result = true;
            
            }
            msg = Records.FirstOrDefault().Message;


        }
        public static void UpdateUserGroup(string userid , string groupid, out bool result)
        {
            result = false;

            dynamic Records = AppDB.COR_USP_UpdateUserGroup(UserId: userid, GroupId: groupid);
            if (Records.FirstOrDefault() != null && Records.FirstOrDefault().Message == "User updated")
            {
                result = true;
            }
            

        }

        public static void DeleteUser(string userid, out bool result, out string msg)
        {
            result = false;
            msg = null;
            dynamic Records = AppDB.COR_USP_DeleteUser(UserId: userid);
             
            if (Records.FirstOrDefault() != null && Records.FirstOrDefault().Message == "User Deleted")
            {
                result = true;
            }
            
            msg = Records.FirstOrDefault().Message;
        }
        public static void UpdateUser(UserModel model, string groupId, out bool result)
        {
            result = false;

            dynamic Records = AppDB.COR_USP_UpdateUser(userId:model.Id, FirstName: model.FirstName, LastName: model.LastName, Username: model.username,  Email: model.email, userType: model.userType, groupId: groupId);

            if (Records.FirstOrDefault() != null && Records.FirstOrDefault().Message == "User Updated")
            {
                result = true;
            }
            
        }
    }
}
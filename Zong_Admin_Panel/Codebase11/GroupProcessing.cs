﻿using Zong_Admin_Panel.Models;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Codebase
{
    public class GroupProcessing
    {
        static dynamic AppDB = Database.OpenNamedConnection("MainDB");
        public static void Get(DTParameters param, out DTResult<GroupMasterModel> result)
        {
            result = new DTResult<GroupMasterModel>();
            dynamic Records = AppDB.COR_USP_GetAllGroups();
            var SingleRecord = Records.FirstOrDefault();
            if (Records.FirstOrDefault() != null)
            {
                foreach (var rec in Records)
                {
                    result.data.Add(new GroupMasterModel()
                    {
                        id = rec.Id,
                        Name = rec.Name,
                        isActive= rec.Status == 1 ? "Active" : "Deactive",
                        Status = rec.Status,
                    });

                }
            }
            
        }

        public static void checkGroupName(string groupName, out bool result)
        {
            result = false;

            dynamic Records = AppDB.COR_USP_CheckGroupName(groupName: groupName);
            if (Records.FirstOrDefault() != null && Records.FirstOrDefault().Message == "NOT EXIST")
            {
                result = true;
            }
            else
            {
                result = false;
            }

        }


        public static void CreateGroup(GroupMasterModel groupMastermodel, GroupMenuModel groupMenu , out bool result, string users = null)
        {
            result = false;
            dynamic Records = AppDB.COR_USP_CreateGroup(Name: groupMastermodel.Name, CreatedDateTime: DateTime.Now, MenuId: groupMenu.MenuId, AllowEdit:groupMenu.AllowEdit, AllowCreate: groupMenu.AllowCreate, AllowDelete: groupMenu.AllowDelete, AllowView: groupMenu.AllowView, Users: users);
            var msg = Records.FirstOrDefault().Message;
            if (Records.FirstOrDefault() != null && Records.FirstOrDefault().Message == "Group Created")
            {
                result = true;
            }
            else
            {
                result = false;
            }

        }

        public static void DeleteGroup(string groupId, out bool result, out string msg)
        {
            result = false;

            dynamic Records = AppDB.COR_USP_DeleteGroup(GroupId: groupId);

            if (Records.FirstOrDefault() != null && Records.FirstOrDefault().Message == "Group Deleted")
            {
                result = true;
            }
            else
            {
                
                result = false;
            }
            msg = Records.FirstOrDefault().Message;
        }
        public static void DeleteGroupMenu( out bool result, int groupId)
        {
            result = false;
            dynamic Records = AppDB.COR_USP_DeleteGroupMenu(GroupId: groupId);

            if (Records.FirstOrDefault() != null && Records.FirstOrDefault().Message == "Group Menu Deleted")
            {
                result = true;
            }
            else
            {
                result = false;
            }

        }

        public static void UpdateGroup(GroupMasterModel model, GroupMenuModel groupMenu, out bool result, string Users = null)
        {
            result = false;

            dynamic Records = AppDB.COR_USP_UpdateGroup(GroupId: model.id ,Name: model.Name, Status: model.Status, UpdatedDateTime: DateTime.Now, MenuId: groupMenu.MenuId, AllowEdit: groupMenu.AllowEdit, AllowCreate: groupMenu.AllowCreate, AllowDelete: groupMenu.AllowDelete, AllowView: groupMenu.AllowView, users: Users);

            if (Records.FirstOrDefault() != null && Records.FirstOrDefault().Message == "Group Updated")
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
    }
}
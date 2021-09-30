using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simple.Data;
using Zong_Admin_Panel.Models;
using System.Dynamic;
using System.Text;
using System.Net;

namespace Zong_Admin_Panel.Codebase
{
    public class CommonProcessing
    {
        static dynamic AppDB = Database.OpenNamedConnection("MainDB");
        public static void GetUserByGroupId(out string selectedUsers, out List<dynamic> users, int? groupId = null)
        {
            users = new List<dynamic>();
            selectedUsers = "";

            dynamic Records = AppDB.COR_USP_GetUsersByGroupId(groupId: groupId);

            if (Records.FirstOrDefault() != null)
            {
                foreach (var rec in Records)
                {
                    users.Add(new UserModel() { Id = rec.Id, FirstName = rec.FirstName });
                }
            }

            Records.NextResult();
            if (Records.FirstOrDefault() != null)
            {
                foreach (var rec in Records)
                {
                    selectedUsers += rec.Id + ",";

                }
            }
            selectedUsers = selectedUsers.TrimEnd(',');
        }
        public static string GetIPAddress()
        {
            StringBuilder sb = new StringBuilder();
            String strHostName = string.Empty;
            strHostName = Dns.GetHostName();
            IPHostEntry ipHostEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] address = ipHostEntry.AddressList;
            sb.Append(address[1].ToString());
            sb.AppendLine();
            return sb.ToString();
        }
        public static void GetUser(out List<UserModel> users, int? userid = null)
        {
            users = new List<UserModel>();

            dynamic Records = AppDB.COR_GetAllUsers();

            if (Records.FirstOrDefault() != null)
            {
                foreach (var rec in Records)
                {
                    users.Add(new UserModel() { Id = rec.Id, FirstName = rec.FirstName });
                }
            }
        }
        public static bool GetUserPageRole(int UserType, int MenuId, out dynamic result)
        {
            result = new ExpandoObject(); ;


            dynamic Records = AppDB.INV_WEB_GetUserPageRole(UserType: UserType, MenuId: MenuId).FirstOrDefault();

            if (Records != null)
            {
                result.AllowCreate = Convert.ToInt16(Records.AllowCreate);
                result.AllowEdit = Convert.ToInt16(Records.AllowEdit);
                result.AllowView = Convert.ToInt16(Records.AllowView);
                result.AllowDelete = Convert.ToInt16(Records.AllowDelete);

                return true;
            }

            return false;
        }
        public static bool GetAllUserRoles(int UserType, out IEnumerable<RoleModel> list)
        {
            // result = new ExpandoObject(); ;


            dynamic Records = AppDB.INV_WEB_GetUserRoles(UserType: UserType);

            list = null;

            List<RoleModel> _list = new List<RoleModel>();

            foreach (var rec in Records)
            {
                _list.Add(new RoleModel { Id = rec.Id, MenuId = rec.MenuId, AllowCreate = rec.AllowCreate, AllowEdit = rec.AllowEdit, AllowView = rec.AllowView, AllowDelete = rec.AllowDelete, Controller = rec.Controller, Action = rec.Action });
            }
            if (Records != null)
            {
                list = _list;

                return true;
            }

            return false;
        }
        private static IEnumerable<MenuModel> GetMenuTree(IEnumerable<MenuModel> list, int? parentId)
        {
            return list.Where(x => x.ParentId == parentId).Select(x => new MenuModel
            {
                Id = x.Id,
                ParentId = x.ParentId,
                MenuName = x.MenuName,
                MenuUrl = x.MenuUrl,
                Controller = x.Controller,
                Action = x.Action,
                IconClass = x.IconClass,
                MenuTree = x.MenuTree,
                List = list// GetMenuTree(list, x.Id)
            }).ToList();
        }
        public static void GetAllGroups(out List<GroupMasterModel> groups)
        {
            groups = new List<GroupMasterModel>();

            dynamic Records = AppDB.COR_USP_GetAllGroups();

            if (Records.FirstOrDefault() != null)
            {
                foreach (var rec in Records)
                {
                    groups.Add(new GroupMasterModel() { id = rec.Id, Name = rec.Name, Status = rec.Status, isActive = rec.Status == 1 ? "Active" : "Deactive" });
                }
            }
        }
        public static void GetGroupWithPermissionsTree(out List<dynamic> groups)
        {
            groups = new List<dynamic>();

            dynamic Records = AppDB.COR_USP_GetAllGroupPermissions();

            if (Records.FirstOrDefault() != null)
            {
                foreach (var rec in Records)
                {
                    groups.Add(new
                    {
                        FormMasterId = rec.FormMasterId,
                        FormMasterName = rec.FormMasterName,
                        MenuName = rec.MenuName,
                        MenuId = rec.MenuId,
                        MenuParentId = rec.MenuParentId,
                        MenuUrl = rec.MenuUrl,
                        Controller = rec.Controller,
                        Action = rec.Action,
                        PermissionId = rec.PermissionId,
                        AllowCreate = rec.AllowCreate,
                        AllowDelete = rec.AllowDelete,
                        AllowEdit = rec.AllowEdit,
                        AllowView = rec.AllowView,

                    });
                }
            }

        }

        public static void SetPermissions(dynamic Records, string userid, out List<dynamic> groups)
        {
            if (userid != null)
            {
                Records = AppDB.COR_WEB_UserPermissions(Id: userid);
            }

            groups = new List<dynamic>();

            if (Records.FirstOrDefault() != null)
            {
                foreach (var rec in Records)
                {
                    groups.Add(new
                    {
                        FormMasterId = rec.FormMasterId,
                        FormMasterName = rec.FormMasterName,
                        MenuName = rec.MenuName,
                        MenuId = rec.MenuId,
                        MenuParentId = rec.MenuParentId,
                        MenuUrl = rec.MenuUrl,
                        Controller = rec.Controller,
                        Action = rec.Action,
                        PermissionId = rec.PermissionId,
                        AllowCreate = rec.AllowCreate,
                        AllowDelete = rec.AllowDelete,
                        AllowEdit = rec.AllowEdit,
                        AllowView = rec.AllowView,

                    });
                }
            }


        }
        public static void GetGroupByIdWithPermissionsTree(out List<dynamic> groups, string GroupId = null)
        {
            groups = new List<dynamic>();

            dynamic Result = AppDB.COR_USP_GetGroupPermissionsByGroupId(groupId: GroupId);
            var Records = Result.FirstOrDefault();

            if (Records != null)
            {
                foreach (var rec in Result)
                {
                    groups.Add(new GroupMasterModel()
                    {
                        id = rec.Id,
                        Name = rec.Name,
                        Status = rec.Status
                    });
                }
            }

            Result.NextResult();
            if (Result.FirstOrDefault() != null)
            {
                foreach (var rec in Result)
                {
                    groups.Add(new
                    {
                        FormMasterId = rec.FormMasterId,
                        FormMasterName = rec.FormMasterName,
                        MenuName = rec.MenuName,
                        MenuId = rec.MenuId,
                        MenuParentId = rec.MenuParentId,
                        MenuUrl = rec.MenuUrl,
                        Controller = rec.Controller,
                        Action = rec.Action,
                        PermissionId = rec.PermissionId,
                        AllowCreate = rec.AllowCreate,
                        AllowDelete = rec.AllowDelete,
                        AllowEdit = rec.AllowEdit,
                        AllowView = rec.AllowView,

                    });
                }

            }

        }
    }
}
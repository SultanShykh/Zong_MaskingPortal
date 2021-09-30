using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public class MenuProcessing
    {
        static dynamic AppDB = Database.OpenNamedConnection("MainDB");
        public static void GetMenu(out DTResult<MenuModel> result)
        {
            result = new DTResult<MenuModel>();

            dynamic Records = AppDB.COR_USP_GetMenu();

            if (Records.FirstOrDefault() != null)
            {
                foreach (var v in Records)
                {
                    result.data.Add(new MenuModel()
                    { 
                        Id = v.Id,
                        MenuName = v.MenuName,
                        MenuUrl = v.MenuUrl,
                        Controller = v.Controller,
                        Action = v.Action,
                        IsActive = v.IsActive
                    });
                }
            }
        }

        public static void CreateMenu(MenuModel menuModel, out bool result)
        {
            result = false;
            dynamic Records = AppDB.COR_USP_CreateMenu(action:menuModel.Action, menuName: menuModel.MenuName, menuURL: menuModel.MenuUrl, Controller:menuModel.Controller,isActive: menuModel.IsActive);
            if (Records.FirstOrDefault() != null && Records.FirstOrDefault().Message == "Menu Created")
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }

        public static void DeleteMenu(string menuId, out bool result, out string msg)
        {
            result = false;

            dynamic Records = AppDB.COR_USP_DeleteMenu(MenuId: menuId);

            if (Records.FirstOrDefault() != null && Records.FirstOrDefault().Message == "Menu Deleted")
            {
                result = true;
            }
            else
            {
                result = false;
            }
            msg = Records.FirstOrDefault().Message;
        }
        public static void UpdateMenu(MenuModel menuModel, out bool result)
        {
            result = false;

            dynamic Records = AppDB.COR_USP_UpdateMenu(action: menuModel.Action, menuName: menuModel.MenuName, menuURL: menuModel.MenuUrl, controller: menuModel.Controller, isActive: menuModel.IsActive,MenuId: menuModel.Id);

            if (Records.FirstOrDefault() != null && Records.FirstOrDefault().Message == "Menu Updated")
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
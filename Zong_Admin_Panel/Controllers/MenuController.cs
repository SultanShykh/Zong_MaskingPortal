//using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Zong_Admin_Panel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zong_Admin_Panel.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetMenu() 
        {
            try
            {
                DTResult<MenuModel> result;

                MenuProcessing.GetMenu(out result);

                return Json(result,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult CreateMenu(FormCollection model)
        {
            try
            {
                bool result;
                MenuModel menuModel = new MenuModel();
                menuModel.MenuName = model["menuName"].ToString();
                menuModel.MenuUrl = model["menuUrl"].ToString();
                menuModel.Action = model["action"].ToString();
                menuModel.Controller = model["controller"].ToString();
                menuModel.IsActive = Convert.ToBoolean(model["isActive"]);
                MenuProcessing.CreateMenu(menuModel, out result);

                return Json(new { status = result, message = "Successfully Created!!!" });
            }
            catch (Exception ex)
            {
                string msg = "Error, Please try again.";
                return Json(new { status = false, message = msg });
            }
          
        }

        [HttpPost]
        public ActionResult DeleteMenu(string Id)
        {
            try
            {
                bool result;
                string msg;
                MenuProcessing.DeleteMenu(Id, out result, out msg);
                return Json(true);

            }
            catch (Exception e)
            {
                return Json("Error");
            }
        }

        [HttpPost]
        public ActionResult UpdateMenu(FormCollection model)
        {
            try
            {
                bool result;

                MenuModel menuModel = new MenuModel();
                 menuModel.Id = Convert.ToInt32(model["menuId"]);
                menuModel.MenuName = model["menuName"].ToString();
                menuModel.MenuUrl = model["menuUrl"].ToString();
                menuModel.Action = model["action"].ToString();
                menuModel.Controller = model["controller"].ToString();
                menuModel.IsActive = Convert.ToBoolean(model["isActive"]);
                MenuProcessing.UpdateMenu(menuModel, out result);

                return Json(new { status = result, message = "Successfully Updated!!!" });
            }
            catch (Exception e)
            {
                return Json("Error");
            }

        }
    }
}
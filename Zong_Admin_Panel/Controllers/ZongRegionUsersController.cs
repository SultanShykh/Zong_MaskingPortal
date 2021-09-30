using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zong_Admin_Panel.Codebase;
using Zong_Admin_Panel.Interaces;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Controllers
{
    public class ZongRegionUsersController : Controller
    {
        // GET: ZongRegionUsers
        private IZongRegionsUsers zongRegions;
        List<UserModel> users;
        IZongRegions zongs;
        public ZongRegionUsersController(IZongRegionsUsers zongRegions,IZongRegions zongs)
        {
            this.zongRegions = zongRegions;
            this.zongs = zongs;
             
        }
        public ActionResult Index()
        {
            CommonProcessing.GetUser(out users);
            ViewBag.users = new SelectList(users, "Id", "FirstName");

             var ss= zongs.GetAll();
            ViewBag.zongusers = new SelectList(ss, "Id", "Name");

            return View();
        }
        public ActionResult Add(FormCollection form)
        {
            ZongRegionsUsers zong = new ZongRegionsUsers();
            zong.UsersId =Convert.ToInt32(form["Users"].ToString());
            zong.RegionId =Convert.ToInt32(form["ZongUsers"].ToString());
            var result = zongRegions.Add(zong);
              return Json(result);
        }

        public ActionResult Update(FormCollection form)
        {
            ZongRegionsUsers zong = new ZongRegionsUsers();
            zong.UsersId = Convert.ToInt32(form["Users"].ToString());
            zong.RegionId = Convert.ToInt32(form["ZongUsers"].ToString());
            zong.Id = Convert.ToInt32(form["Id"].ToString());
            var result = zongRegions.Update(zong);

            return Json(result);
        }


        public ActionResult GetAll()
        {
            var result = zongRegions.GetAll();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string Id)
        {
            var result = zongRegions.Delete(Convert.ToInt32(Id));
            return Json(result);
        }
    }
}
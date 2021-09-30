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
    public class ZongRegionsController : Controller
    {
        // GET: ZongRegions
        private IZongRegions zongRegions;
        
        public ZongRegionsController(IZongRegions zongRegions)
        {
            this.zongRegions = zongRegions;
          
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add(ZongRegions zong)
        {
            var result = zongRegions.Add(zong);

            return Json(result);
        } 
       
        public ActionResult Update(ZongRegions zong)
        {
            var result = zongRegions.Update(zong);

            return Json(result);
        } 
        
        
        public ActionResult GetAll()
        {
            var result = zongRegions.GetAll();

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Delete(string Id)
        {
            var result = zongRegions.Delete(Convert.ToInt32(Id));
             return Json(result);
        }
    }
}
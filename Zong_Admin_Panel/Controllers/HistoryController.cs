using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zong_Admin_Panel.Interaces;

namespace Zong_Admin_Panel.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        private IHistory history;

        public HistoryController(IHistory history)
        {
            this.history = history;

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAll()
        {
            try
            {
                var result = history.GetAll();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
               return Json("Error",JsonRequestBehavior.AllowGet);
            }
            }


        public ActionResult HistoryById()
        {

            return View();
        }
        [HttpGet]
        public ActionResult GetById(string Id)
        {
            try
            {
                var result = history.GetById(Id);
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
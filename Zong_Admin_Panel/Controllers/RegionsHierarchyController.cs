using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zong_Admin_Panel.Interaces;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Controllers
{
    public class RegionsHierarchyController : Controller
    {
        // GET: RegionsHierarchy
        private IRegionsHierarchy regionsHierarchy;
        IZongRegions zongs;
        public RegionsHierarchyController(IRegionsHierarchy regionsHierarchy, IZongRegions zongs)
        {
            this.regionsHierarchy = regionsHierarchy;
            this.zongs = zongs;
        }
        public ActionResult Index()
        {
            var ss = zongs.GetAll();
            ViewBag.regions = new SelectList(ss, "Id", "Name");

            if (Request.Cookies["SMSCookies"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Account");
          
        }
        public ActionResult Add(FormCollection form) 
        {
            try
            {
                RegionsHierarchy regions = new RegionsHierarchy();
                 var requestdby= form["RequestedBy"].ToString();
                var requestedto= form["RequestedTo"].ToString();
                
                var rids = requestedto.Split(',').ToArray();
                
                    for (int i = 0; i < rids.Length; i++)
                {
                    regions.RegionRequestbyId = Convert.ToInt32(requestdby);
                    regions.RegionRequesttoId = Convert.ToInt32(rids[i]);
                    regionsHierarchy.Add(regions);
                }

                

                return Json("Added Successfully!!!");
            }
            catch (Exception e)
            {

                return Json("error");
            }
        
        }


        public ActionResult Update(FormCollection form)
        {
            try
            {
                RegionsHierarchy regions = new RegionsHierarchy();
                   var requestedto = form["RequestedTo"].ToString();

                    var rids = requestedto.Split(',').ToArray();

                    for (int i = 0; i < rids.Length; i++)
                    {
                    if (Convert.ToInt32(i) > 0)
                    {
                        
                        regions.RegionRequestbyId = Convert.ToInt32(form["RequestedBy"]);
                        regions.RegionRequesttoId = Convert.ToInt32(rids[i]);
                        regionsHierarchy.Add(regions);


                    }
                    else
                    {
                        regions.RegionRequestbyId = Convert.ToInt32(form["RequestedBy"]);
                        regions.RegionRequesttoId = Convert.ToInt32(rids[i]);
                        regions.Id = Convert.ToInt32(form["Id"]);
                        regionsHierarchy.Update(regions);

                    }
                }
                return Json(true);


            }
            catch (Exception e)
            {

                return Json("error");
            }
        }

        public ActionResult Delete(string Id)
        {
            try
            {
                var result = regionsHierarchy.Delete(Convert.ToInt32(Id));
                return Json(true);
            }
            catch (Exception e)
            {
                return Json("error");
            }
        }
        public ActionResult GetAll() 
        {
            try
            {
                var result = regionsHierarchy.GetAll();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                return Json("error",JsonRequestBehavior.AllowGet);
            }
        
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Zong_Admin_Panel.Interaces;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Controllers
{
    public class SenderController : Controller
    {

        // GET: Sender

        private ISender senders;
        LoginModel val;
        public SenderController(ISender senders)
        {
            this.senders = senders;

        }
        public ActionResult Index()
        {
            if (Request.Cookies["SMSCookies"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Account");

        }


        [HttpPost]
        public ActionResult Add(Sender sender)
        {
            try
            {
                if (Request.Cookies["SMSCookies"] != null)
                {
                    val = Cookies.getCookieValue();
                    sender.userId = val.UserId;

                    string path = "";
                    string multiplefiles = "";
                    if (Request.Files.Count > 0)
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            var file = Request.Files[i];
                            var fileName = Path.GetFileName(file.FileName);
                            if (fileName.Contains(" "))
                            {
                                fileName = Regex.Replace(fileName, @"\s+", "");
                                path = Path.Combine(Server.MapPath("~/UploadedFiles/"), fileName);
                                file.SaveAs(path);
                                multiplefiles += path + ",";
                            }
                            else if (fileName != "")
                            {
                                path = Path.Combine(Server.MapPath("~/UploadedFiles/"), fileName);
                                file.SaveAs(path);
                                multiplefiles += path + ",";
                            }
                        }
                    }
                    if (multiplefiles != "")
                    {
                        sender.Attachment = multiplefiles.TrimEnd(',');
                    }
                    sender.Status = "1";
                    var result = senders.Add(sender);
                    return Json(result);
                }
               
                    return RedirectToAction("Index","Account");
               

              
            }
            catch (Exception e)
            {
                return Json("");

            }
           
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var result = senders.GetAll();
                return Json(result,JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json("");
            }
            
        }
    }
}
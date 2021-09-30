using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Zong_Admin_Panel.Codebase;
using Zong_Admin_Panel.Interaces;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Controllers
{
    public class SenderController : Controller
    {

        // GET: Sender

        private ISender senders;
        private IRoute routes;
        List<UserModel> users;

        LoginModel val;
        public SenderController(ISender senders, IRoute routes, List<UserModel> users)
        {
            this.senders = senders;
            this.routes = routes;
            this.users = users;

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
                    sender.request = val.UserId;
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

                            }
                            else if (fileName != "")
                            {
                                path = Path.Combine(Server.MapPath("~/UploadedFiles/"), fileName);
                                file.SaveAs(path);

                            }
                            multiplefiles += path + ",";
                        }
                    }
                    if (multiplefiles != "")
                    {
                        sender.Attachment = multiplefiles;
                    }

                    sender.Status = "2";

                    if (val.UserType == "2")
                    {
                        sender.Requestedto = 1;
                    }
                    else
                    {
                        sender.Requestedto = 2;
                    }

                    var result = senders.Add(sender);
                    return Json(result);


                }
                return RedirectToAction("Index", "Account");
            }
            catch (Exception e)
            {
                return Json("error");

            }

        }

        [HttpGet]
        public ActionResult GetAll()
            {
            try
            {
                if (Request.Cookies["SMSCookies"] != null)
                {
                    val = Cookies.getCookieValue();
                    int usertype = Convert.ToInt32(val.UserType);
                    var userid = val.UserId;
                    var result = senders.GetAll("InProcess", usertype, userid, val.RegionId.ToString());


                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return RedirectToAction("Index", "Account");
            }
            catch (Exception e)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        public ActionResult Delete(string Id)
        {

            var result = senders.Delete(Convert.ToInt32(Id));
            return Json(result);
        }

        [HttpPost]
        public ActionResult Update(Sender sender, string AAA)
        {
            //var ass = form["reason"].ToString();
            try
            {
                if (Request.Cookies["SMSCookies"] != null)
                {
                    val = Cookies.getCookieValue();
                    sender.userId = val.UserId;
                    sender.request = val.UserId;

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
                        sender.Attachment = multiplefiles;
                    }

                    switch (sender.RevertedByyy)
                    {
                        //case "HQ":
                        //    {
                        //        sender.Requestedto = 4;
                        //        break;
                        //    }

                        case "ITSAdmin":
                            {
                                sender.Requestedto = 1;
                                break;
                            }
                        case "ZongAdmin":
                            {
                                sender.Requestedto = 2;
                                break;
                            }
                        default:
                            {
                                if (val.UserType == "2")
                                {
                                    sender.Requestedto = 1;
                                }
                                //else if (val.UserType == "4")
                                //{
                                //    sender.Requestedto = 1;
                                //}
                                else
                                {
                                    sender.Requestedto = 2;
                                }
                                break;
                            }
                    }


                    sender.Status = "2";
                    var result = senders.Update(sender);
                    return Json(true);
                }
                return RedirectToAction("Index", "Account");
            }
            catch (Exception e)
            {
                return Json("Error");
            }

        }
        public ActionResult Download(string Files)
        {
            try
            {
                string path = Server.MapPath("~/UploadedFiles/");
                string Filename = Path.GetFileName(Files);
                string fullpath = Path.Combine(path, Filename);
                return File(fullpath, ".", Filename);
            }
            catch (Exception e)
            {
                return Json("Error");
            }
        }



        #region InProcess

        public ActionResult Reverted(Sender sender, string Name)
        {
            try
            {
                var multipulefiles = "";
                if (Request.Cookies["SMSCookies"] != null)
                {
                    val = Cookies.getCookieValue();

                    sender.request = val.UserId;
                    multipulefiles = sender.Attachment + ",";
                    sender.Reverteby = val.UserId;
                    sender.Reason = Name;


                    sender.Requestedto = sender.CreatorType; // USer TypeThe one Who Created this mask
                    //if (val.UserType == "2")
                    //{
                    //    sender.Requestedto = 3;

                    //}
                    //else
                    //{
                    //    sender.Requestedto = 2;
                    //}
                    sender.Status = "2";

                }
                var result = senders.Update(sender);
                return Json(true);
            }
            catch (Exception e)
            {
                return Json("Error");
            }

        }
       
        public ActionResult Approved(Sender sender, string Name)
        {
            try
            {

                var multipulefiles = "";
                if (Request.Cookies["SMSCookies"] != null)
                {
                    val = Cookies.getCookieValue();

                    multipulefiles = sender.Attachment + ",";
                    sender.Approveby = val.UserId;
                    sender.Reason = Name;
                    if (val.UserType == "2")
                    {
                        sender.Requestedto = 1;
                    }

                    else
                    {
                        sender.Requestedto = 0;
                    }

                    if (val.UserType == "2")
                    {
                        sender.Status = "2";
                        sender.request = val.UserId;

                    }
                    else
                    {
                        sender.Status = "3";
                    }

                }
                var result = senders.Update(sender);
                return Json(true);
            }
            catch (Exception e)
            {
                return Json("Error");
            }
        }



        [HttpPost]
        public ActionResult UploadBulk(FormCollection form)
        {
            try
            {
                string path = "";
                //List<DataRow> list =new List<DataRow>();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string list = "";
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    var fileName = Path.GetFileName(file.FileName);

                    //Use the following properties to get file's name, size and MIMEType
                    int fileSize = file.ContentLength;
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;
                    //To save file, use SaveAs method
                    if (fileName.EndsWith(".xls") || fileName.EndsWith(".xlsx") || fileName.EndsWith(".csv"))
                    {
                        if (fileName.Contains(" "))
                        {
                            fileName = Regex.Replace(fileName, @"", "");
                            path = Path.Combine(Server.MapPath("~/UploadedFiles/"), fileName);
                            file.SaveAs(path);
                        }
                        else if (fileName != "")
                        {   
                            path = Path.Combine(Server.MapPath("~/UploadedFiles/"), fileName);
                            file.SaveAs(path);
                        }

                        DataTable data = new DataTable();
                        //if (fileName.EndsWith(".csv"))
                        //{
                        //    data = CsvExtensions.ConvertCSVtoDataTable(path);
                        //}
                        if (Request.Cookies["SMSCookies"] != null)
                        {
                            int Requestedto = 2;
                            val = Cookies.getCookieValue();
                            if (val.UserType == "2")
                            {
                                Requestedto = 4;
                            }

                            data = CsvExtensions.GetDataFromExcel(path, val.UserId, val.UserId, Requestedto);
                            var lst = data.AsEnumerable()
                    .Select(r => r.Table.Columns.Cast<DataColumn>()
                            .Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])
                           ).ToDictionary(z => z.Key, z => z.Value)
                    ).ToList();
                            //now serialize it
                            list = serializer.Serialize(lst);
                            //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();


                        }
                        //if (data.Rows.Count > 0)
                        //{

                        //    var results = senders.UploadBulk(data);

                        //}
                    }

                }


                else
                {
                    return Json("Please Upload xlsx or xls Files Only");
                }

                return Json(list);

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public FileResult Sample()
        {
            try
            {
                string fileName = "Masks.xlsx";
                byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/" + fileName));
                return File(fileBytes, "application/vnd.ms-excel", fileName);
            }
            catch (Exception ex)
            {
                ViewBag.SubmissionError = ex.Message;
            }
            return null;
        }

        public ActionResult InProcessUpdate(Sender sender)
        {

            return Json("Error");

        }

        #endregion

        [HttpGet]
        public ActionResult AddMasking()
        {
            return View();
        }



        [HttpGet]
        public ActionResult Rejected()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Rejected(Sender sender,string Name)
        {
            try
            {
                var multipulefiles = "";
                if (Request.Cookies["SMSCookies"] != null)
                {
                    val = Cookies.getCookieValue();

                    multipulefiles = sender.Attachment + ",";
                    sender.rejected = val.UserId;
                    sender.Reason = Name;

                    sender.Status = "4";
                    sender.request = val.UserId;

                }
                var result = senders.Update(sender);
                return Json(true);
            }
            catch (Exception e)
            {
                return Json("Error");
            }

        }

        #region Complete
        public ActionResult Complete()
        {
            return View();
        }
        public JsonResult CompleteGetAll()
        {
            try
            {
                val = Cookies.getCookieValue();
                var Status = "Approved";
                int usertype = Convert.ToInt32(val.UserType);
                int userid = Convert.ToInt32(val.UserId);
                string region = val.RegionId.ToString();
                var result = senders.GetAll(Status, usertype, userid, region);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Error");
            }

        }
        public ActionResult RouteComplete()
        {
            return View();
        }
        public JsonResult TelcosGetAll()
        {
            try
            {
                val = Cookies.getCookieValue();
                var Status = "Complete";
                int usertype = Convert.ToInt32(val.UserType);
                int userid = Convert.ToInt32(val.UserId);
                string region = val.RegionId.ToString();
                var result = senders.GetAll(Status, usertype, userid, region);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Error");
            }

        }

        public JsonResult RejectedGetAll()
        {
            try
            {
                val = Cookies.getCookieValue();
                var Status = "Rejected";
                int usertype = Convert.ToInt32(val.UserType);
                int userid = Convert.ToInt32(val.UserId);
                string region = val.RegionId.ToString();
                var result = senders.GetAll(Status, usertype, userid, region);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        public JsonResult TelcosComplete(Sender sender,  bool Ufone, bool Jazz, bool Telenor)
        {
            try
            {
                sender.Jazz = Jazz;
                //sender.Zong = Zong;
                //sender.Warid = Warid;
                sender.Ufone = Ufone;
                sender.Telenor = Telenor;

                if (sender.Jazz != true)
                {
                    sender.Status = "3";
                }
                //else if (sender.Zong != true)
                //{
                //    sender.Status = "3";
                //}
                //else if (sender.Warid != true)
                //{
                //    sender.Status = "3";
                //}
                else if (sender.Ufone != true)
                {
                    sender.Status = "3";
                }
                else if (sender.Telenor != true)
                {
                    sender.Status = "3";
                }
                else
                {
                    sender.Status = "5";
                }

                
                var result = senders.TelcosUpdate(sender);
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json("Error");
            }
        }


        public ActionResult Bulk()
        {
            return View();
        }


        [HttpPost]
        public JsonResult SaveBulk(List<BulkModel> data)
        {

            try
            {
                var list = CsvExtensions.ToDataTable(data);
                var result = senders.UploadBulk(list);
                return Json(result);
            }
            catch (Exception e)
            {
                return Json("Error");
            }

        }

        [HttpPost]
        public JsonResult SaveBulkss(FormCollection data)
        {

            try
            {
                string path = "";

                var datas = data["rowdata"].ToString();
                /*  var list =  JsonConvert.DeserializeObject<Sender>(data["rowdata"])*//*;*/
                dynamic dyn = JsonConvert.DeserializeObject<List<Root>>(data["rowdata"]);
                
                // Sender [] list = new JavaScriptSerializer().Deserialize<Sender[]>(datas);

                if (Request.Files.Count > 0)
                {
                    foreach (var v in dyn)
                    {

                        string multiplefiles = "";
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            var file = Request.Files[i];
                            var fileName = Path.GetFileName(file.FileName);
                            if (fileName.Contains(" "))
                            {
                                var split = v.Attachment.Split(',');

                                foreach (var item in split)
                                {
                                    if (item == fileName)
                                    {
                                        fileName = Regex.Replace(fileName, @"\s+", "");
                                        path = Path.Combine(Server.MapPath("~/UploadedFiles/"), fileName);
                                        file.SaveAs(path);
                                        multiplefiles += path + ",";
                                    }

                                }

                            }
                            else if (fileName != "")
                            {
                                path = Path.Combine(Server.MapPath("~/UploadedFiles/"), fileName);
                                var split = v.Attachment.Split(',');

                                foreach (var item in split)
                                {
                                    if (item == fileName)
                                    {
                                        file.SaveAs(path);
                                        multiplefiles += path + ",";
                                    }
                                    //v.Attachment = path;
                                    //dyn.Where(x => x.Attachment == fileName).FirstOrDefault();

                                }
                            }


                        }
                        v.Attachment = multiplefiles;
                    }

                }
           
               
                var list = CsvExtensions.ToDataTable(dyn);
               var result = senders.UploadBulk(list);
                return Json(result);
            }
            catch (Exception e)
            {
                return Json("Error");
            }

        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult ReportGetAll(FormCollection form)
        {
            try 
            {
                if (Request.Cookies["SMSCookies"] != null)
                {
                    var todate = "";
                    var fromdate = "";
                    string account=null;
                    string masking =null;
                    string ntn = null;
                    string Username = null;
                    if (form.Count != 0)
                    {
                        todate = form["todate"].ToString()==""?null: form["todate"].ToString();
                        fromdate = form["fromdate"].ToString()==""?null: form["fromdate"].ToString();
                        account =form["AccountNumber"]==""?null: form["AccountNumber"];
                        masking = form["maskingId"] == "" ? null : form["maskingId"];
                        ntn = form["ntn"] == "" ? null : form["ntn"];
                        Username = form["Username"] == "" ? null : form["Username"];
                    }
            val = Cookies.getCookieValue();
            int usertype = Convert.ToInt32(val.UserType);
            var userid = val.UserId;
         
            var result = senders.ReportsGetAll(usertype, userid,Convert.ToInt32(val.RegionId.ToString()),todate,fromdate,account,masking,ntn,Username);
           return Json(result, JsonRequestBehavior.AllowGet);
           
                }
                
                return RedirectToAction("Index", "Account");
        
            }
            catch (Exception e)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
    }
}
       
  






    }
}
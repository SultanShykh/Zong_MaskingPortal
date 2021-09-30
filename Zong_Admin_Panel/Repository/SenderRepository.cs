using Simple.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Zong_Admin_Panel.Interaces;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Repository
{
    public class SenderRepository : ISender
    {
        private dynamic PayDB;
        public SenderRepository()
        {
            this.PayDB = Database.OpenNamedConnection("MainDB");
        }
        public string Add(Sender item)
        {


            var result = PayDB.SenderCreate(Masking: item.Masking, NTN: item.NTN_FTN, Status: Convert.ToInt32(item.Status), Attachments: item.Attachment, AccountNumber: item.AccountNumber, NTN_FTN_Status: item.NTN_FTN_Status, Priority: item.Priority, Requestedto: item.Requestedto, Operators: Convert.ToInt32(0), Comments: item.Comments, RequestedBy: item.request, userId: item.userId);
            return "Added Successfully!!!";

        }

        public bool Delete(int id)
        {
            var Result = PayDB.SenderDelete(Id: id);
            return true;
        }



        public Sender Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sender> GetAll(string Status, int usertype, int userid, string regionId)
        {
            if (usertype == 1)
            {
                regionId = null;
            }
            LoginModel val = Cookies.getCookieValue();
            Sender sender;
            dynamic Result;
            List<Sender> senders = new List<Sender>();
            if (val.UserType == "2")
            {
                Result = PayDB.SendersGetAll1(Status, usertype, userid);
            }
            else if (val.UserType == "1" && Status == "Rejected")
            {
                Result = PayDB.SendersGetAll1(Status, usertype, userid);
            }
            else if (val.UserType == "1" && Status == "Approved") 
            {
                Result = PayDB.SendersGetAll1(Status, usertype, userid);
            }
            else if(regionId == null && Status =="Complete")
            {
                Result = PayDB.SendersGetAll1(Status, usertype, userid);
            }
            else
            {
                Result = PayDB.SendersGetAll2(Status, usertype, userid, regionId);
            }
            foreach (var item in Result)
            {

                senders.Add(new Sender()
                {

                    Id = item.Id,
                    AccountNumber = item.AccountNumber,
                    userId = item.UserId,
                    user = val.UserId,
                    Username = item.Username,
                    Masking = item.Sender,
                    NTN_FTN = item.NTN,
                    NTN_FTN_Status = item.NTN_FTN_Status,
                    Usertypess = usertype,
                    NTN_FTN_Statuses = item.NTN_FTN_Status == true ? "Verified" : Convert.ToInt32(item.NTN_FTN_Status) == 1 ? "Verified" : "Non Verified",
                    Operators = item.Operators == 1 ? "On Net" : "OFF NET",
                    Comments = item.Comments,
                    Priorities = item.Priority == 0 ? "Normal" : "Urgent",
                    Status = item.Status,
                    Attachment = item.Attachment == "" ? " -- " : item.Attachment,
                    //Requestedto=item.RequestedTo,
                    RequestedToo = item.RequestedTo == 2 ? "ZongHQ" : item.RequestedTo == 3 ? "Users" : item.RequestedTo == 1 ? "ITS Admin" : "--",
                    RequestedBY = item.RequestedBy == null ? "--" : item.RequestedBy,
                    //Approveby=item.ApprovedBy,
                    approved = item.approvedId,
                    ApprovedByyy = item.ApprovedBy == null ? "--" : item.ApprovedBy,
                    Jazz = item.Jazz,
                    j = item.Jazz == true ? "Complete" : "Pending",
                    Warid = item.Warid,
                    w = item.Warid == true ? "Complete" : "Pending",
                    Ufone = item.Ufone,
                    u = item.Ufone == true ? "Complete" : "Pending",
                    Zong = item.zong,
                    z = item.zong == true ? "Complete" : "Pending",
                    Telenor = item.Telenor,
                    t = item.Telenor == true ? "Complete" : "Pending",
                    //Reverteby = item.RevertedBy,
                    RevertedByyy = item.RevertedBy == null ? "--" : item.RevertedBy,
                    CreatedDate = item.CreatedDate.ToString("dd-MMM-yyyy hh:mm"),
                    Reason = item.Reason == null ? "--" : item.Reason,
                    CurrentStatus = item.RequestedTo == 2 ? "Requested To ZongHQ" : item.RequestedTo == 3 ? "Reverted by " + item.RevertedBy : item.RequestedTo == 1 ? "Requested To ITSAdmin" : item.RejectedBy != null ? "Rejected By " + item.RejectedBy : "--",
                    CreatorType = item.CreatorType,
                    TatStatus = item.TATStatus,
                    DaysElapsed = item.DaysElapsed,
                    op = "Pending",
                });
            }
            return senders;
        }

        public IEnumerable<Sender> ReportsGetAll(int usertype, int userid, int regionId, string todate, string fromdate, string account, string masking, string ntn, string Username)
        {
            LoginModel val = Cookies.getCookieValue();
            Sender sender;
            dynamic Results;
            List<Sender> senders = new List<Sender>();
            if (val.UserType != "2" && val.UserType != "1")
            {
                if(fromdate == "" || todate=="")
                {
                    fromdate = null;
                    todate = null;
                }
                
                Results = PayDB.FilterDate1(usertype, userid, regionId,fromdate,todate,account,masking,ntn,Username);
            }
            else if (todate != "" || fromdate !="" || account!=null || masking!=null || ntn !=null || Username !=null)
            {
                Results = PayDB.FilterDate(fromdate,todate,account,masking,ntn,Username);
            }
            else
            {

                Results = PayDB.ReportGetAll1();
                 
            }
            foreach (var item in Results)
            {

                senders.Add(new Sender()
                {
                    Id = item.Id,
                    AccountNumber = item.AccountNumber,
                    userId = item.UserId,
                    user = val.UserId,
                    Username = item.Username,
                    Masking = item.Sender,
                    NTN_FTN = item.NTN,
                    NTN_FTN_Status = item.NTN_FTN_Status,
                    Usertypess = usertype,
                    NTN_FTN_Statuses = item.NTN_FTN_Status == true ? "Verified" : Convert.ToInt32(item.NTN_FTN_Status) == 1 ? "Verified" : "Non Verified",
                    Operators = item.Operators == 1 ? "On Net" : "OFF NET",
                    Comments = item.Comments,
                    Priorities = item.Priority == 0 ? "Normal" : "Urgent",
                    Status = item.Status,
                    Attachment = item.Attachment == "" ? " -- " : item.Attachment,
                    //Requestedto=item.RequestedTo,
                    RequestedToo = item.RequestedTo == 2 ? "ZongHQ" : item.RequestedTo == 3 ? "Users" : item.RequestedTo == 1 ? "ITS Admin" : "--",
                    RequestedBY = item.RequestedBy == null ? "--" : item.RequestedBy,
                    //Approveby=item.ApprovedBy,
                    approved = item.approvedId,
                    ApprovedByyy = item.ApprovedBy == null ? "--" : item.ApprovedBy,
                    Jazz = item.Jazz,
                    j = item.Jazz == true ? "Complete" : "Pending",
                    Warid = item.Warid,
                    w = item.Warid == true ? "Complete" : "Pending",
                    Ufone = item.Ufone,
                    u = item.Ufone == true ? "Complete" : "Pending",
                    Zong = item.zong,
                    z = item.zong == true ? "Complete" : "Pending",
                    Telenor = item.Telenor,
                    t = item.Telenor == true ? "Complete" : "Pending",
                    //Reverteby = item.RevertedBy,
                    RevertedByyy = item.RevertedBy == null ? "--" : item.RevertedBy,
                    CreatedDate = item.CreatedDate.ToString("dd-MMM-yyyy hh:mm"),
                    Reason = item.Reason == null ? "--" : item.Reason,
                    CurrentStatus = item.RequestedTo == 2 ? "Requested To ZongHQ" : item.RequestedTo == 3 ? "Reverted by " + item.RevertedBy : item.RequestedTo == 1 ? "Requested To ITSAdmin" : item.RejectedBy != null ? "Rejected By " + item.RejectedBy : "Completed By ITSAdmin",
                    CreatorType = item.CreatorType,
                    TatStatus = item.TATStatus,
                    DaysElapsed = item.DaysElapsed,
                    op = "Pending",
                });
            }
            return senders;
        }

    
        public bool TelcosUpdate(Sender item)
        {
            var Result = PayDB.TelcosComplete(zong: item.Zong, telenor: item.Telenor, jazz: item.Jazz, warid: item.Warid, ufone: item.Ufone, Id: item.Id, Status: item.Status);
            return true;
        }

        public bool Update(Sender item)
        {

            LoginModel val;
            val = Cookies.getCookieValue();
            if (val.UserType == "2")
            {
                var result = PayDB.SenderUpdate(Masking: item.Masking, NTN: item.NTN_FTN, Status: Convert.ToInt32(item.Status), Attachments: item.Attachment, AccountNumber: item.AccountNumber, NTN_FTN_Status: item.NTN_FTN_Status, Priority: item.Priority, Operators: 0, Comments: item.Comments, userId: item.userId, Id: item.Id, Reason: item.Reason, RequestedBy: item.request, Requestedto: item.Requestedto, RevertedBy: item.Reverteby, ApprovedBy: item.Approveby, RejectedBy: item.rejected);
            }
            else if (val.UserType == "3")
            {
                var result = PayDB.SenderUpdate(Masking: item.Masking, NTN: item.NTN_FTN, Status: Convert.ToInt32(item.Status), Attachments: item.Attachment, AccountNumber: item.AccountNumber, NTN_FTN_Status: item.NTN_FTN_Status, Priority: item.Priority, Operators: 0, Comments: item.Comments, userId: item.userId, Id: item.Id, Reason: item.Reason, RequestedBy: item.request, Requestedto: item.Requestedto, RevertedBy: item.Reverteby, ApprovedBy: item.Approveby);
            }

            else if (val.UserType == "1")
            {
                var result = PayDB.SenderUpdate(Masking: item.Masking, NTN: item.NTN_FTN, Status: Convert.ToInt32(item.Status), Attachments: item.Attachment, AccountNumber: item.AccountNumber, NTN_FTN_Status: item.NTN_FTN_Status, Priority: item.Priority, Operators: 0, Comments: item.Comments, userId: item.userId, Id: item.Id, Reason: item.Reason, RequestedBy: item.request, Requestedto: item.Requestedto, RevertedBy: item.Reverteby, ApprovedBy: item.Approveby, RejectedBy: item.rejected);
            }
            else
            {
                var result = PayDB.SenderUpdateApproved(Masking: item.Masking, NTN: item.NTN_FTN, Status: Convert.ToInt32(item.Status), Attachments: item.Attachment, AccountNumber: item.AccountNumber, NTN_FTN_Status: item.NTN_FTN_Status, Priority: item.Priority, Operators: 0, Comments: item.Comments, userId: item.userId, Id: item.Id, Reason: item.Reason, Requestedto: item.Requestedto, RevertedBy: item.Reverteby, ApprovedBy: item.Approveby, RejectedBy: item.rejected);
            }
            return true;
        }


        public string UploadBulk(DataTable dataTables)
        {
             var Result = PayDB.SendersBulkAdd(TempTable: dataTables);
                return "Added Successfully !!!";
           
        }

    }
}
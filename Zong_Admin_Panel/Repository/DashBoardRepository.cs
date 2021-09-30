using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zong_Admin_Panel.Interaces;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Repository
{
    public class DashBoardRepository : IDashboard
    {
        private dynamic PayDB;
        public DashBoardRepository()
        {
            this.PayDB = Database.OpenNamedConnection("MainDB");
        }
        public IEnumerable<DashBoard> GetDashboard(int userid)
        {
            LoginModel val;
            val = Cookies.getCookieValue();
            if (val.UserType != "2" && val.UserType != "1")
            {
                var Result = PayDB.DashboardGetAll(userid);
                List<DashBoard> senders = new List<DashBoard>();
                foreach (var item in Result)
                {
                    senders.Add(new DashBoard()
                    {
                        Today = item.today,
                        Week = item.thisweek,
                        Month = item.thismonth,

                    });
                }
                Result.NextResult();
                foreach (var item in Result)
                {
                    senders.Add(new DashBoard()
                    {
                        Total = item.counted,
                        CreatedDateTime = item.CreatedDate.ToString("dd-MMM-yyyy hh:mm"),

                    });
                }
                return senders;
            }
            else
            {

                var Result = PayDB.DashboardGetAllforadmin();
                List<DashBoard> senders = new List<DashBoard>();
                foreach (var item in Result)
                {
                    senders.Add(new DashBoard()
                    {
                        Today = item.today,
                        Week = item.thisweek,
                        Month = item.thismonth,

                    });
                }
                Result.NextResult();
                foreach (var item in Result)
                {
                    senders.Add(new DashBoard()
                    {
                        Total = item.counted,
                        CreatedDateTime = item.CreatedDate.ToString("dd-MMM-yyyy hh:mm"),

                    });
                }
                return senders;
            }
            
        }
    }
}
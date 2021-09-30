using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zong_Admin_Panel.Interaces;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Repository
{
    public class HistoryRepository : IHistory
    {
        private dynamic PayDB;
        public HistoryRepository()
        {
            this.PayDB = Database.OpenNamedConnection("MainDB");

        }
        public IEnumerable<History> GetAll()
        {
            List<History> history = new List<History>();
            var Result = PayDB.HistoryGetAll1();
            foreach (var v in Result)
            {
                history.Add(new History()
                {
                    Id = v.Id,
                    UserName = v.UserName,
                    Activity = v.Activity,
                    Activitytype = v.Activitytype,
                    MaskingId = v.MaskingId,
                    Date = v.Date.ToString("dd-MMM-yyyy hh:mm"),
                });
            }
            return history;
        }



        public IEnumerable<History> GetById(string Id)
        {
            List<History> history = new List<History>();
            var Result = PayDB.ZongHistoryGetById1(Id: Id);
            foreach (var v in Result)
            {
                history.Add(new History()
                {
                    UserName = v.UserName,
                    Activity = v.Activity,
                    Activitytype = v.Activitytype,
                    MaskingId = v.MaskingId,
                    Date = v.Datetime.ToString("dd-MMM-yyyy hh:mm"),
                });
            }
            return history;
        }
    }
}
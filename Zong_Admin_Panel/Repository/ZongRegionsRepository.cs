using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zong_Admin_Panel.Interaces;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Repository
{
    public class ZongRegionsRepository : IZongRegions
    {
        private dynamic PayDB;
        public ZongRegionsRepository()
        {
            this.PayDB = Database.OpenNamedConnection("MainDB");

        }
        public string Add(ZongRegions item)
        {

            var result = PayDB.ZongRegionsAdd(Name: item.Name);
            return "Added Successfully!!!";

        }

        public bool Delete(int id)
        {
            var result = PayDB.ZongRegionsDelete(Id: id);
            return true;
        }

        public Sender Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ZongRegions> GetAll()
        {
            List<ZongRegions> senders = new List<ZongRegions>();
            var Result = PayDB.ZongRegionsGetAll();
            foreach (var item in Result)
            {
                senders.Add(new ZongRegions()
                {
                    Id = item.Id,
                   Name=item.Name,
                });

            }
            return senders;
        }

        public bool Update(ZongRegions item)
        {
            var result = PayDB.ZongRegionsUpdate(Name:item.Name,Id:item.Id);
            return true;
        }

        ZongRegions IZongRegions.Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
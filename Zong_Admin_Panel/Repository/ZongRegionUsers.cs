using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zong_Admin_Panel.Interaces;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Repository
{
    public class ZongRegionUsers : IZongRegionsUsers
    {
        private dynamic PayDB;
        public ZongRegionUsers()
        {
            this.PayDB = Database.OpenNamedConnection("MainDB");

        }
        public string Add(ZongRegionsUsers item)
        {
            var result = PayDB.ZongRegionUsersAdd(UserId: item.UsersId,RegionId:item.RegionId);
            return "Added Successfully!!!";
        }

        public bool Delete(int id)
        {
            var result = PayDB.ZongRegionUsersDelete(Id: id);
            return true;
        }

        public ZongRegionsUsers Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ZongRegionsUsers> GetAll()
        {
            List<ZongRegionsUsers> senders = new List<ZongRegionsUsers>();
            var Result = PayDB.ZongRegionUsersGetAll();
            foreach (var item in Result)
            {
                senders.Add(new ZongRegionsUsers()
                {
                    Id = item.Id,
                     Username = item.FirstName,
                     UsersId=item.UserId,
                     Regionname = item.Name,
                     RegionId=item.RegionId
                });

            }
            return senders;
        }

        public bool Update(ZongRegionsUsers item)
        {
            var result = PayDB.ZongRegionUsersUpdate(UserId: item.UsersId, RegionId: item.RegionId,Id:item.Id);
            return true;
        }
    }
}
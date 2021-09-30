using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zong_Admin_Panel.Interaces;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Repository
{
    public class RegionsHierarchyRepository : IRegionsHierarchy
    {
        private dynamic PayDB;
        public RegionsHierarchyRepository()
        {
            this.PayDB = Database.OpenNamedConnection("MainDB");
        }
        public string Add(RegionsHierarchy item)
        {
            var result = PayDB.RegionsHierarchyCreate(RegionRequestbyId:item.RegionRequestbyId, RegionRequesttoId:item.RegionRequesttoId);
            return "Added Successfully!!!";
        }

        public bool Delete(int id)
        {
            var Result = PayDB.RegionsHierarchyDelete(Id: id);
            return true;
        }

        public RegionsHierarchy Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegionsHierarchy> GetAll()
        {
            List<RegionsHierarchy> regionhirarchy = new List<RegionsHierarchy>();
            var Result = PayDB.RegionsHierarchyGetAll();
            foreach (var v in Result)
            {
                regionhirarchy.Add(new RegionsHierarchy()
                {
                    Id=v.Id,
                    RegionRequestbyId=v.RegionRequestbyId,
                    RegionRequesttoId=v.RegionRequestedToId,
                    Requestedby = v.RequestedbyName,
                    Requestedto = v.RequestedtoName,

                });
            }
            return regionhirarchy;
        }

        public bool Update(RegionsHierarchy item)
        {
            var result = PayDB.RegionsHierarchyUpdate(RegionRequestbyId: item.RegionRequestbyId, RegionRequesttoId: item.RegionRequesttoId,Id:item.Id);
            return true;
        }
    }
}
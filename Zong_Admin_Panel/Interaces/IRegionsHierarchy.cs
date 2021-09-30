using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Interaces
{
   public interface IRegionsHierarchy
    {
        IEnumerable<RegionsHierarchy> GetAll();
        RegionsHierarchy Get(int id);
        string Add(RegionsHierarchy item);
        bool Update(RegionsHierarchy item);
        bool Delete(int id);
    }
}

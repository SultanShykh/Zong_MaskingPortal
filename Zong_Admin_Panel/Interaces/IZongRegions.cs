using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Interaces
{
   public interface IZongRegions
    {
        IEnumerable<ZongRegions> GetAll();
        ZongRegions Get(int id);
        string Add(ZongRegions item);
        bool Update(ZongRegions item);
        bool Delete(int id);
    }
}

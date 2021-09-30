using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Interaces
{
   public interface IZongRegionsUsers
    {
        IEnumerable<ZongRegionsUsers> GetAll();
        ZongRegionsUsers Get(int id);
        string Add(ZongRegionsUsers item);
        bool Update(ZongRegionsUsers item);
        bool Delete(int id);
    }
}

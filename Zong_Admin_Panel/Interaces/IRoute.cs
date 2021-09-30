using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Interaces
{
   public interface IRoute
    {
        IEnumerable<Routes> GetAll(int? Id);
        string Add(Routes route);
    }
}

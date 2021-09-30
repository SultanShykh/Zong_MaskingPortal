using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Interaces
{
  public  interface IHistory
    {
        IEnumerable<History> GetAll();
        IEnumerable<History> GetById(string Id);
    }
}

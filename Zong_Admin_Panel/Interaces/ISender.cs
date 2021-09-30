using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Interaces
{
    public interface ISender
    {
        IEnumerable<Sender> GetAll(string Status, int usertype, int userid, string regionid);  
        IEnumerable<Sender> ReportsGetAll(int usertype, int userid, int regionid,string todate,string formdate,string account,string masking,string ntn,string Username);

        bool TelcosUpdate(Sender item);
        Sender Get(int id);
        string Add(Sender item);
        bool Update(Sender item);
        bool Delete(int id);
        string UploadBulk(DataTable item);
        
    }
}

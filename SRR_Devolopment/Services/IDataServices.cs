using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;

namespace SRR_Devolopment.Services
{
    /// <summary>
    /// Interface For the Data services
    /// </summary>
    public interface IDataServices
    {
        ObservableCollection<SRR_M_User_Login_H> GetLoginID(String UserName, String Password);
        bool saveLogin(string UserName, String Name, String Password);
        
    }

}

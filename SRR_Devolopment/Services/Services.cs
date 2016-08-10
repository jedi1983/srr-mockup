using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;

namespace SRR_Devolopment.Services
{
    public class DataAccessServices : IDataServices

    {

        //ctor
        
        public ObservableCollection<SRR_M_User_Login_H> GetLoginID(String UserName, String Password)
        {

            try
            {
                using (srr_devEntities sr = new srr_devEntities())
                {
                    var linqQuery = from tbl in sr.SRR_M_User_Login_H
                                    where tbl.User_Name == UserName && tbl.Password.Contains(Password)
                                    select tbl;

                    return new ObservableCollection<SRR_M_User_Login_H>(linqQuery);

                }
            }

            catch
            {
                throw new Exception("Database Error");
            }
        }
        public bool saveLogin(string UserName, String Name, String Password)
        {   
        //

            try
            {

            }
            catch
            {

            }

        return true;
        }

        public ObservableCollection<SRR_KK_M_Screen_Master_H> GetScreenMenu()
        {
            try{
                    using (srr_devEntities sr = new srr_devEntities())
                            {
                                var linqQuery = from tbl in sr.SRR_KK_M_Screen_Master_H
                                                select tbl;

                                return new ObservableCollection<SRR_KK_M_Screen_Master_H>(linqQuery);
                            }
               }
            catch
               {
                      throw new Exception("Database Error");
               }
        }
    }
}

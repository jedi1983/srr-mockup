using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;
using SRR_Devolopment.BaseLib;

namespace SRR_Devolopment.Services
{
    public class DataAccessServices : IDataServices

    {

        //ctor
        
        public ObservableCollection<USP_CG_KP_M_UserProfile_H_Find_Result> GetLoginID(String UserName,String Password)
        {
            String dataHashed = BaseLib.Class.HashingClass.getHash(Password);
            try
            {
                using (srr_devEntities context = new srr_devEntities())
                {
                    IList<USP_CG_KP_M_UserProfile_H_Find_Result> data = context.USP_CG_KP_M_UserProfile_H_Find(UserName, dataHashed).ToList();
                    return new ObservableCollection<USP_CG_KP_M_UserProfile_H_Find_Result>(data);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;

namespace SRR_Devolopment.Services
{
    class RevenueDataServices : IRevenueDataServices
    {

        public Collection<USP_CGL_KP_R_Revenue_H_Find_Result> getRevenueTransaction(int legalEntityID,int periodID)
        {
            try
            {
                using (srr_devEntities xData = new srr_devEntities())
                {
                    var dataBack = xData.USP_CGL_KP_R_Revenue_H_Find(legalEntityID, periodID).ToList();
                    return new Collection<USP_CGL_KP_R_Revenue_H_Find_Result>(dataBack);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
           
        }

        public ObservableCollection<CGL_KP_M_Revenue_Type_H> getRevenueType()
        {
            try
            {
                using (srr_devEntities xData = new srr_devEntities())
                {
                    var linqToSQL = from tbl in xData.CGL_KP_M_Revenue_Type_H
                                    where tbl.Is_Deleted == false
                                    select tbl;

                    return new ObservableCollection<CGL_KP_M_Revenue_Type_H>(linqToSQL);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
           
        }

        public Collection<USP_CGL_KP_T_Generate_Transaction_Number_Result> getTransactionNumber(string transactionCode, DateTime timeOfDay)
        {
            try
            {
                using (srr_devEntities xData = new srr_devEntities())
                {
                    var dataBack = xData.USP_CGL_KP_T_Generate_Transaction_Number(transactionCode, timeOfDay).ToList();

                    return new Collection<USP_CGL_KP_T_Generate_Transaction_Number_Result>(dataBack);

                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
            
        }

        public Collection<CGL_KP_M_Period_H> getPeriod()
        {
            try
            {
                using (srr_devEntities xData = new srr_devEntities())
                {
                    var dataBack = (from tbl in xData.CGL_KP_M_Period_H
                                    join xx in xData.CGL_KP_M_Period_Status_H on tbl.Period_Status_Id equals xx.Period_Status_Id
                                    where tbl.Is_Deleted == false && (xx.Period_Status_Desc.Contains("Active") || xx.Period_Status_Desc.Contains("Reopen"))
                                    select tbl).ToList();
                    return new Collection<CGL_KP_M_Period_H>(dataBack);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
        }

        public Collection<CGL_KP_M_Member_H> getMember()
        {
            try
            {
                using (srr_devEntities xData = new srr_devEntities())
                {
                    var linq = (from tbl in xData.CGL_KP_M_Member_H where tbl.Is_Deleted == false && tbl.Status_Id == 2 select tbl).ToList();
                    return new Collection<CGL_KP_M_Member_H>(linq);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
          
        }
    }
}

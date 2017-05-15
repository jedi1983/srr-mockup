using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;

namespace SRR_Devolopment.Services
{
    class ProcessDataService : IProcessDataService
    {
        public bool generateIuran(DateTime nowData,ref string message)
        {
            bool ret = false;
            try
            {
                using (srr_devEntities asData = new srr_devEntities())
                {

                    bool refStatus = false;
                    string refMessage = string.Empty;
                    System.Data.Objects.ObjectParameter pMessage = new System.Data.Objects.ObjectParameter("Message", refMessage);
                    System.Data.Objects.ObjectParameter pStatus = new System.Data.Objects.ObjectParameter("Success", refStatus);
                    asData.USP_CGL_KP_R_Generate_Loan_Payment(nowData, pStatus, pMessage);
                    if ((bool)pStatus.Value != true)
                    {
                        ret = false;
                        message = pMessage.Value.ToString();
                        return ret;
                    }
                    else
                    {
                        asData.USP_CGL_KP_R_Generate_Simpanan_Wajib(nowData, pStatus, pMessage);
                        ret = (bool)pStatus.Value;
                        message = pMessage.Value.ToString();
                    }
                    return ret;
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
        }

        public bool balanceCalculation(DateTime nowData,ref string message)
        {
            bool ret = false;
            try 
            {
                using (srr_devEntities asData = new srr_devEntities())
                {

                    bool refStatus = false;
                    string refMessage = string.Empty;
                    System.Data.Objects.ObjectParameter pMessage = new System.Data.Objects.ObjectParameter("Message", refMessage);
                    System.Data.Objects.ObjectParameter pStatus = new System.Data.Objects.ObjectParameter("Success", refStatus);
                    asData.USP_CGL_KP_R_Balance_Calculation(pStatus, pMessage);
                    ret = (bool)pStatus.Value;
                    message = pMessage.Value.ToString();
                    return ret;
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

    }
}

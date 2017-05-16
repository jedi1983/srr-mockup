using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;

namespace SRR_Devolopment.Services
{
    public class PeriodDataService : IPeriodDataService
    {
        public ICollection<USP_CGL_KP_M_Period_Status_Result> GetDataGridData()
        {
            try
            {
                using(srr_devEntities dataX = new srr_devEntities())
                {
                    ICollection<USP_CGL_KP_M_Period_Status_Result> dataRet = dataX.USP_CGL_KP_M_Period_Status().ToList();
                    return dataRet;
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
        }

        public bool newPeriod(DateTime getData, ref string message,string user)
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
                    asData.USP_CGL_KP_M_New_Period(getData, user, pStatus, pMessage);
                    //ret = (bool)pStatus.Value;
                    if ((int)pStatus.Value == 1)
                        ret = true;
                    else
                        ret = false;
                    message = pMessage.Value.ToString();
                    return ret;
                }

            }


            catch
            {
                throw new Exception("Database Error");
            }
        }

        public bool closePeriod(DateTime getData, ref string message, string user)
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
                    asData.USP_CGL_KP_M_Close_Period(getData, user, pStatus, pMessage);
                    //ret = (bool)pStatus.Value;
                    if ((int)pStatus.Value == 1)
                        ret = true;
                    else
                        ret = false;
                    message = pMessage.Value.ToString();
                    return ret;
                }

            }


            catch
            {
                throw new Exception("Database Error");
            }
        }

        public bool reOpenPeriod(DateTime getData, ref string message, string user)
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
                    asData.USP_CGL_KP_M_Reopen_Period(getData, user, pStatus, pMessage);
                    //ret = (bool)pStatus.Value;
                    if ((int)pStatus.Value == 1)
                        ret = true;
                    else
                        ret = false;
                    message = pMessage.Value.ToString();
                    return ret;
                }

            }


            catch
            {
                throw new Exception("Database Error");
            }
        }

    }
}

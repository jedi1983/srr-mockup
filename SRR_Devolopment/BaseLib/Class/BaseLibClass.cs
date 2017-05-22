using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SRR_Devolopment.BaseLib;
using System.Windows.Media;
using System.Windows.Shapes;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;

namespace SRR_Devolopment.BaseLib.Class
{
    static public class BaseLibClass
    {
        /// <summary>
        /// Checking Whether A Date Is In Period Open Or Re-Open Period
        /// </summary>
        /// <param name="dateIn">Date Time Of the Transaction Date</param>
        /// <param name="getPeriod">Collection Of Period </param>
        /// <returns></returns>
        public static bool getDateIsInPeriod(DateTime dateIn,Collection<CGL_KP_M_Period_H> getPeriod)
        {
            bool ret = false;
            DateTime startDateOpen;
            DateTime closeDateOpen;
            DateTime startDateReopen;
            DateTime closeDateReopen;

            using(srr_devEntities dataAdapter = new srr_devEntities())
            {
                var dataBack = (from tbl in getPeriod
                                join xx in dataAdapter.CGL_KP_M_Period_Status_H on tbl.Period_Status_Id equals xx.Period_Status_Id
                                where tbl.Is_Deleted == false && (xx.Period_Status_Desc.Contains("Active"))
                                select tbl).ToList();

                startDateOpen = dataBack.FirstOrDefault().Period_Start_Date;
                closeDateOpen = dataBack.FirstOrDefault().Period_End_Date;

                var dataBackReopen = (from tbl in getPeriod
                                join xx in dataAdapter.CGL_KP_M_Period_Status_H on tbl.Period_Status_Id equals xx.Period_Status_Id
                                where tbl.Is_Deleted == false && (xx.Period_Status_Desc.Contains("Reopen"))
                                select tbl).ToList();

                if(dataBackReopen.Count() > 0)
                {
                    
                    startDateReopen = dataBackReopen.FirstOrDefault().Period_Start_Date;
                    closeDateReopen = dataBackReopen.FirstOrDefault().Period_End_Date;
                }
                else
                {
                    startDateReopen = startDateOpen;
                    closeDateReopen = closeDateOpen;
                }

            }

            if ((dateIn.Date >= startDateOpen.Date && dateIn.Date <= closeDateOpen) || (dateIn.Date >= startDateReopen.Date && dateIn.Date <= closeDateReopen.Date))
                ret = true;
            else
                ret = false;
  
            return ret;
        }
    }
}

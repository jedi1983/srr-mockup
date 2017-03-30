using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;

namespace SRR_Devolopment.Services
{
   public interface IRevenueDataServices
   {
       ObservableCollection<CGL_KP_M_Revenue_Type_H> getRevenueType();

       Collection<USP_CGL_KP_T_Generate_Transaction_Number_Result> getTransactionNumber(string transactionCode,DateTime timeOfDay);

       Collection<USP_CGL_KP_R_Revenue_H_Find_Result> getRevenueTransaction(int legalEntityID,int periodID);

       Collection<CGL_KP_M_Period_H> getPeriod();

       Collection<CGL_KP_M_Member_H> getMember();

   }
}

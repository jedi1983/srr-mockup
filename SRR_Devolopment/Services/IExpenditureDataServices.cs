using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;

namespace SRR_Devolopment.Services
{
   public interface IExpenditureDataServices
    {

        Collection<CGL_KP_M_Period_H> getPeriod();

        ObservableCollection<CGL_KP_M_Expenditure_Type_H> getExpenditureType();

        Collection<USP_CGL_KP_T_Generate_Transaction_Number_Result> getTransactionNumber(string transactionCode, DateTime timeOfDay);

        Collection<CGL_KP_M_Member_H> getMember();

        Collection<USP_CGL_KP_R_Expenditure_H_Find_Result> getExpenditureTransaction(int legalEntityID, int periodID);

        Collection<USP_CGL_KP_S_Setting_H_Get_Result> getLoanSetting();

        Collection<USP_CGL_KP_R_Member_Loan_H_Find_Result> getMemberLoan(int memberID);

        bool saveDataToExpenditure(DateTime expenditureDate, int expenditureType, decimal expenditureAmount, int memberId, string userID, ref string expenditureNo,decimal interestRate,int termOfLoan);

        bool editDataToExpenditure(int transactionID,DateTime expenditureDate, int expenditureType, decimal expenditureAmount, int memberId, string userID, decimal interestRate, int termOfLoan,int loanData);

        bool deleteDataToExpenditure(int transactionID,string userID);

    }
}

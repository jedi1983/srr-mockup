using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;
using System.Transactions;

namespace SRR_Devolopment.Services
{
    class ExpenditureDataServices : IExpenditureDataServices
    {
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

        public ObservableCollection<CGL_KP_M_Expenditure_Type_H> getExpenditureType()
        {
            try
            {
                using (srr_devEntities xData = new srr_devEntities())
                {
                    var linqToSQL = from tbl in xData.CGL_KP_M_Expenditure_Type_H
                                    where tbl.Is_Deleted == false
                                    select tbl;

                    return new ObservableCollection<CGL_KP_M_Expenditure_Type_H>(linqToSQL);
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

        public Collection<CGL_KP_M_Member_H> getMember()
        {
            try
            {
                using (srr_devEntities xData = new srr_devEntities())
                {
                    xData.Configuration.LazyLoadingEnabled = false;
                    var linq = (from tbl in xData.CGL_KP_M_Member_H where tbl.Is_Deleted == false && tbl.Status_Id == 2 select tbl).ToList();
                    return new Collection<CGL_KP_M_Member_H>(linq);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }

        }

        public Collection<USP_CGL_KP_R_Expenditure_H_Find_Result> getExpenditureTransaction(int legalEntityID, int periodID)
        {
            try
            {
                using (srr_devEntities xData = new srr_devEntities())
                {
                    var dataBack = xData.USP_CGL_KP_R_Expenditure_H_Find(legalEntityID, periodID).ToList();
                    return new Collection<USP_CGL_KP_R_Expenditure_H_Find_Result>(dataBack);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }

        }

        public Collection<USP_CGL_KP_S_Setting_H_Get_Result> getLoanSetting()
        {
            try
            {
                using (srr_devEntities xData = new srr_devEntities())
                {
                    var dataBack = xData.USP_CGL_KP_S_Setting_H_Get().ToList();
                    return new Collection<USP_CGL_KP_S_Setting_H_Get_Result>(dataBack);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
        }

        public Collection<USP_CGL_KP_R_Member_Loan_H_Find_Result> getMemberLoan(int memberID)
        {

            try
            {
                using (srr_devEntities x = new srr_devEntities())
                {

                    var linqToList = x.USP_CGL_KP_R_Member_Loan_H_Find(memberID).ToList();
                    return new Collection<USP_CGL_KP_R_Member_Loan_H_Find_Result>(linqToList);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
        }

        public bool saveDataToExpenditure(DateTime expenditureDate, int expenditureType, decimal expenditureAmount, int memberId, string userID, ref string expenditureNo, decimal interestRate, int termOfLoan)
        {
            bool ret = false;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (srr_devEntities insertX = new srr_devEntities())
                    {
                        CGL_KP_M_Expenditure_Type_H typExp = (from tbl in insertX.CGL_KP_M_Expenditure_Type_H where tbl.Is_Deleted == false && tbl.Expenditure_Type_Id == expenditureType select tbl).FirstOrDefault();
                        CGL_KP_R_Expenditure_H dataInsert = new CGL_KP_R_Expenditure_H();
                        dataInsert.Expenditure_Date = expenditureDate;
                        dataInsert.Expenditure_Type_Id = expenditureType;
                        if (memberId != 0)
                            dataInsert.Member_Id = memberId;
                        else
                            dataInsert.Member_Id = null;
                        dataInsert.Expenditure_Amount = expenditureAmount;
                        //dataInsert.Description = Description;
                        if (typExp.Need_Approval == true)
                        {
                            dataInsert.Is_Approved = false;
                            dataInsert.Approved_By = null;
                            dataInsert.Approved_Date = null;
                        }
                        else
                        {
                            dataInsert.Is_Approved = true;
                            dataInsert.Approved_By = "System Generated"; ;
                            dataInsert.Approved_Date = DateTime.Now;
                        }
                        dataInsert.Is_Deleted = false;
                        dataInsert.Created_By = userID;
                        dataInsert.Created_Date = DateTime.Now;
                        dataInsert.Expenditure_No = insertX.USP_CGL_KP_T_Generate_Transaction_Number("Expenditure", DateTime.Now).FirstOrDefault().ReturnBack;
                        //insertX.CGL_KP_R_Expenditure_H.Add(dataInsert);

                        if (typExp.Expenditure_Type_Description.ToString().Contains("Loan"))
                        {
                            CGL_KP_R_Member_Loan_H loanInsert = new CGL_KP_R_Member_Loan_H();
                            loanInsert.Interest_Rate = interestRate;
                            loanInsert.Loan_Amount = expenditureAmount;
                            loanInsert.Term_of_Loan = termOfLoan;
                            loanInsert.Member_Id = memberId;
                            loanInsert.Year = expenditureDate.Year;
                            loanInsert.Month = expenditureDate.Month;
                            loanInsert.Interest_Amount = expenditureAmount * interestRate;
                            loanInsert.Remaining_Loan_Amount = expenditureAmount;
                            loanInsert.Remaining_Interest_Amount = loanInsert.Interest_Amount;
                            loanInsert.Remaining_Term_of_Loan = termOfLoan;
                            loanInsert.Is_Deleted = false;
                            loanInsert.Created_By = userID;
                            loanInsert.Created_Date = DateTime.Now;
                            insertX.CGL_KP_R_Member_Loan_H.Add(loanInsert);
                            insertX.SaveChanges();
                            dataInsert.Member_Loan_Id = loanInsert.Member_Loan_Id;
                        }
                        insertX.CGL_KP_R_Expenditure_H.Add(dataInsert);
                        insertX.SaveChanges();
                        expenditureNo = dataInsert.Expenditure_No;
                        //sp will run here
                        ret = true;
                        insertX.SaveChanges();
                        
                    }
                    scope.Complete();
                    return ret;
                }

            }
            catch
            {
                throw new Exception("Database Error");
            }
        }

        public bool editDataToExpenditure(int transactionID, DateTime expenditureDate, int expenditureType, decimal expenditureAmount, int memberId, string userID, decimal interestRate, int termOfLoan, int loanData)
        {
            bool ret = false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (srr_devEntities x = new srr_devEntities())
                    {
                        CGL_KP_M_Expenditure_Type_H typExp = (from tbl in x.CGL_KP_M_Expenditure_Type_H where tbl.Is_Deleted == false && tbl.Expenditure_Type_Id == expenditureType select tbl).FirstOrDefault();
                        CGL_KP_R_Expenditure_H dataInsert = x.CGL_KP_R_Expenditure_H.FirstOrDefault(xx => xx.Expenditure_Id == transactionID);
                        dataInsert.Expenditure_Date = expenditureDate;
                        dataInsert.Expenditure_Type_Id = expenditureType;
                        if (memberId != 0)
                            dataInsert.Member_Id = memberId;
                        else
                            dataInsert.Member_Id = null;
                        dataInsert.Expenditure_Amount = expenditureAmount;
                        //dataInsert.Description = Description;
                        if (typExp.Need_Approval == true)
                        {
                            dataInsert.Is_Approved = false;
                            dataInsert.Approved_By = null;
                            dataInsert.Approved_Date = null;
                        }
                        else
                        {
                            dataInsert.Is_Approved = true;
                            dataInsert.Approved_By = "System Generated"; ;
                            dataInsert.Approved_Date = DateTime.Now;
                        }
                        dataInsert.Is_Deleted = false;
                        dataInsert.Modified_By = userID;
                        dataInsert.Modified_Date = DateTime.Now;
                        

                        if (typExp.Expenditure_Type_Description.ToString().Contains("Loan") && loanData > 0)
                        {
                            CGL_KP_R_Member_Loan_H loanInsert = x.CGL_KP_R_Member_Loan_H.FirstOrDefault(xx => xx.Member_Loan_Id == loanData);

                            loanInsert.Interest_Rate = interestRate/100;
                            loanInsert.Loan_Amount = expenditureAmount;
                            loanInsert.Term_of_Loan = termOfLoan;
                            loanInsert.Member_Id = memberId;
                            loanInsert.Year = expenditureDate.Year;
                            loanInsert.Month = expenditureDate.Month;
                            loanInsert.Interest_Amount = expenditureAmount * loanInsert.Interest_Rate;
                            loanInsert.Remaining_Loan_Amount = expenditureAmount;
                            loanInsert.Remaining_Interest_Amount = loanInsert.Interest_Amount;
                            loanInsert.Remaining_Term_of_Loan = termOfLoan;
                            loanInsert.Is_Deleted = false;
                            loanInsert.Modified_By = userID;
                            loanInsert.Modified_Date = DateTime.Now;
                           
                        }
                        x.SaveChanges();
                    }
                   
                    ret = true;
                    scope.Complete();
                    return ret;
                }
            }
            catch 
            {
                throw new Exception("Database Error");
            }
        }

        public bool deleteDataToExpenditure(int transactionID,string userID)
        {
            bool ret = false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (srr_devEntities deleteX = new srr_devEntities())
                    {
                        CGL_KP_R_Expenditure_H data = deleteX.CGL_KP_R_Expenditure_H.FirstOrDefault(x => x.Expenditure_Id == transactionID);
                        if (data.Member_Loan_Id > 0)
                        {
                            CGL_KP_R_Member_Loan_H loanData = deleteX.CGL_KP_R_Member_Loan_H.FirstOrDefault(x => x.Member_Loan_Id == data.Member_Loan_Id);
                            loanData.Is_Deleted = true;
                            loanData.Deleted_By = userID;
                            loanData.Deleted_Date = DateTime.Now;
                        }
                        data.Is_Deleted = true;
                        data.Deleted_By = userID;
                        data.Deleted_Date = DateTime.Now;
                        deleteX.SaveChanges();
                        //deleting Journal If Exist
                        deleteX.USP_CGL_KP_R_Journal_H_To_Find_Delete("Expenditure",(int)data.Expenditure_Id,(string)userID);
                    }
                    ret = true;
                    scope.Complete();
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

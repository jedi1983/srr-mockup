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

        public bool saveDataToRevenue(DateTime revenueDate, int revenueType, decimal revenueAmount, int memberId,string userID,ref string revenueNo,int memberLoanID)
        {
            bool ret = false;

            try
            {

                using(srr_devEntities insertX = new srr_devEntities())
                {
                    CGL_KP_M_Revenue_Type_H typeRev = (from tbl in insertX.CGL_KP_M_Revenue_Type_H where tbl.Is_Deleted == false && tbl.Revenue_Type_Id == revenueType select tbl).FirstOrDefault(); 
                    CGL_KP_R_Revenue_H dataInsert = new CGL_KP_R_Revenue_H();
                    bool refStatus = false;
                    string refMessage = string.Empty;
                    System.Data.Objects.ObjectParameter pMessage = new System.Data.Objects.ObjectParameter("Message", refMessage);
                    System.Data.Objects.ObjectParameter pStatus = new System.Data.Objects.ObjectParameter("Success", refStatus);
                    dataInsert.Revenue_Date = revenueDate;
                    dataInsert.Revenue_Type_Id = revenueType;
                    if (memberId != 0)
                        dataInsert.Member_Id = memberId;
                    else
                        dataInsert.Member_Id = null;
                    dataInsert.Revenue_Amount = revenueAmount;
                    dataInsert.Member_Loan_Id = null;
                    dataInsert.Description = string.Empty;
                    if (typeRev.Need_Approval == false)
                    { dataInsert.Is_Approved = true; dataInsert.Approved_By = "System Generated"; dataInsert.Approved_Date = DateTime.Now; }
                    else
                    {
                        dataInsert.Is_Approved = false; dataInsert.Approved_By = string.Empty; dataInsert.Approved_Date = null;
                    }
                    if (memberLoanID != 0)
                        dataInsert.Member_Loan_Id = memberLoanID;
                    else
                        dataInsert.Member_Loan_Id = null;

                    dataInsert.Is_Deleted = false;
                    dataInsert.Created_By = userID;
                    dataInsert.Created_Date = DateTime.Now;
                    //Generate RV Number
                    //dataInsert.Revenue_No = insertX.USP_CGL_KP_T_Generate_Transaction_Number("Revenue", DateTime.Now).FirstOrDefault().ReturnBack;// Closed
                    dataInsert.Revenue_No = insertX.USP_CGL_KP_T_Generate_Transaction_Number("Revenue", revenueDate).FirstOrDefault().ReturnBack;
                    insertX.CGL_KP_R_Revenue_H.Add(dataInsert);
                    insertX.SaveChanges();
                    revenueNo = dataInsert.Revenue_No;


                    if (typeRev.Revenue_Type_Description.ToString().Contains("Repayment"))
                        insertX.USP_CGL_KP_R_Generate_Loan_Full_Payment(dataInsert.Member_Loan_Id, dataInsert.Revenue_Date,pStatus, pMessage);
                    else
                        insertX.USP_CGL_KP_R_Generate_Journal("REVENUE", dataInsert.Revenue_Id, pStatus, pMessage);


                    ret = true;
                return ret;
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
        }

        public bool editDataToRevenue(int TransactionID, DateTime revenueDate, int revenueType, decimal revenueAmount, int memberId, string userID, string revenueNo,int memberLoanID)
        {
            bool ret = false;
            try
            {
                using (srr_devEntities x = new srr_devEntities())
                {
                    bool refStatus = false;
                    string refMessage = string.Empty;
                    System.Data.Objects.ObjectParameter pMessage = new System.Data.Objects.ObjectParameter("Message", refMessage);
                    System.Data.Objects.ObjectParameter pStatus = new System.Data.Objects.ObjectParameter("Success", refStatus);
                    CGL_KP_M_Revenue_Type_H typeRev = (from tbl in x.CGL_KP_M_Revenue_Type_H where tbl.Is_Deleted == false && tbl.Revenue_Type_Id == revenueType select tbl).FirstOrDefault(); 
                    CGL_KP_R_Revenue_H insertData = x.CGL_KP_R_Revenue_H.FirstOrDefault(y => y.Revenue_Id == TransactionID);
                    insertData.Revenue_Date = revenueDate;
                    insertData.Revenue_Type_Id = revenueType;
                    if (memberId != 0)
                        insertData.Member_Id = memberId;
                    else
                        insertData.Member_Id = null;
                    insertData.Revenue_Amount = revenueAmount;
                    insertData.Member_Loan_Id = null;
                    insertData.Description = string.Empty;
                    if (typeRev.Need_Approval == false)
                    { insertData.Is_Approved = true; insertData.Approved_By = "System Generated"; insertData.Approved_Date = DateTime.Now; }
                    else
                    {
                        insertData.Is_Approved = false; insertData.Approved_By = string.Empty; insertData.Approved_Date = null;
                    }
                    if (memberLoanID != 0)
                        insertData.Member_Loan_Id = memberLoanID;
                    else
                        insertData.Member_Loan_Id = null;
                    insertData.Is_Deleted = false;
                    insertData.Modified_By = userID;
                    insertData.Modified_Date = DateTime.Now;
                    x.SaveChanges();

                    x.USP_CGL_KP_R_Generate_Journal("REVENUE", insertData.Revenue_Id, pStatus, pMessage);//Generate Journal

                    ret = true;
                    return ret;
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

        public bool deleteDataToRevenue(int transactionID,string userID)
        {
            bool ret = false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (srr_devEntities deleteX = new srr_devEntities())
                    {
                        CGL_KP_R_Revenue_H data = deleteX.CGL_KP_R_Revenue_H.FirstOrDefault(x => x.Revenue_Id == transactionID);
                        data.Is_Deleted = true;
                        data.Deleted_By = userID;
                        data.Deleted_Date = DateTime.Now;
                        deleteX.SaveChanges();
                        //deleting Journal If Exist
                        deleteX.USP_CGL_KP_R_Journal_H_To_Find_Delete("Revenue", (int)data.Revenue_Id, (string)userID);
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

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
    public class ApprovalDataService : IApprovalDataService
    {
        public Collection<USP_CGL_KP_R_Expenditure_H_UnApproved_Yet_Result> getUnApprovedTransaction()
        {
            try
            {
                using (srr_devEntities xData = new srr_devEntities())
                {
                    var dataBack = xData.USP_CGL_KP_R_Expenditure_H_UnApproved_Yet().ToList();
                    return new Collection<USP_CGL_KP_R_Expenditure_H_UnApproved_Yet_Result>(dataBack);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
        }

        public bool saveTransaction(Collection<USP_CGL_KP_R_Expenditure_H_UnApproved_Yet_Result> dataUnApproved, string userID)
        {
            bool ret = false;
            try 
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (srr_devEntities dataX = new srr_devEntities())
                    {


                        var queryLinq = from tbl in dataUnApproved where tbl.Flag == true select tbl;

                        foreach (USP_CGL_KP_R_Expenditure_H_UnApproved_Yet_Result xData in queryLinq)
                        {
                            int _id = xData.Expenditure_Id;
                            CGL_KP_R_Expenditure_H dataMod = dataX.CGL_KP_R_Expenditure_H.FirstOrDefault(x=> x.Expenditure_Id == _id);
                            dataMod.Is_Approved = true;
                            dataMod.Approved_By = userID;
                            dataMod.Approved_Date = DateTime.Now;
                            dataMod.Modified_By = userID;
                            dataMod.Modified_Date = DateTime.Now;
                            
                            dataX.SaveChanges();

                            //SP To Create Journal


                        }

                        
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

        public bool rejectTransaction(Collection<USP_CGL_KP_R_Expenditure_H_UnApproved_Yet_Result> dataUnApproved, string userID)
        {
            try
            {
                bool ret = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    using (srr_devEntities dataX = new srr_devEntities())
                    {


                        var queryLinq = from tbl in dataUnApproved where tbl.Flag == true select tbl;

                        foreach (USP_CGL_KP_R_Expenditure_H_UnApproved_Yet_Result xData in queryLinq)
                        {
                            int _id = (int)xData.Expenditure_Id;
                            CGL_KP_R_Expenditure_H dataMod = dataX.CGL_KP_R_Expenditure_H.FirstOrDefault(x => x.Expenditure_Id == _id);
                            dataMod.Is_Approved = false;
                            dataMod.Is_Deleted = true;
                            dataMod.Deleted_By = userID;
                            dataMod.Deleted_Date = DateTime.Now;
                            
                            //delete the Loan Member data

                            int _loanID = (int)xData.Member_Loan_Id;

                            CGL_KP_R_Member_Loan_H dataLoanMod = dataX.CGL_KP_R_Member_Loan_H.FirstOrDefault(x => x.Member_Loan_Id == _loanID);

                            dataLoanMod.Is_Deleted = true;
                            dataLoanMod.Deleted_By = userID;
                            dataLoanMod.Deleted_Date = DateTime.Now;
                            dataX.SaveChanges();

                        }


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

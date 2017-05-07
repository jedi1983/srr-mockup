using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;

namespace SRR_Devolopment.Services
{
    public interface IApprovalDataService
    {
        Collection<USP_CGL_KP_R_Expenditure_H_UnApproved_Yet_Result> getUnApprovedTransaction();

        bool saveTransaction(Collection<USP_CGL_KP_R_Expenditure_H_UnApproved_Yet_Result> dataUnApproved, string userID);

        bool rejectTransaction(Collection<USP_CGL_KP_R_Expenditure_H_UnApproved_Yet_Result> dataUnApproved, string userID);
    }
}

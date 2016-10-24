using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;

namespace SRR_Devolopment.Services
{
    /// <summary>
    /// Interface For the Data services
    /// </summary>
    public interface IDataServices
    {   
        /// <summary>
        /// To Get The Login ID
        /// </summary>
        /// <param name="UserName">UserName </param>
        /// <param name="Password">Password</param>
        /// <returns>Observable Collections of user</returns>
        //ObservableCollection<SRR_M_User_Login_H> GetLoginID(String UserName, String Password);
        ObservableCollection<USP_CG_KP_M_UserProfile_H_Find_Result> GetLoginID(String UserName, String Password);
        bool saveLogin(string UserName, String Name, String Password);
       
        ObservableCollection<USP_CG_KP_M_AccessRights_H_Find_Result> GetAccessRights(int GroupID);

        ObservableCollection<USP_CG_KP_M_FormMaster_H_Find_R_Result> GetFormMasterData(int GroupID);

        ObservableCollection<USP_CG_KP_M_Form_Type_H_Find_Result> GetFormType(int GroupID);

        int GetFormTypeId(String FormType);

        
    }

}

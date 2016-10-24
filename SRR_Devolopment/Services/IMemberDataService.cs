using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;

namespace SRR_Devolopment.Services
{
    public interface IMemberDataService
    {
        ObservableCollection<CGL_KP_M_Legal_Entity_H> getLegalEntity();
        ObservableCollection<USP_CGL_KP_M_Member_H_Find_Result> getMember(int LegalEntityID, string Name);
        ObservableCollection<BaseLib.Class.CGL_KP_M_Gender_H> getGenderType();
        ObservableCollection<CGL_KP_M_Status_H> getStatus();
        bool memberSaveData(CGL_KP_M_Member_H dataInsert, string userID);   

        bool memberEditedData(CGL_KP_M_Member_H dataInsert, string userID);

    }
}

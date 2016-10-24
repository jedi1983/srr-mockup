using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections.ObjectModel;
using SRR_Devolopment.BaseLib;

namespace SRR_Devolopment.Services
{
    public class DataAccessServices : IDataServices

    {

        
        public int GetFormTypeId(String FormType)
        {
            int ret = 0;
            using (srr_devEntities x = new srr_devEntities())
            {
                var linq = from tbl in x.CGL_KP_M_Form_Type_H
                           where tbl.Is_Deleted == false && tbl.Form_Type_Name.Contains(FormType)
                           select tbl;
                ret = linq.FirstOrDefault().Form_Type_Id;
            }
            return ret;
        }
        
        public ObservableCollection<USP_CG_KP_M_UserProfile_H_Find_Result> GetLoginID(String UserName,String Password)
        {
            String dataHashed = BaseLib.Class.HashingClass.getHash(Password);
            try
            {
                using (srr_devEntities context = new srr_devEntities())
                {
                    IList<USP_CG_KP_M_UserProfile_H_Find_Result> data = context.USP_CG_KP_M_UserProfile_H_Find(UserName, dataHashed).ToList();
                    return new ObservableCollection<USP_CG_KP_M_UserProfile_H_Find_Result>(data);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
           
        }

        public ObservableCollection<USP_CG_KP_M_FormMaster_H_Find_R_Result> GetFormMasterData(int GroupID)
        {
            try
            {
                using (srr_devEntities context = new srr_devEntities())
                {
                    IList<USP_CG_KP_M_FormMaster_H_Find_R_Result> data = context.USP_CG_KP_M_FormMaster_H_Find_R(GroupID).ToList();
                    return new ObservableCollection<USP_CG_KP_M_FormMaster_H_Find_R_Result>(data);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
        }

        public ObservableCollection<USP_CG_KP_M_Form_Type_H_Find_Result> GetFormType(int GroupID)
        {
            try
            {
                using (srr_devEntities context = new srr_devEntities())
                {
                    IList<USP_CG_KP_M_Form_Type_H_Find_Result> data = context.USP_CG_KP_M_Form_Type_H_Find(GroupID).ToList();
                    return new ObservableCollection<USP_CG_KP_M_Form_Type_H_Find_Result>(data);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
        }

        public ObservableCollection<USP_CG_KP_M_AccessRights_H_Find_Result> GetAccessRights(int GroupID)
        {
            try
            {
                using (srr_devEntities context = new srr_devEntities())
                {
                    IList<USP_CG_KP_M_AccessRights_H_Find_Result> data = context.USP_CG_KP_M_AccessRights_H_Find(GroupID).ToList();
                    return new ObservableCollection<USP_CG_KP_M_AccessRights_H_Find_Result>(data);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
        }
        
        public bool saveLogin(string UserName, String Name, String Password)
        {   
        //

            try
            {

            }
            catch
            {

            }

        return true;
        }

      
    }
}

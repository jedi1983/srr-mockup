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
  
   class MemberDataService : IMemberDataService
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

        public ObservableCollection<CGL_KP_M_Legal_Entity_H> getLegalEntity()
        {
            try
            {
                using (srr_devEntities xData = new srr_devEntities())
                {
                    
                    var linqToSQL = from tbl in xData.CGL_KP_M_Legal_Entity_H
                                    where tbl.Is_Deleted == false
                                    select tbl;

                    return new ObservableCollection<CGL_KP_M_Legal_Entity_H>(linqToSQL);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
           
        }

        public ObservableCollection<USP_CGL_KP_M_Member_H_Find_Result> getMember(int LegalEntityID,string Name)
        {
            try
            {
                using (srr_devEntities xData = new srr_devEntities())
                {
                    IList<USP_CGL_KP_M_Member_H_Find_Result> linQData = xData.USP_CGL_KP_M_Member_H_Find(legalEntityID: LegalEntityID, name: Name).ToList();
                    return new ObservableCollection<USP_CGL_KP_M_Member_H_Find_Result>(linQData);

                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
          
        }
       public ObservableCollection<CGL_KP_M_Status_H> getStatus()
        {
            try
            {
                using (srr_devEntities x = new srr_devEntities())
                {
                    var linqToSQL = from tbl in x.CGL_KP_M_Status_H
                                    where tbl.Is_Deleted == false
                                    select tbl;

                    return new ObservableCollection<CGL_KP_M_Status_H>(linqToSQL);
                }
            }
            catch
            {
                throw new Exception("Database Error");
            }
        }

       /// <summary>
       /// Static
       /// </summary>
       /// <returns></returns>
       public ObservableCollection<BaseLib.Class.CGL_KP_M_Gender_H>getGenderType()
        {
            BaseLib.Class.CGL_KP_M_Gender_H data = new BaseLib.Class.CGL_KP_M_Gender_H();
            BaseLib.Class.CGL_KP_M_Gender_H data2 = new BaseLib.Class.CGL_KP_M_Gender_H();
            ObservableCollection<BaseLib.Class.CGL_KP_M_Gender_H> ret = new ObservableCollection<BaseLib.Class.CGL_KP_M_Gender_H>();
            data.Id = 1;
            data.GenderCode = "M";
            data.GenderName = "Male";
            ret.Add(data);
            data2.Id = 2;
            data2.GenderName = "Female";
            data2.GenderCode = "F";
            ret.Add(data2);
            return ret;
        }

       public bool memberEditedData(CGL_KP_M_Member_H dataInsert, string userID)
       {
           bool ret = false;
           try
           {
               using (srr_devEntities x = new srr_devEntities())
               {
                   CGL_KP_M_Member_H insertInto = x.CGL_KP_M_Member_H.FirstOrDefault(y => y.Member_Id == dataInsert.Member_Id);
                   insertInto.Employee_No = dataInsert.Employee_No;
                   insertInto.Name = dataInsert.Name;
                   insertInto.Legal_Entity_Id = dataInsert.Legal_Entity_Id;
                   insertInto.Status_Id = dataInsert.Status_Id;
                   insertInto.Birth_Date = dataInsert.Birth_Date;
                   insertInto.Join_Date = dataInsert.Join_Date;
                   insertInto.Gender = dataInsert.Gender;
                   insertInto.Address = dataInsert.Address;
                   insertInto.Is_Deleted = false;
                   insertInto.Modified_By = userID;
                   insertInto.Modified_Date = DateTime.Now;
                   x.SaveChanges();
                   ret = true;
                   return ret;
               }
           }
           catch
           {
               throw new Exception("Database Error");
           }
       }

       public bool memberSaveData(CGL_KP_M_Member_H dataInsert, string userID)
       {
           bool ret = false;
               try
               {
                    using(srr_devEntities x = new srr_devEntities())
                    {
                        bool refStatus = false;
                        string refMessage = string.Empty;
                        System.Data.Objects.ObjectParameter pMessage = new System.Data.Objects.ObjectParameter("Message", refMessage);
                        System.Data.Objects.ObjectParameter pStatus = new System.Data.Objects.ObjectParameter("Success", refStatus);
                        CGL_KP_M_Member_H insertInto = new CGL_KP_M_Member_H();
                        insertInto.Employee_No = dataInsert.Employee_No;
                        insertInto.Name = dataInsert.Name;
                        insertInto.Legal_Entity_Id = dataInsert.Legal_Entity_Id;
                        insertInto.Status_Id = dataInsert.Status_Id;
                        insertInto.Birth_Date = dataInsert.Birth_Date;
                        insertInto.Join_Date = dataInsert.Join_Date;
                        insertInto.Gender = dataInsert.Gender;
                        insertInto.Address = dataInsert.Address;
                        insertInto.Is_Deleted = false;
                        insertInto.Created_By = userID;
                        insertInto.Created_Date = DateTime.Now;
                        x.CGL_KP_M_Member_H.Add(insertInto);
                        x.SaveChanges();
                        //run SP                        
                        x.USP_CGL_KP_R_Generate_Simpanan_Pokok(insertInto.Member_Id, insertInto.Join_Date, pStatus, pMessage);
                        ret = true;
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

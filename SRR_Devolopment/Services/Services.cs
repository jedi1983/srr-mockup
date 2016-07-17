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
    /// Interfaces For Accessing Data Model 
    /// </summary>
    /// Roland 2016
    public interface IDataServices
    {
        ObservableCollection<SRR_M_User_Login_H> GetLoginID(String UserName,String Password);
        bool saveLogin(string UserName, String Name, String Password);

    }

    class DataAccessServices : IDataServices

    {
       
       //ctor
        srr_devEntities DataContextService;
        public DataAccessServices()
        {
           DataContextService = new srr_devEntities();
        }

        ObservableCollection<SRR_M_User_Login_H> GetLoginID(String UserName,String Password)
        {
            ObservableCollection<SRR_M_User_Login_H> dataLinq;

            object linQtoDB = from tbl in DataContextService.SRR_M_User_Login_H where tbl.Name == UserName && tbl.Password == Password select tbl;



            dataLinq = new ObservableCollection<SRR_M_User_Login_H>(linQtoDB);         

        }
    }
}

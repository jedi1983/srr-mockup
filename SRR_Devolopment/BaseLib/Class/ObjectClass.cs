using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRR_Devolopment.Model;
using System.Collections;
using System.Collections.ObjectModel;

namespace SRR_Devolopment.BaseLib.Class
{
    /// <summary>
    /// Class Name in Relation to View Model Locator
    /// </summary>
   
    public class Grid_Data_Add_Remove
    {
        public int TabItemNo { get; set; }
        public string FormName { get; set;}
        
    }

    /// <summary>
    /// Gender Class for Container
    /// </summary>
    public class CGL_KP_M_Gender_H
    {
        public int Id{get; set;}
        public string GenderCode {get; set;}
        public string GenderName {get; set;}
    }

    /// <summary>
    /// Sealed Class Singleton for Setting Up Class That Would use Globally and only one instances may exist on the whole Application scope
    /// @Roland 2016
    /// </summary>
    public sealed class Singleton
    {
        private static Singleton instance = null;
        private static readonly object padlock = new object();

        Singleton()
        {
        }


        private ObservableCollection<USP_CG_KP_M_AccessRights_H_Find_Result> _accessRight;
        public ObservableCollection<USP_CG_KP_M_AccessRights_H_Find_Result> AccessRight
        {
            get
            {
                return _accessRight;
            }
            set
            {
                _accessRight = value;
            }
        }

        private string _tmpUserName;
        public string TmpUserName
        {
            get
            {
                return _tmpUserName;
            }
            set
            {
                _tmpUserName = value;
            }
        }
        /// <summary>
        /// Statis Constructor
        /// </summary>
        public static Singleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                    return instance;
                }
            }
        }
    }
}

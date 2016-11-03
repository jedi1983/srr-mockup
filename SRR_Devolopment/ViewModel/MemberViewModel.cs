using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SRR_Devolopment.Model;
using SRR_Devolopment.Services;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using SRR_Devolopment.Views;
using SRR_Devolopment.BaseLib.Class;
using GalaSoft.MvvmLight.Messaging;
using SRR_Devolopment.MessageInfrastructure;
using System.Drawing;


namespace SRR_Devolopment.ViewModel
{
    public class MemberViewModel : ViewModelBased
    {

        #region "Properties"

        public RelayCommand<DataGrid> SelectionGrid { get; set; }

        IMemberDataService _dataServices;

        /// <summary>
        /// member ID
        /// </summary>
        private int _memberID;
        public int MemberID
        {
            get
            {
                return _memberID;
            }
            set
            {
                _memberID = value;
                RaisePropertyChanged("MemberID");
            }
        }

        /// <summary>
        /// Properties for marking Modify
        /// </summary>
        private bool _dataMod;
        public bool DataMod
        {
            get
            {
                return _dataMod;
            }
            set
            {
                _dataMod = value;
                RaisePropertyChanged("DataMod");

            }
        }

        /// <summary>
        /// Join Date
        /// </summary>
        private DateTime _setJoinDate;
        public DateTime SetJoinDate
        {
            get
            {
                return _setJoinDate;
            }
            set
            {
                _setJoinDate = value;
                RaisePropertyChanged("SetJoinDate");
            }
        }

        /// <summary>
        /// Birth Date
        /// </summary>
        //private DateTime _setBirthDate;
        //public DateTime SetBirthDate
        //{
        //    get
        //    {
        //        return _setBirthDate;
        //    }
        //    set 
        //    {
        //        _setBirthDate = value;
        //        RaisePropertyChanged("SetBirthDate");
        //    }
        //}

        /// <summary>
        /// Get Status 
        /// </summary>
        private ObservableCollection<CGL_KP_M_Status_H> _getStatus;
        public ObservableCollection<CGL_KP_M_Status_H> GetStatus
        {
            get
            {
                _getStatus = _dataServices.getStatus();
                return _getStatus;
            }
            set
            {
                _getStatus = value;
                RaisePropertyChanged("GetStatus");
            }
        }

        /// <summary>
        /// Set Status
        /// </summary>
        private CGL_KP_M_Status_H _setStatus;
        public CGL_KP_M_Status_H SetStatus
        {
            get
            {
                return _setStatus;
            }
            set
            {
                _setStatus = value;
                RaisePropertyChanged("SetStatus");
            }

        }

        /// <summary>
        /// Get Gender
        /// </summary>
        //private ObservableCollection<BaseLib.Class.CGL_KP_M_Gender_H> _getGender;
        //public ObservableCollection<BaseLib.Class.CGL_KP_M_Gender_H> GetGender
        //{
        //    get
        //    {
        //        _getGender = _dataServices.getGenderType();
        //        return _getGender;
        //    }
        //    set
        //    {
        //        _getGender = value;
        //        RaisePropertyChanged("GetGender");
        //    }
        //}
        ///// <summary>
        ///// Set Gender
        ///// </summary>
        //private BaseLib.Class.CGL_KP_M_Gender_H _setGender;
        //public BaseLib.Class.CGL_KP_M_Gender_H SetGender
        //{
        //    get
        //    {
        //        return _setGender;
        //    }
        //    set
        //    {
        //        _setGender = value;
        //        RaisePropertyChanged("SetGender");
        //    }

        //}
        /// <summary>
        /// address
        /// </summary>
        //private string _address;
        //public string Address
        //{
        //    get
        //    {
        //        return _address;
        //    }
        //    set
        //    {
        //        _address = value;
        //        RaisePropertyChanged("Address");
        //    }
        //}

        /// <summary>
        /// Employee Name
        /// </summary>
        private string _employeeName;
        public string EmployeeName
        {
            get
            {
                return _employeeName;
            }
            set
            {
                _employeeName = value;
                RaisePropertyChanged("EmployeeName");
            }
        }

        /// <summary>
        /// Employee No
        /// </summary>
        private string _employeeNo;
        public string EmployeeNo
        {
            get
            {
                return _employeeNo;
            }
            set
            {
                _employeeNo = value;
                RaisePropertyChanged("EmployeeNo");
            }

        }

        private ObservableCollection<USP_CG_KP_M_AccessRights_H_Find_Result> _dataAccessLevel;
        public ObservableCollection<USP_CG_KP_M_AccessRights_H_Find_Result> DataAccessLevel
        {
            get
            {
                return _dataAccessLevel;
            }
            set
            {
                _dataAccessLevel = value;
                RaisePropertyChanged("DataAccessLevel");
            }
        }

        private USP_CGL_KP_M_Member_H_Find_Result _SelectedGridData;
        public USP_CGL_KP_M_Member_H_Find_Result SelectedGridData
        {
            get
            {
                return _SelectedGridData;
            }
            set
            {
                _SelectedGridData = value;
                RaisePropertyChanged("SelectedGridData");
            }
        }

        private string _findName;
        public string FindName
        {
            get
            {
                return _findName;
            }
            set
            {
                _findName = value;
                RaisePropertyChanged("FindName");
            }
        }

        private ObservableCollection<USP_CGL_KP_M_Member_H_Find_Result> _getMemberData;
        public ObservableCollection<USP_CGL_KP_M_Member_H_Find_Result> GetMemberData
        {
            get
            {
                return _getMemberData;
            }
            set
            {
                _getMemberData = value;
                RaisePropertyChanged("GetMemberData");
            }
        }

        private ObservableCollection<CGL_KP_M_Legal_Entity_H> _getLegalEntity;
        public ObservableCollection<CGL_KP_M_Legal_Entity_H> GetLegalEntity
        {
            get
            {
                _getLegalEntity = _dataServices.getLegalEntity();
                return _getLegalEntity;
            }
            set
            {
                _getLegalEntity = value;
                RaisePropertyChanged("GetLegalEntity");
            }
        }

       
        private CGL_KP_M_Legal_Entity_H _setLegalEntity;
        public CGL_KP_M_Legal_Entity_H SetLegalEntity
        {
            get
            {
                return _setLegalEntity;
            }

            set
            {
                _setLegalEntity = value;
                RaisePropertyChanged("SetLegalEntity");
            }
        }

        private CGL_KP_M_Legal_Entity_H _setLegalEntityFind;
        public CGL_KP_M_Legal_Entity_H SetLegalEntityFind
        {
            get
            {
                return _setLegalEntityFind;
            }

            set
            {
                _setLegalEntityFind = value;
                RaisePropertyChanged("SetLegalEntityFind");
            }
        }

      

        #endregion

        #region "Methods"

        /// <summary>
        /// Event To Command for Grid Selection
        /// </summary>
        /// <param name="x"></param>
        public void getGridSelection(DataGrid x)
        {
           if(SelectedGridData != null && (IsMod == true) )
           {
               showDataOnForm(SelectedGridData);
               EnabledModify = true;
           }
            
        }

        /// <summary>
        /// cancel button
        /// </summary>
        public override void cancelButton()
        {
            CleanObject();
            if(IsNew == true)
            {
                ObjFilterEnabled = true;
                ObjEnabled = false;
                EnabledNew = true;
            }
            else if(IsMod == true)
            {
                ObjFilterEnabled = true;
                ObjEnabled = false;
                EnabledNew = true;
            }
            else if(IsRead == true)
            {
                ObjFilterEnabled = true;
                ObjEnabled = false;
                EnabledNew = true;
            }
            base.cancelButton();
        }

        /// <summary>
        /// Modify Button
        /// </summary>
        public override void modifyButton()
        {
            
            EnabledNew = false;
            EnabledCancel = true;
            EnabledSave = true;
            base.modifyButton();
            ObjEnabled = true;
            ObjFilterEnabled = false;
            DataMod = true;
        }
        
        /// <summary>
        /// show data to the form
        /// </summary>
        /// <param name="SelectedGridData">Object Param</param>
        public void showDataOnForm(USP_CGL_KP_M_Member_H_Find_Result SelectedGridData)
        {
            if(SelectedGridData != null)
            {
                SetLegalEntity = GetLegalEntity.FirstOrDefault(x => x.Legal_Entity_Id == SelectedGridData.Legal_Entity_Id);
                EmployeeName = SelectedGridData.Name;
                EmployeeNo = SelectedGridData.Employee_No;
                //SetGender = GetGender.FirstOrDefault(x => x.GenderCode.Contains(SelectedGridData.Gender));
                //Address = SelectedGridData.Address;
                //SetBirthDate = (DateTime)SelectedGridData.Birth_Date;
                SetJoinDate = (DateTime)SelectedGridData.Join_Date;
                SetStatus = GetStatus.FirstOrDefault(x => x.Status_Id == SelectedGridData.Status_Id);
                MemberID = SelectedGridData.Member_Id;

            }
        }

        private string getReturnFilled()
        {
            string ret = string.Empty;

            if (SetLegalEntity == null)
            {
                ret = "Please Fill Up Legal Entity";
            }
            if (EmployeeName == string.Empty)
            {
                ret = "Please Fill Up Employee Name";
            }
            if (EmployeeNo == string.Empty)
            {
                ret = "Please Fill Up Employee No";
            }
            //if (Address == string.Empty)
            //{
            //    ret = "Please Fill Up Address";
            //}
            //if(SetGender == null)
            //{
            //    ret = "Please Fill Up Gender";
            //}
            if(SetStatus == null)
            {
                ret = "Please Fill Up Status";
            }
            return ret;
        }



        /// <summary>
        /// new button
        /// </summary>
        public override void newButton()
        {
            CleanObject();
            base.newButton();
            ObjEnabled = true;
            ObjFilterEnabled = false;
            EnabledSave = true;
            EnabledCancel = true;
        }

        /// <summary>
        /// save button
        /// </summary>
        public override void saveButton()
        {
            base.saveButton();
           
            if(MessageBox.Show("Are You Sure You Want To Save This Data?","Member Screen",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //modify
                if(DataMod == true)
                {
                    if (getReturnFilled() != string.Empty)
                    {
                        MessageBox.Show(getReturnFilled(), "Member Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    else
                    {
                        CGL_KP_M_Member_H dataInsert = new CGL_KP_M_Member_H();
                        dataInsert.Member_Id = MemberID;
                        dataInsert.Employee_No = EmployeeNo;
                        dataInsert.Name = EmployeeName;
                        //dataInsert.Address = Address;
                        //dataInsert.Birth_Date = SetBirthDate;
                        dataInsert.Join_Date = SetJoinDate;
                        dataInsert.Status_Id = SetStatus.Status_Id;
                        dataInsert.Legal_Entity_Id = SetLegalEntity.Legal_Entity_Id;
                        //dataInsert.Gender = SetGender.GenderCode;
                        if (_dataServices.memberEditedData(dataInsert, (string)BaseLib.Class.Singleton.Instance.TmpUserName) == true)
                        {
                            MessageBox.Show("Data Saved Successfully", "Member Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                            cancelButton();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Data Not Successfully Saved", "Member Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }
                }
                //new Data
                else
                {
                    
                   if(getReturnFilled() != string.Empty)
                   {
                       MessageBox.Show(getReturnFilled(), "Member Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                       return;
                   }
                   else
                   {
                       CGL_KP_M_Member_H dataInsert = new CGL_KP_M_Member_H();
                       dataInsert.Employee_No = EmployeeNo;
                       dataInsert.Name = EmployeeName;
                       //dataInsert.Birth_Date = SetBirthDate;
                       dataInsert.Join_Date = SetJoinDate;
                       dataInsert.Status_Id = SetStatus.Status_Id;
                       dataInsert.Legal_Entity_Id = SetLegalEntity.Legal_Entity_Id;
                       //dataInsert.Gender = SetGender.GenderCode;
                       //dataInsert.Address = Address;
                       if(_dataServices.memberSaveData(dataInsert,(string)BaseLib.Class.Singleton.Instance.TmpUserName)==true)
                       {
                           MessageBox.Show("Data Saved Successfully", "Member Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                           cancelButton();
                           return;
                       }
                       else
                       {
                           MessageBox.Show("Data Not Successfully Saved", "Member Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                           return;
                       }
                   }
                       
                   }
             }
        }
        

        /// <summary>
        /// Generate Button
        /// </summary>
        public override void generateButton()
        {
            base.generateButton();
            GetMemberData = null;
            if(SetLegalEntityFind == null)
            {
                MessageBox.Show("Please Set The Legal Entity", "Member Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                GetMemberData = _dataServices.getMember(SetLegalEntityFind.Legal_Entity_Id, FindName);
            }
        }

        /// <summary>
        /// Get First Screen Access
        /// </summary>
        private void getScreenAccess()
        {

            getFirstAccess(isModify: (bool)DataAccessLevel.FirstOrDefault().IsMod, isRead: (bool)DataAccessLevel.FirstOrDefault().IsRead, isWrite: (bool)DataAccessLevel.FirstOrDefault().IsWrite);
            //set Property
            IsMod = (bool)DataAccessLevel.FirstOrDefault().IsMod;
            IsNew = (bool)DataAccessLevel.FirstOrDefault().IsWrite;
            IsRead = (bool)DataAccessLevel.FirstOrDefault().IsRead;

        }

        private void Initalize()
        {
            
            //properties data Level
            _setLegalEntityFind = null;
            _getMemberData = null;
            _findName = string.Empty;
            _employeeName = string.Empty;
            _employeeNo = string.Empty;
            //_address = string.Empty;
            //_setGender = null;
            _setLegalEntity = null;
            _setStatus = null;
            ObjEnabled = false;
            ObjFilterEnabled = true;
            _dataMod = false;
            //_setBirthDate = DateTime.Now;
            _setJoinDate = DateTime.Now;
            _memberID = 0;
            //end here
            _dataAccessLevel = BaseLib.Class.Singleton.Instance.AccessRight;
            getScreenAccess();

        }

        private void CleanObject()
        {
            SetLegalEntity = null;
            EmployeeName = string.Empty;
            EmployeeNo = string.Empty;
            //Address = string.Empty;
            SetStatus = null;
            //SetGender = null;
            //SetBirthDate = DateTime.Now;
            SetJoinDate = DateTime.Now;
            FindName = string.Empty;
            SetLegalEntityFind = null;
            SelectedGridData = null;
            GetMemberData = null;
            EnabledNew = false;
            EnabledSave = false;
            EnabledModify = false;
            EnabledCancel = false;
            DataMod = false;
            MemberID = 0;
        }

        #endregion

        #region "Ctor"
         public MemberViewModel(IMemberDataService dataServices)
        {
            _dataServices = dataServices;
            SelectionGrid = new RelayCommand<DataGrid>(getGridSelection);
            //initializer
            Initalize();
        }
        #endregion

        #region "Grid Event"

        #endregion

    }
}

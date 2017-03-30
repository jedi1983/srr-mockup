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
    public class RevenueViewModel : ViewModelBased
    {

        #region "Properties"

        //interface to solid object
        IRevenueDataServices _dataServices;
        public RelayCommand<DataGrid> SelectionGrid { get; set; }

        /// <summary>
        /// Member Enabled Property
        /// </summary>
        private bool _isMemberEnabled;
        public bool IsMemberEnabled
        {
            get
            {
                return _isMemberEnabled;
            }
            set
            {
                _isMemberEnabled = value;
                RaisePropertyChanged("IsMemberEnabled");
            }
        }

        /// <summary>
        /// Member Data
        /// </summary>
        private Collection<CGL_KP_M_Member_H> _getMember;
        public  Collection<CGL_KP_M_Member_H> GetMember
        {
            get
            {
                _getMember = _dataServices.getMember();
                return _getMember;
            }
            set
            {
                _getMember = value;
                RaisePropertyChanged("GetMember");
            }
        }

        /// <summary>
        /// Member Setter
        /// </summary>
        private CGL_KP_M_Member_H _setMember;
        public CGL_KP_M_Member_H SetMember
        {
            get
            {
                return _setMember;
            }
            set
            {
                _setMember = value;
                RaisePropertyChanged("SetMember");
            }
        }

        /// <summary>
        /// Get Periods
        /// </summary>
        private Collection<CGL_KP_M_Period_H> _getPeriod;
        public Collection<CGL_KP_M_Period_H> GetPeriod
        {
            get
            {
                _getPeriod = _dataServices.getPeriod();
                return _getPeriod;
            }
            set
            {
                _getPeriod = value;
                RaisePropertyChanged("GetPeriod");
            }
        }

        /// <summary>
        /// Set Period
        /// </summary>
        private CGL_KP_M_Period_H _setPeriod;
        public CGL_KP_M_Period_H SetPeriod
        {
            get 
            {
                return _setPeriod;
            }
            set
            {
                _setPeriod = value;
                RaisePropertyChanged("SetPeriod");
            }
        }

        /// <summary>
        /// Get Revenue Data
        /// </summary>
        private Collection<USP_CGL_KP_R_Revenue_H_Find_Result> _getRevenueData;
        public Collection<USP_CGL_KP_R_Revenue_H_Find_Result> GetRevenueData
        {
            get
            {
                return _getRevenueData;
            }
            set
            {
                _getRevenueData = value;
                RaisePropertyChanged("GetRevenueData");
            }
        }


        private USP_CGL_KP_R_Revenue_H_Find_Result _SelectedGridData;
        public USP_CGL_KP_R_Revenue_H_Find_Result SelectedGridData
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
        /// Revenue Amount
        /// </summary>
        private decimal _revenueAmount;
        public decimal RevenueAmount
        {
            get
            {
                return _revenueAmount;
            }
            set
            {
                _revenueAmount = value;
                RaisePropertyChanged("RevenueAmount");
            }
        }

        /// <summary>
        /// Get Revenue Type
        /// </summary>
        private ObservableCollection<CGL_KP_M_Revenue_Type_H> _getRevenueType;
        public ObservableCollection<CGL_KP_M_Revenue_Type_H> GetRevenueType
        {
            get
            {
                _getRevenueType = _dataServices.getRevenueType();
                return _getRevenueType;
            }
            set
            {
                _getRevenueType = value;
                RaisePropertyChanged("GetRevenueType");
            }
        }

        /// <summary>
        /// Set Revenue Type
        /// </summary>

        private CGL_KP_M_Revenue_Type_H _setRevenueType;
        public CGL_KP_M_Revenue_Type_H SetRevenueType
        {
            get
            {
                return _setRevenueType;
            }
            set
            {
                if(value!=_setRevenueType)
                {
                    _setRevenueType = value;
                    if (_setRevenueType.Need_Member == true)
                        IsMemberEnabled = true;
                    else
                        IsMemberEnabled = false;
                }
                
                RaisePropertyChanged("SetRevenueType");
            }
        }

        /// <summary>
        /// Revenue No
        /// </summary>
        private string _revenueNo;
        public string RevenueNo
        {
            get
            {
                return _revenueNo;
            }
            set
            {
                _revenueNo = value;
                RaisePropertyChanged("RevenueNo");
            }

        }

        /// <summary>
        /// Revenue Date
        /// </summary>
        private DateTime _revenueDate;
        public DateTime RevenueDate
        {
            get
            {
                return _revenueDate;
            }
            set
            {
                _revenueDate = value;
                RaisePropertyChanged("RevenueDate");
            }
        }

        /// <summary>
        /// Data Access Level Set
        /// </summary>
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

        #endregion

        #region "Methods"

        /// <summary>
        /// Generate Button
        /// </summary>
        public override void generateButton()
        {
            base.generateButton();
            GetRevenueData = null;
            if (SetPeriod == null)
            {
                MessageBox.Show("Please Set The Period", "Revenue Transaction Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                GetRevenueData = _dataServices.getRevenueTransaction(0,SetPeriod.Period_Id);
            }
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
        /// cancel button
        /// </summary>
        public override void cancelButton()
        {
            CleanObject();
            if (IsNew == true)
            {
                ObjFilterEnabled = true;
                ObjEnabled = false;
                EnabledNew = true;
            }
            else if (IsMod == true)
            {
                ObjFilterEnabled = true;
                ObjEnabled = false;
                EnabledNew = true;
            }
            else if (IsRead == true)
            {
                ObjFilterEnabled = true;
                ObjEnabled = false;
                EnabledNew = true;
            }
            base.cancelButton();
        }

        /// <summary>
        /// Clean Object
        /// </summary>
        private void CleanObject()
        {
            SetRevenueType = null;
            RevenueNo = string.Empty;
            RevenueAmount = 0;
            RevenueDate = DateTime.Now;
            SetPeriod = null;
            SetPeriod = GetPeriod.FirstOrDefault();
            SelectedGridData = null;
            GetRevenueData = null;
            EnabledNew = false;
            EnabledSave = false;
            EnabledModify = false;
            EnabledCancel = false;
            DataMod = false;
            ObjEnabled = false;
            ObjFilterEnabled = true;
            SetMember = null;
            //MemberID = 0;
            IsMemberEnabled = false;
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
            _setRevenueType = null;
            _revenueAmount = 0;
            _revenueNo = string.Empty;
            ObjEnabled = false;
            ObjFilterEnabled = true;
            _dataMod = false;
            _revenueDate = DateTime.Now;
            //end here
            _dataAccessLevel = BaseLib.Class.Singleton.Instance.AccessRight;
            getScreenAccess();
            GetRevenueData = null;
            SetPeriod = GetPeriod.FirstOrDefault();
            ObjEnabled = false;
            ObjFilterEnabled = true;
            SetMember = null;
            IsMemberEnabled = false;

        }

        /// <summary>
        /// Event To Command for Grid Selection
        /// </summary>
        /// <param name="x"></param>
        public void getGridSelection(DataGrid x)
        {
            if (SelectedGridData != null && (IsMod == true))
            {
                showDataOnForm(SelectedGridData);
                EnabledModify = true;
            }

        }

        /// <summary>
        /// show data to the form
        /// </summary>
        /// <param name="SelectedGridData">Object Param</param>
        public void showDataOnForm(USP_CGL_KP_R_Revenue_H_Find_Result SelectedGridData)
        {
            if (SelectedGridData != null)
            {
                SetRevenueType = GetRevenueType.FirstOrDefault(x => x.Revenue_Type_Id == SelectedGridData.Revenue_Type_Id);
                RevenueNo = SelectedGridData.Revenue_No;
                RevenueAmount = SelectedGridData.Revenue_Amount;
                RevenueDate = SelectedGridData.Revenue_Date;
                if (SelectedGridData.Member_Id.GetValueOrDefault() != 0)
                {
                    SetMember = GetMember.FirstOrDefault(x => x.Member_Id == SelectedGridData.Member_Id);
                }
                else
                    SetMember = null;
            }
        }

        #endregion

        #region "CTOR"
        public RevenueViewModel(IRevenueDataServices dataServices)
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

﻿using System;
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
using System.Windows.Controls.Primitives;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows.Data;
using System.Windows.Input;
using DataGridFilterLibrary;

namespace SRR_Devolopment.ViewModel
{
    public class RevenueViewModel : ViewModelBased,IDisposable
    {

        #region "Properties"

        //data services Dependency Injection
        IRevenueDataServices _dataServices;
        public RelayCommand<DataGrid> SelectionGrid { get; set; }
        
        //On Focus Object Inherited by COntrol
        public RelayCommand<Control> LostFocus { get; set; }

        /// <summary>
        /// Collection of Control
        /// </summary>
        private Collection<Control> _fillControl = new Collection<Control>();
        public Collection<Control> FillControl
        {
            get
            {
                return _fillControl;
            }
            set
            {
                _fillControl = value;
                RaisePropertyChanged("FillControl");
            }
        }

        //disposable
        bool disposed = false;

        /// <summary>
        /// Object Enabled Transaction Type
        /// </summary>
        private bool _objEnabledTransactionType;
        public bool ObjEnabledTransactionType
        {
            get
            {
                return _objEnabledTransactionType;
            }
            set
            {
                _objEnabledTransactionType = value;
                RaisePropertyChanged("ObjEnabledTransactionType");
            }
        }

        /// <summary>
        /// Get Member Loan Data
        /// </summary>
        private Collection<USP_CGL_KP_R_Member_Loan_H_Find_Result> _getMemberLoadData;
        public Collection<USP_CGL_KP_R_Member_Loan_H_Find_Result> GetMemberLoadData
        {
            get
            {
                return _getMemberLoadData;
            }
            set
            {
                _getMemberLoadData = value;
                RaisePropertyChanged("GetMemberLoadData");
            }
        }

        /// <summary>
        /// Set Loan Data
        /// </summary>
        private USP_CGL_KP_R_Member_Loan_H_Find_Result _setLoanData;
        public USP_CGL_KP_R_Member_Loan_H_Find_Result SetLoanData
        {
            get
            {
                return _setLoanData;
            }
            set
            {
                _setLoanData = value;
                if (_setLoanData != null)
                    RevenueAmount = _setLoanData.Remaining_Loan_Amount;
                RaisePropertyChanged("SetLoanData");
            }
        }

        /// <summary>
        /// Amount Enabled
        /// </summary>
        private bool _isAmountEnabled;
        public bool IsAmountEnabled
        {
            get
            {
                return _isAmountEnabled;
            }
            set 
            {
                _isAmountEnabled = value;
                RaisePropertyChanged("IsAmountEnabled");
            }
        }
        
        /// <summary>
        /// Is Repayment
        /// </summary>
        private bool _isRepayment;
        public bool IsRepayment
        {
            get
            {
                return _isRepayment; 
            }
            set
            {
                _isRepayment = value;
                RaisePropertyChanged("IsRepayment");
                
            }
        }
        

        /// <summary>
        /// Get ID Transaction
        /// </summary>
        int _getTransactionID;
        public int GetTransactionID
        {
            get
            {
                return _getTransactionID;
            }
            set
            {
                _getTransactionID = value;
                RaisePropertyChanged("GetTransactionID");
            }
        }

        /// <summary>
        /// Get Edit Mode of TextBoxWithSearch
        /// </summary>
        private bool _getEditMode;
        public bool GetEditMode
        {
            get
            {
                return _getEditMode;
            }
            set
            {
                _getEditMode = value;
                RaisePropertyChanged("GetEditMode");
            }
        }

        /// <summary>
        /// Object From Front End TextBoxWith Search
        /// </summary>
        private Object _objectFromText;
        public Object ObjectFromText
        {
            get
            {
                return _objectFromText;
            }
            set
            {
                _objectFromText = value;
                if(_objectFromText != null)
                { 
                    SetMember = (CGL_KP_M_Member_H)_objectFromText; 
                    if(SetRevenueType.Revenue_Type_Description.Contains("Repayment"))
                    {
                        GetMemberLoadData = _dataServices.getMemberLoan(SetMember.Member_Id);
                    }
                }
                RaisePropertyChanged("ObjectFromText");
            }
        }

        /// <summary>
        /// Text Employee
        /// </summary>
        private string _employeeText;
        public string EmployeeText
        {
            get
            {
                return _employeeText;
            }
            set
            {
                _employeeText = value;
                RaisePropertyChanged("EmployeeText");
            }
        }

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
        /// Properties for marking New Data
        /// </summary>
        private bool _dataNew;
        public bool DataNew
        {
            get
            {
                return _dataNew;
            }
            set
            {
                _dataNew = value;
                RaisePropertyChanged("DataNew");

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
                _setRevenueType = value;
                if (_setRevenueType != null)
                {
                    if(DataNew == true || DataMod == true)
                    {
                        GetMemberLoadData = null;
                        SetLoanData = null;
                        SetMember = null;
                        EmployeeText = null;
                        ObjectFromText = null;
                        RevenueAmount = 0;

                        if (_setRevenueType.Need_Member == true)
                        { 
                            IsMemberEnabled = true;
                            if (GetEditMode != true)
                                GetEditMode = true;
                            //GetEditMode = true; 
                        }
                        else
                        {
                            IsMemberEnabled = false;
                            GetEditMode = false;
                        }
                        if (_setRevenueType.Revenue_Type_Description.Contains("Repayment"))
                        { IsAmountEnabled = false; IsRepayment = true; }
                        else
                        { IsAmountEnabled = true; IsRepayment = false; }
                    }
                 
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
        /// Check Data
        /// </summary>
        /// <returns>String Of Error (If any)</returns>
        private string getReturnFilled()
        {
            string ret = string.Empty;

            if (SetRevenueType == null)
            {
                ret = "Please Fill Up Revenue";
            }
            if (RevenueAmount == 0)
            {
                ret = "Revenue Amount Could Not Be Zero";
            }
            if(IsMemberEnabled == true && string.IsNullOrEmpty(EmployeeText))
            {
                ret = "Please Fill Member Name";
            }
            if(SetRevenueType.Revenue_Type_Description.Contains("Repayment") && SetLoanData == null)
            {
                ret = "Please Select Loan Data";
            }
            if (BaseLibClass.getDateIsInPeriod(RevenueDate, GetPeriod) == false)
            {
                ret = "Revenue Date Is Not In Period";
            }

           
            return ret;
        }

        /// <summary>
        /// save button
        /// </summary>
        public override void saveButton()
        {
            base.saveButton();
            getNextFocus(FillControl);//get All Last Direction

            if (MessageBox.Show("Are You Sure You Want To Save This Data?", "Revenue Screen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //SetMember =(CGL_KP_M_Member_H) ObjectFromText;
                string _revenueNo = string.Empty;
                string _retBack = getReturnFilled();
                if (_retBack != string.Empty)
                {
                    MessageBox.Show(_retBack, "Revenue Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                int _memberID = 0;
                if(SetMember != null)
                {
                    _memberID = SetMember.Member_Id;
                }
                int _memberLoanID = 0;
                if(SetLoanData != null)
                {
                    _memberLoanID = SetLoanData.Member_Loan_Id;
                }

                //modify
                if (DataMod == true)
                {
                    if (_dataServices.editDataToRevenue(GetTransactionID, RevenueDate, SetRevenueType.Revenue_Type_Id, RevenueAmount, _memberID, (string)BaseLib.Class.Singleton.Instance.TmpUserName, RevenueNo,_memberLoanID) == true)
                    {
                        MessageBox.Show("Revenue Transaction No " + RevenueNo.ToString() + " Successfully Saved!", "Revenue Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                        RevenueNo = _revenueNo;
                        cancelButton();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Revenue Transaction Failed ", "Revenue Screen", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                ////new Data
                else if(DataNew == true)
                {

                    if (_dataServices.saveDataToRevenue(RevenueDate, SetRevenueType.Revenue_Type_Id, RevenueAmount, _memberID, (string)BaseLib.Class.Singleton.Instance.TmpUserName, ref _revenueNo,_memberLoanID) == true)
                   {
                       MessageBox.Show("Revenue Transaction No " + _revenueNo.ToString() + " Successfully Saved!","Revenue Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                       RevenueNo = _revenueNo;
                       cancelButton();
                       return;
                   }
                   else
                   {
                       MessageBox.Show("Revenue Transaction Failed ", "Revenue Screen", MessageBoxButton.OK, MessageBoxImage.Warning);
                       return;
                   }


                }
                else
                {
                    MessageBox.Show("Oops Something Wrong", "Revenue Screen", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
            }
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this); no need to call GC Suppress, since we still need the Garbage Collector to Do its job
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                //handle.Dispose(); Hanlding Unmanager Resource Here, since there are no, so this is Left Marked
                // Free any other managed objects here.
                //
                //cleans Up For Garbage Collection
                CleanObject();
                if (GetMember != null)
                    _getMember = null;
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

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
            
            if (SetRevenueType.Need_Member == true)
            {
                //IsMemberEnabled = true;
                //GetEditMode = true;
                IsMemberEnabled = false;
                GetEditMode = false;
            }

            if (SetRevenueType.Revenue_Type_Description.Contains("Repayment"))
                IsAmountEnabled = false;
            else
                IsAmountEnabled = true;

            ObjEnabledTransactionType = false;

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
        /// Delete Button
        /// </summary>
        public override void deleteButton()
        {
            base.deleteButton();
            if (MessageBox.Show("Are You Sure You Want To Delete This Transaction ?", "Revenue Screen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (_dataServices.deleteDataToRevenue(GetTransactionID, (string)BaseLib.Class.Singleton.Instance.TmpUserName) == true)
                {
                    MessageBox.Show("Revenue Transaction No " + RevenueNo.ToString() + " Successfully Deleted!", "Expenditure Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                    RevenueNo = _revenueNo;
                    cancelButton();
                    return;
                }
                else
                {
                    MessageBox.Show("Expenditure Transaction Failed ", "Expenditure Screen", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
        }


        /// <summary>
        /// Clean Object
        /// </summary>
        private void CleanObject()
        {
            //SetRevenueType = null;
            
            RevenueNo = string.Empty;
            RevenueAmount = 0;
            RevenueDate = DateTime.Now;
            SetPeriod = null;
            SetPeriod = GetPeriod.FirstOrDefault();
            SelectedGridData = null;
            //GetRevenueData = null;
            GetRevenueData = _dataServices.getRevenueTransaction(0, -1);
            EnabledNew = false;
            EnabledSave = false;
            EnabledModify = false;
            EnabledCancel = false;
            EnabledDelete = false;
            DataMod = false;
            ObjEnabled = false;
            ObjFilterEnabled = true;
            SetMember = null;
            //MemberID = 0;
            IsMemberEnabled = false;
            GetEditMode = false;
            ObjectFromText = null;
            EmployeeText = " ";
            EmployeeText = null;
            DataNew = false;
            GetTransactionID = 0;
            GetMemberLoadData = null;
            SetLoanData = null;
            SetRevenueType = null;
            IsAmountEnabled = false;
            IsRepayment = false;
            ObjEnabledTransactionType = false;
            FillControl.Clear();//clear all Fill Control
        }

        /// <summary>
        /// new button
        /// </summary>
        public override void newButton()
        {
            CleanObject();
            base.newButton();
            DataNew = true;
            ObjEnabled = true;
            ObjFilterEnabled = false;
            ObjEnabledTransactionType = true;
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
            _getRevenueData = _dataServices.getRevenueTransaction(0, -1); ;
            _setPeriod = GetPeriod.FirstOrDefault();
            ObjEnabled = false;
            ObjFilterEnabled = true;
            _setMember = null;
            _isMemberEnabled = false;
            _getEditMode = false;
            _objectFromText = null;
            _employeeText = null;
            _dataNew = false;
            _getTransactionID = 0;
            _isAmountEnabled = false;
            _isRepayment = false;
            _getMemberLoadData = null;
            _setLoanData = null;
            _objEnabledTransactionType = false;
        }

        /// <summary>
        /// Event To Command for Grid Selection
        /// </summary>
        /// <param name="x"></param>
        public void getGridSelection(DataGrid x)
        {
            if (SelectedGridData != null && (IsMod == true))
            {
                //DataGridExtensions.SetClearFilterCommand(x,Action);
                showDataOnForm(SelectedGridData);
                EnabledModify = true;
            }

        }

        /// <summary>
        /// Get Focus
        /// </summary>
        /// <param name="dataObject"></param>
        private void getNextFocus(Collection<Control> dataObject)
        {
            foreach (Control dd in dataObject.ToList())
            {
                //Focus Navigation Direction
                FocusNavigationDirection focusDirection = new FocusNavigationDirection();
                focusDirection = FocusNavigationDirection.Next;//
                // MoveFocus takes a TraveralReqest as its argument.
                TraversalRequest request = new TraversalRequest(focusDirection);
                dd.MoveFocus(request);
            }
        }

        /// <summary>
        /// Get Control Focused
        /// </summary>
        /// <param name="x"></param>
        public void getControl(Control x)
        {
            var _checkControl = from dataControl in FillControl where dataControl.Name == x.Name select dataControl;

            if (x.Name != "txWithSearchBoxComp")
            {
                //check
                var _checkTextBox = from dataControl in FillControl where dataControl.Name == "txWithSearchBoxComp" select dataControl;
                if (_checkTextBox.Count() > 0)
                {
                    TextBoxWithSearch dataX = (TextBoxWithSearch)FillControl.FirstOrDefault(xx => xx.Name == "txWithSearchBoxComp");
                    //TextBoxWithSearch dataX = (TextBoxWithSearch)_checkTextBox;
                    if (dataX.GetFilterPopUp.IsOpen == true)
                        dataX.GetFilterPopUp.IsOpen = false;
                } 
                    
            }

            if(_checkControl.Count() > 0)
            {
                //Do Nothing
            }
            else
            {
                FillControl.Add(x);
            }
            //if (_checkControl.Count() > 0)
            //    return;
            //FillControl.Add(x);

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
                GetTransactionID = SelectedGridData.Revenue_Id;
                if(GetEditMode == false)
                {
                    if(SetRevenueType.Need_Member == true)
                    {
                        SetMember = GetMember.FirstOrDefault(x => x.Member_Id == SelectedGridData.Member_Id);
                        //Set This TO Object TextBoxWithSearch
                        ObjectFromText = SetMember;
                        EmployeeText = SetMember.Name;
                    }
                        
                }
                if(SetRevenueType.Revenue_Type_Description.ToString().Contains("Repayment"))
                {
                    GetMemberLoadData = _dataServices.getMemberLoan(SetMember.Member_Id);
                    SetLoanData = GetMemberLoadData.FirstOrDefault(x => x.Member_Loan_Id == SelectedGridData.Member_Loan_Id);
                }
                EnabledDelete = true;//Delete are always possible
            }
        }

        #endregion

        #region "CTOR"
        public RevenueViewModel(IRevenueDataServices dataServices)
        {
            _dataServices = dataServices;
            SelectionGrid = new RelayCommand<DataGrid>(getGridSelection);
            LostFocus = new RelayCommand<Control>(getControl);//get Control Focused
            //initializer
            Initalize();
        }

       
        #endregion

        #region "Grid Event"

        #endregion

    }
}

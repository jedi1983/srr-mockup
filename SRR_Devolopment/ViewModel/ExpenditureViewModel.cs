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
using System.Windows.Data;
using System.Windows.Input;


namespace SRR_Devolopment.ViewModel
{
    public class ExpenditureViewModel : ViewModelBased,IDisposable
    {

        #region "Properties"
        //data services Dependency Injection
        IExpenditureDataServices _dataServices;
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
        /// Max Term Of loan
        /// </summary>
        private int _maxTermOfLoan;
        public int MaxTermOfLoan
        {
            get
            {
                return _maxTermOfLoan;
            }
            set
            {
                _maxTermOfLoan = value;
                RaisePropertyChanged("MaxTermOfLoan");
            }
        }

        /// <summary>
        /// Max Loan Amount
        /// </summary>
        private decimal _maxLoanAmount;
        public decimal MaxLoanAmount
        {
            get
            {
                return _maxLoanAmount;
            }
            set
            {
                _maxLoanAmount = value;
                RaisePropertyChanged("MaxLoanAmount");
            }
        }

        //disposable
        bool disposed = false;

        /// <summary>
        /// Total Installment
        /// </summary>
        private decimal _totalInstallment;
        public decimal TotalInstallment
        {
            get
            {
                return _totalInstallment;
            }
            set
            {
                _totalInstallment = value;
                RaisePropertyChanged("TotalInstallment");
            }
        }

        /// <summary>
        /// Interest Installment
        /// </summary>
        private decimal _interestInstallment;
        public decimal InterestInstallment
        {
            get
            {
                return _interestInstallment;
            }
            set
            {
                _interestInstallment = value;
                RaisePropertyChanged("InterestInstallment");
            }
        }

        /// <summary>
        /// Main INstallment
        /// </summary>
        private decimal _mainInstallment;
        public decimal MainInstallment
        {
            get
            {
                return _mainInstallment;
            }
            set
            {
                _mainInstallment = value;
                RaisePropertyChanged("MainInstallment");
            }
        }

        /// <summary>
        /// Loan Amount
        /// </summary>
        private decimal _loanAmount;
        public decimal LoanAmount
        {
            get
            {
                return _loanAmount;
            }
            set
            {
                _loanAmount = value;
                RaisePropertyChanged("LoanAmount");
            }
        }

        /// <summary>
        /// Term Of Loan
        /// </summary>
        private int _termOfLoan;
        public int TermOfLoan
        {
            get
            {
                return _termOfLoan;
            }
            set
            {
                _termOfLoan = value;
                if(_termOfLoan != 0)
                {
                    
                    MaxTermOfLoan = (int)GetLoanSetting.FirstOrDefault().MAXTENOR;

                    if (_termOfLoan > MaxTermOfLoan)
                    { 
                        MessageBox.Show("You Could Not set Term Of Loan Bigger Than " + MaxTermOfLoan.ToString(), "Expenditure Screen", MessageBoxButton.OK, MessageBoxImage.Warning); 
                        _termOfLoan = 0;
                        MainInstallment = 0;
                        InterestInstallment = 0;
                        TotalInstallment = 0;
                    }
                    else 
                    {
                        MainInstallment = LoanAmount / _termOfLoan;
                        InterestInstallment = LoanAmount * (InterestRate/100) / _termOfLoan;
                        TotalInstallment = MainInstallment + InterestInstallment;
                    }    


                }
                RaisePropertyChanged("TermOfLoan");
            }
        }

        /// <summary>
        /// Interest Rate
        /// </summary>
        private decimal _interestRate;
        public decimal InterestRate
        {
            get
            {
                return _interestRate;
            }
            set
            {
                _interestRate = value;
                RaisePropertyChanged("InterestRate");
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
                RaisePropertyChanged("SetLoanData");
            }
        }

        /// <summary>
        /// Get Member Loan Data
        /// </summary>
        private Collection<USP_CGL_KP_R_Member_Loan_H_Find_Result> _getMemberLoanData;
        public Collection<USP_CGL_KP_R_Member_Loan_H_Find_Result> GetMemberLoanData
        {
            get
            {
                return _getMemberLoanData;
            }
            set
            {
                _getMemberLoanData = value;
                RaisePropertyChanged("GetMemberLoanData");
            }
        }

        /// <summary>
        /// Term Of Loan
        /// </summary>
        private bool _isTermOfLoanEnabled;
        public bool IsTermOfLoanEnabled
        {
            get
            {
                return _isTermOfLoanEnabled;
            }
            set
            {
                _isTermOfLoanEnabled = value;
                RaisePropertyChanged("IsTermOfLoanEnabled");
            }
        }

        /// <summary>
        /// Get Loan Setting
        /// </summary>
        private Collection<USP_CGL_KP_S_Setting_H_Get_Result> _getLoanSetting;
        public Collection<USP_CGL_KP_S_Setting_H_Get_Result> GetLoanSetting
        {
            get
            {
                //_getLoanSetting = _dataServices.getLoanSetting();
                return _getLoanSetting;
            }
            set
            {
                _getLoanSetting = value;
                RaisePropertyChanged("GetLoanSetting");
            }
        }

        /// <summary>
        /// Member Data
        /// </summary>
        private Collection<CGL_KP_M_Member_H> _getMember;
        public Collection<CGL_KP_M_Member_H> GetMember
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
                if (_objectFromText != null)
                {
                    SetMember = (CGL_KP_M_Member_H)_objectFromText;
                    if (SetExpenditureType.Expenditure_Type_Description.Contains("Loan"))
                    {
                        //Get Loan Data
                    }
                }
                RaisePropertyChanged("ObjectFromText");
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
        /// Get Expenditure Data
        /// </summary>
        private Collection<USP_CGL_KP_R_Expenditure_H_Find_Result> _getExpenditureData;
        public Collection<USP_CGL_KP_R_Expenditure_H_Find_Result> GetExpenditureData
        {
            get
            {
                return _getExpenditureData;
            }
            set
            {
                _getExpenditureData = value;
                RaisePropertyChanged("GetExpenditureData");
            }
        }

        /// <summary>
        /// Selected Grid
        /// </summary>
        private USP_CGL_KP_R_Expenditure_H_Find_Result _SelectedGridData;
        public USP_CGL_KP_R_Expenditure_H_Find_Result SelectedGridData
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
        /// Expenditure Amount
        /// </summary>
        private decimal _expenditureAmount;
        public decimal ExpenditureAmount
        {
            get
            {
                return _expenditureAmount;
            }
            set
            {
                _expenditureAmount = value;
                if(SetExpenditureType != null)
                {
                    if(SetExpenditureType.Expenditure_Type_Description.ToString().Contains("Loan"))
                    {
                        LoanAmount = _expenditureAmount;
                        MaxLoanAmount = (decimal)GetLoanSetting.FirstOrDefault().MAXLOAN;
                        if(LoanAmount > MaxLoanAmount)
                        {
                            MessageBox.Show("Loan Mount Could Not Be Greater Than " + MaxLoanAmount.ToString(),"Expenditure Screen",MessageBoxButton.OK,MessageBoxImage.Warning);
                        }
                    }
                }
                RaisePropertyChanged("ExpenditureAmount");
            }
        }

        /// <summary>
        /// Get Expenditure Type
        /// </summary>
        private ObservableCollection<CGL_KP_M_Expenditure_Type_H> _getExpenditureType;
        public ObservableCollection<CGL_KP_M_Expenditure_Type_H> GetExpenditureType
        {
            get
            {
                _getExpenditureType = _dataServices.getExpenditureType();
                return _getExpenditureType;
            }
            set
            {
                _getExpenditureType = value;
                RaisePropertyChanged("GetExpenditureType");
            }
        }

        /// <summary>
        /// Set Expenditure Type
        /// </summary>

        private CGL_KP_M_Expenditure_Type_H _setExpenditureType;
        public CGL_KP_M_Expenditure_Type_H SetExpenditureType
        {
            get
            {
                return _setExpenditureType;
            }
            set
            {
                _setExpenditureType = value;
                if (_setExpenditureType != null)
                {
                    if (DataNew == true || DataMod == true)
                    {
                        GetMemberLoanData = null;
                        SetLoanData = null;
                        SetMember = null;
                        EmployeeText = null;
                        ObjectFromText = null;
                        ExpenditureAmount = 0;
                        IsTermOfLoanEnabled = false;
                        TermOfLoan = 0;
                        MainInstallment = 0;
                        InterestInstallment = 0;
                        TotalInstallment = 0;
                        InterestRate = 0;
                        LoanAmount = 0;

                        if (_setExpenditureType.Need_Member == true)
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
                        if (_setExpenditureType.Expenditure_Type_Description.Contains("Loan"))
                        { //Get Loan Setting Data 
                            //check Edit or New
                            if(DataNew == false && DataMod == true)//edit mode
                            {
                                //do nothing we could not edit data when editing
                            }
                            else if(DataNew == true && DataMod == false)//new
                            {
                                GetLoanSetting = _dataServices.getLoanSetting();
                                if(GetLoanSetting != null)
                                {
                                    InterestRate = (Decimal)GetLoanSetting.FirstOrDefault().INTEREST * 100;
                                    IsTermOfLoanEnabled = true;
                                   
                                }
                            }
                        }
                        else
                        {//other  
                            //no Scenario yet for this
                        }
                    }

                }
                    RaisePropertyChanged("SetExpenditureType");
            }
        }

      

        /// <summary>
        /// Expenditure No
        /// </summary>
        private string _expenditureNo;
        public string ExpenditureNo
        {
            get
            {
                return _expenditureNo;
            }
            set
            {
                _expenditureNo = value;
                RaisePropertyChanged("ExpenditureNo");
            }

        }

        /// <summary>
        /// Expenditure Date
        /// </summary>
        private DateTime _expenditureDate;
        public DateTime ExpenditureDate
        {
            get
            {
                return _expenditureDate;
            }
            set
            {
                _expenditureDate = value;
                RaisePropertyChanged("ExpenditureDate");
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

        #endregion

        #region "Methods"

     

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
            ObjEnabledTransactionType = false;
            DataMod = true;

            if (SetExpenditureType.Need_Member == true)
            {
                //IsMemberEnabled = true;
                //GetEditMode = true;
                IsMemberEnabled = false;
                GetEditMode = false;
            }

            if(SetExpenditureType.Expenditure_Type_Description.ToString().Contains("Loan"))
            {
                IsTermOfLoanEnabled = true;
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
                //handle.Dispose(); Hanlding Unmanaged Resource Here, since there are no, so this is Left Marked
                // Free any other managed objects here.
                //
                //cleans Up For Garbage Collection
               
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
            GetExpenditureData = null;
            if (SetPeriod == null)
            {
                MessageBox.Show("Please Set The Period", "Expenditure Transaction Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                GetExpenditureData = _dataServices.getExpenditureTransaction(0, SetPeriod.Period_Id);
            }
        }

        /// <summary>
        /// show data to the form
        /// </summary>
        /// <param name="SelectedGridData">Object Param</param>
        public void showDataOnForm(USP_CGL_KP_R_Expenditure_H_Find_Result SelectedGridData)
        {
            if (SelectedGridData != null)
            {

                

                ExpenditureDate = SelectedGridData.Expenditure_Date;
                ExpenditureNo = SelectedGridData.Expenditure_No;
                GetTransactionID = SelectedGridData.Expenditure_Id;
                ExpenditureAmount = SelectedGridData.Expenditure_Amount;
                SetExpenditureType = GetExpenditureType.FirstOrDefault(x => x.Expenditure_Type_Id == SelectedGridData.Expenditure_Type_Id);
                if (GetEditMode == false)
                {
                    if (SetExpenditureType.Need_Member == true)
                    {
                        SetMember = GetMember.FirstOrDefault(x => x.Member_Id == SelectedGridData.Member_Id);
                        //Set This TO Object TextBoxWithSearch
                        ObjectFromText = SetMember;
                        EmployeeText = SetMember.Name;
                    }
                    if(SetExpenditureType.Expenditure_Type_Description.ToString().Contains("Loan"))
                    {
                        if (SelectedGridData.Is_Approved == false)
                        { EnabledModify = true; EnabledDelete = true; }
                        var _id = (int)SelectedGridData.Member_Id;
                        GetMemberLoanData = _dataServices.getMemberLoan(_id);
                        SetLoanData = GetMemberLoanData.FirstOrDefault(x => x.Member_Loan_Id == (int)SelectedGridData.Member_Loan_Id);
                        InterestRate = SetLoanData.Interest_Rate*100;
                        TermOfLoan = SetLoanData.Term_of_Loan;
                        LoanAmount = ExpenditureAmount;
                        MainInstallment = SetLoanData.Loan_Amount / SetLoanData.Term_of_Loan;
                        InterestInstallment = SetLoanData.Loan_Amount * SetLoanData.Interest_Rate / SetLoanData.Term_of_Loan;
                        TotalInstallment = InterestInstallment + MainInstallment;
                        LoanAmount = SetLoanData.Loan_Amount;
                    }
                    else
                    {
                        EnabledModify = true;
                        IsTermOfLoanEnabled = false;
                        EnabledDelete = true;
                    }

                }
            }
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
                //EnabledModify = true;
            }

        }

        /// <summary>
        /// Clean Object
        /// </summary>
        private void CleanObject()
        {
            SetExpenditureType = null;
            ExpenditureNo = string.Empty;
            ExpenditureAmount = 0;
            ExpenditureDate = DateTime.Now;
            SetPeriod = null;
            SetPeriod = GetPeriod.FirstOrDefault();
            SelectedGridData = null;
            GetExpenditureData = null;
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
            GetMemberLoanData = null;
            SetLoanData = null;
            SetExpenditureType = null;
            IsTermOfLoanEnabled = false;
            MaxLoanAmount = 0;
            MaxTermOfLoan = 0;
            LoanAmount = 0;
            InterestRate = 0;
            TermOfLoan = 0;
            LoanAmount = 0;
            MainInstallment = 0;
            InterestInstallment = 0;
            TotalInstallment = 0;
            ObjEnabledTransactionType = false;
            FillControl.Clear();//clear Fill Control
        }

        /// <summary>
        /// Check Data
        /// </summary>
        /// <returns>String Of Error (If any)</returns>
        private string getReturnFilled()
        {
            string ret = string.Empty;

            if (SetExpenditureType == null)
            {
                ret = "Please Fill Up Expenditure Type";
            }
            if (ExpenditureAmount == 0)
            {
                ret = "Expenditure Amount Could Not Be Zero";
            }
            if (ExpenditureDate > GetPeriod.FirstOrDefault().Period_End_Date || ExpenditureDate < GetPeriod.FirstOrDefault().Period_Start_Date)
            {
                ret = "Expenditure Date Is Not In The Period " + GetPeriod.FirstOrDefault().Period_Name.ToString();
            }
            if (IsMemberEnabled == true &&  String.IsNullOrEmpty(EmployeeText))//change string to null or empty
            {
                ret = "Please Fill Member Name";
            }
            if (SetExpenditureType.Expenditure_Type_Description.Contains("Loan") && TermOfLoan == 0)
            {
                ret = "Please Fill Term Loan";
            }


            return ret;
        }

        /// <summary>
        /// Delete Button
        /// </summary>
        public override void deleteButton()
        {
            base.deleteButton();
            if (MessageBox.Show("Are You Sure You Want To Delete This Transaction ?", "Expenditure Screen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (_dataServices.deleteDataToExpenditure(GetTransactionID, (string)BaseLib.Class.Singleton.Instance.TmpUserName) == true)
                {
                    MessageBox.Show("Expenditure Transaction No " + ExpenditureNo.ToString() + " Successfully Deleted!", "Expenditure Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                    ExpenditureNo = _expenditureNo;
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

            if (_checkControl.Count() > 0)
            {
                //Do Nothing
            }
            else
            {
                FillControl.Add(x);
            }
           
            //var _checkControl = from dataControl in FillControl where dataControl.Name == x.Name select dataControl;
            //if (_checkControl.Count() > 0)
            //    return;
            //FillControl.Add(x);

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
        /// save button
        /// </summary>
        public override void saveButton()
        {
            base.saveButton();
            
            getNextFocus(FillControl);//Get Filled Control
            TermOfLoan = TermOfLoan;//check for loan

            if (MessageBox.Show("Are You Sure You Want To Save This Data?", "Expenditure Screen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //SetMember =(CGL_KP_M_Member_H) ObjectFromText;
                string _expenditureNo = string.Empty;
                string _retBack = getReturnFilled();
                int _loanData = 0;
                if (_retBack != string.Empty)
                {
                    MessageBox.Show(_retBack, "Expenditure Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                int _memberID = 0;
                if (SetMember != null)
                {
                    _memberID = SetMember.Member_Id;
                }
                int _memberLoanID = 0;

                if (SetLoanData != null && SetLoanData.Member_Loan_Id > 0)
                {
                    _memberLoanID = SetLoanData.Member_Loan_Id;
                }

                if(SetLoanData != null)
                {
                    _loanData = SetLoanData.Member_Loan_Id;
                }

                //modify
                if (DataMod == true)
                {

                    if (_dataServices.editDataToExpenditure(GetTransactionID, ExpenditureDate, SetExpenditureType.Expenditure_Type_Id, ExpenditureAmount, _memberID, (string)BaseLib.Class.Singleton.Instance.TmpUserName, InterestRate, TermOfLoan, _loanData) == true)
                    {
                        MessageBox.Show("Expenditure Transaction No " + ExpenditureNo.ToString() + " Successfully Saved!", "Expenditure Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                        ExpenditureNo = _expenditureNo;
                        cancelButton();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Expenditure Transaction Failed ", "Expenditure Screen", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                ////new Data
                else if (DataNew == true)
                {
                    //decimal _interestRate = ;
                    if (_dataServices.saveDataToExpenditure(ExpenditureDate, SetExpenditureType.Expenditure_Type_Id, ExpenditureAmount, _memberID, (string)BaseLib.Class.Singleton.Instance.TmpUserName, ref _expenditureNo, (decimal)GetLoanSetting.FirstOrDefault().INTEREST, TermOfLoan) == true)
                    {
                        MessageBox.Show("Expenditure Transaction No " + _expenditureNo.ToString() + " Successfully Saved!", "Expenditure Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                        ExpenditureNo = _expenditureNo;
                        cancelButton();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Expenditure Transaction Failed ", "Expenditure Screen", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                
                }
                else
                {
                    MessageBox.Show("Oops Something Wrong", "Expenditure Screen", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
            }
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

        private void Initalize()
        {

            ////properties data Level
            _setExpenditureType = null;
            _expenditureAmount = 0;
            _expenditureNo = string.Empty;
            ObjEnabled = false;
            ObjFilterEnabled = true;
            _dataMod = false;
            _expenditureDate = DateTime.Now;
            ////end here
            _dataAccessLevel = BaseLib.Class.Singleton.Instance.AccessRight;
            getScreenAccess();
            _getExpenditureData = null;
            _setPeriod = GetPeriod.FirstOrDefault();
            _setMember = null;
            _isMemberEnabled = false;
            _getEditMode = false;
            _objectFromText = null;
            _employeeText = null;
            _dataNew = false;
            _getTransactionID = 0;
            _isTermOfLoanEnabled = false;
            _interestRate = 0;
            _termOfLoan = 0;
            _loanAmount = 0;
            _mainInstallment = 0;
            _interestInstallment = 0;
            _totalInstallment = 0;
            _maxLoanAmount = 0;
            _maxTermOfLoan = 0;
            //_isRepayment = false;
            _getMemberLoanData = null;
            _setLoanData = null;
            _getLoanSetting = _dataServices.getLoanSetting();
            _objEnabledTransactionType = false;
        }


        #endregion

        #region "CTOR"
         public ExpenditureViewModel(IExpenditureDataServices dataServices)
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

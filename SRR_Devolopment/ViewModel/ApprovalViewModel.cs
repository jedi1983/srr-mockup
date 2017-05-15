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
using System.Windows.Controls.Primitives;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows.Data;



namespace SRR_Devolopment.ViewModel
{
    public class ApprovalViewModel : ViewModelBased,IDisposable
    {

        #region "Properties"

        //data services Dependency Injection
        IApprovalDataService _dataServices;

        //disposable
        bool disposed = false;

        public RelayCommand<DataGrid> SelectionGrid { get; set; }

        /// <summary>
        /// Selected Grid Data
        /// </summary>
        private USP_CGL_KP_R_Expenditure_H_UnApproved_Yet_Result _SelectedGridData;
        public USP_CGL_KP_R_Expenditure_H_UnApproved_Yet_Result SelectedGridData
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
        /// GetApproval
        /// </summary>
        private Collection<USP_CGL_KP_R_Expenditure_H_UnApproved_Yet_Result> _getApprovalData;
        public Collection<USP_CGL_KP_R_Expenditure_H_UnApproved_Yet_Result> GetApprovalData
        {
            get 
            {
                //_getApprovalData = _dataServices.getUnApprovedTransaction();
                return _getApprovalData;
            }
            set
            {
                _getApprovalData = value;
                RaisePropertyChanged("GetApprovalData");
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
                //CleanObject();
                //if (GetMember != null)
                //    _getMember = null;
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

       

        /// <summary>
        /// Event To Command for Grid Selection
        /// </summary>
        /// <param name="x"></param>
        public void getGridSelection(DataGrid x)
        {
          
            if (SelectedGridData != null && (IsMod == true))
            {
                //showDataOnForm(SelectedGridData);
                //EnabledModify = true;
            }

        }

        /// <summary>
        /// Delete Button / Reject Button
        /// </summary>
        public override void deleteButton()
        {
            base.deleteButton();

            var _selectData = from tbl in GetApprovalData where tbl.Flag == true select tbl;



            if (_selectData.Count() < 1)
            {
                MessageBox.Show("Please Select At Least One Data To Reject ", "Approval Screen", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (MessageBox.Show("Are You Sure You Want To Reject This Transaction ?", "Approval Screen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (_dataServices.rejectTransaction(GetApprovalData, (string)BaseLib.Class.Singleton.Instance.TmpUserName) == true)
                {
                    MessageBox.Show("Transaction Rejected ", "Approval Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                    CleanObject();
                    return;
                }
                else
                {
                    MessageBox.Show("Rejection Failed! ", "Approval Screen", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

        }



        /// <summary>
        /// Process Button
        /// </summary>
        public override void processButton()
        {
            base.processButton();


            var _selectData = from tbl in GetApprovalData where tbl.Flag == true select tbl;



            if(_selectData.Count() < 1)
            {
                MessageBox.Show("Please Select At Least One Data To Approve " , "Approval Screen", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if(MessageBox.Show("Are You Sure You Want To Approve This Transaction ?","Approval Screen",MessageBoxButton.YesNo,MessageBoxImage.Question)==MessageBoxResult.Yes)
            {
                if (_dataServices.saveTransaction(GetApprovalData, (string)BaseLib.Class.Singleton.Instance.TmpUserName) == true)
                {
                    MessageBox.Show("Transaction Approved ", "Approval Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                    CleanObject();
                    return;
                }
                else
                {
                    MessageBox.Show("Approval Failed! ", "Approval Screen", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
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

        /// <summary>
        /// Clean Object
        /// </summary>
        private void CleanObject()
        {
            GetApprovalData = null;
            GetApprovalData = _dataServices.getUnApprovedTransaction();
            SelectedGridData = null;

        }

        private void Initalize()
        {
            _dataAccessLevel = BaseLib.Class.Singleton.Instance.AccessRight;
            getScreenAccess();
            _getApprovalData = _dataServices.getUnApprovedTransaction();
        }

        #endregion

        #region "CTOR"

        public ApprovalViewModel(IApprovalDataService dataServices)
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

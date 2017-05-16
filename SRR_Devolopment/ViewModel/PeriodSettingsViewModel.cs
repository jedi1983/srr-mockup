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
    public class PeriodSettingsViewModel : ViewModelBased
    {
        #region "Properties"

        IPeriodDataService _dataServices;

        /// <summary>
        /// Month Year
        /// </summary>
        private DateTime _yearMonthDate;
        public DateTime YearMonthDate
        {
            get
            {
                return _yearMonthDate;
            }
            set
            {
                _yearMonthDate = value;
                RaisePropertyChanged("YearMonthDate");
            }
        }

        /// <summary>
        /// Grid Data
        /// </summary>
        private ICollection<USP_CGL_KP_M_Period_Status_Result> _getPeriodSetting;
        public ICollection<USP_CGL_KP_M_Period_Status_Result> GetPeriodSetting
        {
            get
            {
                return _getPeriodSetting;
            }
            set
            {
                _getPeriodSetting = value;
                RaisePropertyChanged("GetPeriodSetting");
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

        private void CleanObject()
        {
            getScreenAccess();
            GetPeriodSetting = null;
            GetPeriodSetting = _dataServices.GetDataGridData();

        }

        /// <summary>
        /// Close Period
        /// </summary>
        public override void deleteButton()
        {
            base.deleteButton();

            if (MessageBox.Show("Are You Sure You Want Closed Period Of " + YearMonthDate.Month.ToString() + " - " + YearMonthDate.Year.ToString() + " ? ", "Period Screen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string _messageBack = string.Empty;
                if (_dataServices.closePeriod(YearMonthDate, ref _messageBack, (string)BaseLib.Class.Singleton.Instance.TmpUserName) == true)
                {
                    MessageBox.Show(_messageBack, "Period Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                    CleanObject();
                    return;
                }
                else
                {
                    MessageBox.Show(_messageBack, "Period Screen", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
        }

        /// <summary>
        /// Create New Period
        /// </summary>
        public override void generateButton()
        {
            base.generateButton();

            if (MessageBox.Show("Are You Sure You Want Create New Period Of " + YearMonthDate.Month.ToString() + " - " + YearMonthDate.Year.ToString() + " ? ", "Period Screen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string _messageBack = string.Empty;
                if (_dataServices.newPeriod(YearMonthDate, ref _messageBack, (string)BaseLib.Class.Singleton.Instance.TmpUserName) == true)
                {
                    MessageBox.Show(_messageBack, "Period Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                    CleanObject();
                    return;
                }
                else
                {
                    MessageBox.Show(_messageBack, "Period Screen", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
        }

        /// <summary>
        /// Reopen Period
        /// </summary>
        public override void processButton()
        {
            base.processButton();
            
            if (MessageBox.Show("Are You Sure You Want To Re-Open Period Of "+ YearMonthDate.Month.ToString() +" - "+YearMonthDate.Year.ToString()+" ? ", "Period Screen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string _messageBack = string.Empty;
                if (_dataServices.reOpenPeriod(YearMonthDate, ref _messageBack, (string)BaseLib.Class.Singleton.Instance.TmpUserName) == true)
                {
                    MessageBox.Show(_messageBack, "Period Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                    CleanObject();
                    return;
                }
                else
                {
                    MessageBox.Show(_messageBack, "Period Screen", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void Initalize()
        {
            _yearMonthDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _dataAccessLevel = BaseLib.Class.Singleton.Instance.AccessRight;
            getScreenAccess();
            _getPeriodSetting = null;
            _getPeriodSetting = _dataServices.GetDataGridData();
        }
        #endregion

        #region "CTOR"
        public PeriodSettingsViewModel(IPeriodDataService dataServices)
        {
            _dataServices = dataServices;
            //initializer
            Initalize();
        }
        #endregion

        #region "Grid Event"

        #endregion

    }
}

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
    public class ProcessViewModel : ViewModelBased
    {

#region "Properties"

        IProcessDataService _dataServices;

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
            if(SetPeriod == null)
            {
                MessageBox.Show("There Are No Period Selected Yet, Please Select At Least One Period", "Process Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            base.generateButton();
            if(MessageBox.Show("Are You Sure You Want To Generate Iuran Wajib?","Process Screen",MessageBoxButton.YesNo,MessageBoxImage.Question)== MessageBoxResult.Yes)
            {
                string _messageBack = string.Empty;
                DateTime _dataNew = new DateTime(SetPeriod.Year, SetPeriod.Month, 1);
                if(_dataServices.generateIuran(_dataNew,ref _messageBack)==true)
                {
                    MessageBox.Show(_messageBack, "Process Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                    CleanObject();
                    return;
                }
                else
                {
                    MessageBox.Show(_messageBack, "Process Screen", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
        }


        public override void processButton()
        {
            base.processButton();
            if (SetPeriod == null)
            {
                MessageBox.Show("There Are No Period Selected Yet, Please Select At Least One Period", "Process Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Are You Sure You Want To Generate Balance Calculation?", "Process Screen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string _messageBack = string.Empty;
                DateTime _dataNew = new DateTime(SetPeriod.Year, SetPeriod.Month, 1);
                if (_dataServices.balanceCalculation(_dataNew, ref _messageBack) == true)
                {
                    MessageBox.Show(_messageBack, "Process Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                    CleanObject();
                    return;
                }
                else
                {
                    MessageBox.Show(_messageBack, "Process Screen", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
        }

        /// <summary>
        /// Generate Loan Payment
        /// </summary>
        public override void exportButton()
        {
            base.exportButton();
            if (SetPeriod == null)
            {
                MessageBox.Show("There Are No Period Selected Yet, Please Select At Least One Period", "Process Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Are You Sure You Want To Generate Loan Payment?", "Process Screen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string _messageBack = string.Empty;
                DateTime _dataNew = new DateTime(SetPeriod.Year, SetPeriod.Month, 1);
                if (_dataServices.loanPaymentCalculation(_dataNew, ref _messageBack) == true)
                {
                    MessageBox.Show(_messageBack, "Process Screen", MessageBoxButton.OK, MessageBoxImage.Information);
                    CleanObject();
                    return;
                }
                else
                {
                    MessageBox.Show(_messageBack, "Process Screen", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void CleanObject()
        {
            SetPeriod = GetPeriod.FirstOrDefault();
        }

        /// <summary>
        /// Initialize
        /// </summary>
        private void Initalize()
        {
            _dataAccessLevel = BaseLib.Class.Singleton.Instance.AccessRight;
            getScreenAccess();
            _setPeriod = GetPeriod.FirstOrDefault();
          
        }

#endregion

#region "CTOR"
        public ProcessViewModel(IProcessDataService dataServices)
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

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

namespace SRR_Devolopment.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        #region "Helper"
        private readonly IDataServices _dataServices;
        #endregion

        #region "Properties"

        public RelayCommand Login { get; set; }

        #endregion

        #region "Command Methods"

        #endregion

        #region "CTOR"
        public LoginViewModel(IDataServices dataServices)
        {

            _dataServices = dataServices;

            Login = new RelayCommand(getLogin);
        }

        #endregion
     

        void getLogin()
        {
            MessageBox.Show("Testing", "Testing", MessageBoxButton.OK);
        }
 
    }
}

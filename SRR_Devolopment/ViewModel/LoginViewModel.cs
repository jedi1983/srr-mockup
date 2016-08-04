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

namespace SRR_Devolopment.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        #region "Helper"
        private readonly IDataServices _dataServices;
        #endregion

        #region "Properties"
        
        public RelayCommand Login { get; set; }

        public RelayCommand <PasswordBox> PasswordChanged { get; set; }

        private string _username;

        private ObservableCollection<SRR_M_User_Login_H> _dataBack;

        public ObservableCollection<SRR_M_User_Login_H> DataBack
        {
            get { return _dataBack; }
            set { _dataBack = value; }
        }
        

        public string Username
	    {
            get { return _username; }
            set { _username = value; }
	    }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }


        #endregion

        #region "Command Methods"

        void getLogin()
        {
        
            DataBack = _dataServices.GetLoginID(Username, Password);
            if (DataBack.Count != 0)
            {
                MessageBox.Show("Yes Login Success", "Testing", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("No Data", "Testing", MessageBoxButton.OK);

         }

        void getPassword(PasswordBox passWord)
        {
           Password = passWord.Password;
        }

        #endregion

        #region "CTOR"
        public LoginViewModel(IDataServices dataServices)
        {

            _dataServices = dataServices;
            Login = new RelayCommand(getLogin);
            PasswordChanged = new RelayCommand<PasswordBox>(getPassword);
            Username = string.Empty;
            Password = string.Empty;
        }

        #endregion

    }
}

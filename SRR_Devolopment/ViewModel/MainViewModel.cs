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
using SRR_Devolopment.BaseLib.UserControl;
using SRR_Devolopment.BaseLib.Class;
using GalaSoft.MvvmLight.Messaging;
using SRR_Devolopment.MessageInfrastructure;

namespace SRR_Devolopment.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        #region "Helper"
        private readonly IDataServices _dataServices;
        #endregion

        #region "Methods"

        /// <summary>
        /// This is Method for accepting Message from other ModelView
        /// </summary>
        void ReceiveStatusInfo()
        {
            Messenger.Default.Register<Messaging>(this, (SuccessStatus) =>
                {
                    this.SuccessStatusV = SuccessStatus.SuccessStatus;
                });
        }
 
        void Initalize()
        {
            _successStatusV = false;
            _menuLoad = "/SRR_Devolopment;component/Views/ucLoginPage.xaml";
            _moduleLoad = ModuleLoadObject.GetModule(_menuLoad);
            ReceiveStatusInfo();
        }

        void SendStatusInfo(bool stat)
        {
            //sending status of success login
            Messenger.Default.Send<Messaging>(new Messaging() { SuccessStatus = stat });

        }

        void getLogin()
        {
            DataBack = _dataServices.GetLoginID(Username, Password);
            if (DataBack.Count != 0)
            {
                MessageBox.Show("Login Correct", "Koperasi", MessageBoxButton.OK, MessageBoxImage.Hand);
                SendStatusInfo(true);
            }
            else
                MessageBox.Show("Please Use The Correct User Name And Password", "Koperasi", MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        void getPassword(PasswordBox passWord)
        {
            Password = passWord.Password;
        }

        #endregion

        #region "Properties"

        private bool _successStatusV;

        public  bool SuccessStatusV
        {
            get{return this._successStatusV;}
            set {
                 this._successStatusV = value;
                 if (this._successStatusV == true)//Correct Login will call Methods under this If Statement
                 { 
                     //load the default screen name for Tree List
                     ModuleLoad = ModuleLoadObject.GetModule("/SRR_Devolopment;component/Views/ucTreeListMenu.xaml");
                 }
                RaisePropertyChanged("SuccessStatusV");
                }
        }

        private Object _moduleLoad;
        public Object ModuleLoad
        {
            get { return _moduleLoad; }
            set { 
                    _moduleLoad = value;
                    RaisePropertyChanged("ModuleLoad");
                }
        }

        private string _menuLoad;
        public string MenuLoad
        {
            get { return  _menuLoad; }
            set {  
                    _menuLoad = value;
                    RaisePropertyChanged("MenuLoad");
                }
        }

        public RelayCommand Login { get; set; }

        public RelayCommand<PasswordBox> PasswordChanged { get; set; }

        private string _username;

        private ObservableCollection<SRR_M_User_Login_H> _dataBack;

        public ObservableCollection<SRR_M_User_Login_H> DataBack
        {
            get { return _dataBack; }
            set
            {
                _dataBack = value;
                RaisePropertyChanged("DataBack");
            }
        }


        public string Username
        {
            get { return this._username; }
            set
            {
                this._username = value;
                RaisePropertyChanged("Username");
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }


        #endregion

        #region"Ctor"


        public MainViewModel(IDataServices dataServices)
        {
            //initializer
            Initalize();
            _dataServices = dataServices;
            Login = new RelayCommand(getLogin);
            PasswordChanged = new RelayCommand<PasswordBox>(getPassword);
            _username = string.Empty; //setting first value of username
            _password = string.Empty; //setting first value of password
            
        }

        #endregion 
    }
}
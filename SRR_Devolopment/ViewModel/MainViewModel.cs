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
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBased
    {
        

        #region "Helper"
        private readonly IDataServices _dataServices;
        #endregion

        #region "Methods"

       
        /// <summary>
        /// Button to Close Down the Tab
        /// </summary>
        /// <param name="sender">it would be the Sender Object</param>
        /// <param name="e">event argument</param>
        public void clickButtonClose(object sender, System.EventArgs e)
        {
            Button dataBut = (Button)sender;
            if(TabCollection != null)
            {
                var linQ = from tbl in TabCollection
                           where tbl.Name.ToString().Contains(dataBut.Name)
                           select tbl;

                TabCollection.Remove((CloseableTab)linQ.FirstOrDefault());
                //clenups the View Model Data this will using SimpleIoC unregister and re-register the Application
                //ViewModelLocator.Cleanup(dataBut.Name);
            }
        }

        
        /// <summary>
        /// Setting Up Disposable TextBox
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Event Args</param>
        public void unloadedItem(object sender, System.EventArgs e)
        {
            CloseableTab _disposeAbleItem = (CloseableTab)sender;
            _disposeAbleItem.Dispose();
        }

        

        public void TextBoxMenuClicked(object sender,System.EventArgs e)
        {
            //setting Visibilty of the Tab Control to Visible, after hide it in Menu Plain UI
            VisibilitySet = Visibility.Visible;
            TextBlock menuDataName;
            menuDataName = (TextBlock)sender; // change the sender object to TextBlock
            string formName = string.Empty;
            string formURI = string.Empty;
            //Be Hold!
            //this will load the Appropriate XAML UC to the Docking
            formName = menuDataName.Text;
            var linqToObj = from tbl in DataForm where tbl.Form_Name == formName select tbl;
            formURI = linqToObj.FirstOrDefault().Uri;
            MenuLoad = string.Format("/SRR_Devolopment;component/views/{0}{1}",formURI.ToString(),".xaml");
            if(TabCollection == null) //then this Tab is Already filled
            {
                TabCollection = new ObservableCollection<CloseableTab>();
            }
            //checking menu does this menu already load?.
            var checkMenu = from x in TabCollection where x.Name.ToString().Contains(formURI.ToString()) select x;
            if(checkMenu.Count() > 0)
            {
                MessageBox.Show("There Are Already A Tab Open For This Menu, Please Use Those Tab Instead", "Koperasi Menu", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            //
            int TabMenuControlCount = TabCollection.Count;
            TextBlock HeaderData = new TextBlock();
            HeaderData.Text = formName + "  ";//adding space
            CloseableTab menuData = new CloseableTab();
            menuData.Name = formURI.ToString();
            menuData.SetHeader(HeaderData);
            menuData.DataButton.Name = formURI.ToString();
            menuData.DataButton.Click += clickButtonClose;//adding Event Click
            menuData.Unloaded += unloadedItem;//unloaded Items To Check Dispose of Object
            menuData.Content = ModuleLoadObject.GetModule(MenuLoad);
            TabCollection.Add(menuData);
            SelectedItemTab = menuData;
           

        }

     
        /// <summary>
        /// Click Event From the Main Expander 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void toolStripClick(object sender, System.EventArgs e)
        {
            DataForm = null;
            Expander dataHold;
            //checking the Object Data Sender is it the same as now?
            if (DataMenuHold != null && DataMenuHold != sender)
            {
                
                
                dataHold = (Expander)DataMenuHold;
                dataHold.IsExpanded = false;
                DataMenuHold = null;

            }
            
            StackPanel dataMenu = new StackPanel();
            dataHold = (Expander)sender;
            DataForm = _dataServices.GetFormMasterData(DataBack.FirstOrDefault().Group_Id);
            int form_Type_Id = 0;
            string headerConver = dataHold.Header.ToString();
            using (srr_devEntities sr = new srr_devEntities())
            {
                form_Type_Id = _dataServices.GetFormTypeId(headerConver);

                var dataLinq = from tbl in DataForm
                               where tbl.Form_Type_Id == form_Type_Id
                               select tbl;

                foreach (var x in dataLinq)
                {
                    TextBlock newBut = new TextBlock();
                    newBut.Text = x.Form_Name;
                    newBut.Padding = new Thickness(6, 6, 6, 6);
                    newBut.MouseLeftButtonUp += TextBoxMenuClicked;
                    dataMenu.Children.Add(newBut);
                    
                }

            }

            dataHold.Content = dataMenu;
            DataMenuHold = dataHold;
            
        }

        /// <summary>
        /// Creating The Expander Main Menu (Transaction,Master,Setup ETC)
        /// </summary>
        /// <param name="dataInsert"></param>
        private void setUpScreenMenu(ObservableCollection<USP_CG_KP_M_UserProfile_H_Find_Result> dataInsert)
        {
            //TODO : if there are more than one group ID!
            //Room for more thoughts on this
            //
            ObservableCollection<USP_CG_KP_M_AccessRights_H_Find_Result> AccessRight = _dataServices.GetAccessRights(dataInsert.FirstOrDefault().Group_Id);
            //setting up Singleton Object with access Right and tmpUserName Data
            BaseLib.Class.Singleton.Instance.TmpUserName = dataInsert.FirstOrDefault().User_Id;
            BaseLib.Class.Singleton.Instance.AccessRight = AccessRight;
            //end Singleton Setting
            ObservableCollection<USP_CG_KP_M_Form_Type_H_Find_Result> dataBack = _dataServices.GetFormType(dataInsert.FirstOrDefault().Group_Id);
            List<Expander> MenuCollection = new List<Expander>();
            StackPanel dataHandler = new StackPanel();

            foreach (var x in dataBack)
            {
                Expander data = new Expander();
                data.Header = x.Form_Type_Name.ToString();
                MenuCollection.Add(data);
                data.Expanded += toolStripClick; 
            }
            
            foreach(var x in MenuCollection)
            {
                dataHandler.Children.Add(x);
            }
            MenuObject = dataHandler;
        }

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
        
        /// <summary>
        /// Initialize Methods for the Main View Model
        /// </summary>
        void Initalize()
        {
            _successStatusV = false;
            _dataMenuHold = null;
            ReceiveStatusInfo();
        }

        /// <summary>
        /// for sending data accross View Model
        /// </summary>
        /// <param name="stat">status (boolean true/false)</param>
        void SendStatusInfo(bool stat)
        {
            //sending status of success login
            Messenger.Default.Send<Messaging>(new Messaging() { SuccessStatus = stat });

        }
        /// <summary>
        /// Overidable Methods of Login button
        /// </summary>
        public override void LoginButton()
        {
            DataBack = _dataServices.GetLoginID(Username, Password);
            if (DataBack.Count != 0)
            {
                SendStatusInfo(true);
            }
            else
                MessageBox.Show("Please Use The Correct User Name And Password", "Koperasi", MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        /// <summary>
        /// Event To Command Getting the password, i forced to do this since passwordbox literally gave no plaintext text
        /// </summary>
        /// <param name="passWord">password data</param>
        void getPassword(PasswordBox passWord)
        {
            Password = passWord.Password;
        }

        #endregion

        #region "Properties"
     

        private object _selectedItemTab;
        public object SelectedItemTab
        {
            get
            {
                return _selectedItemTab;
            }

            set
            {
                _selectedItemTab = value;
                RaisePropertyChanged("SelectedItemTab");
            }
        }

        private Visibility _visibilitySet;
        public Visibility VisibilitySet
        {
            get
            {
                return _visibilitySet;
            }
            set
            {
                _visibilitySet = value;
                RaisePropertyChanged("VisibilitySet");
            }


        }

        /// <summary>
        /// WIll Delete This After test 
        /// </summary>
        private ObservableCollection<CloseableTab> _tabCollection;
        public ObservableCollection<CloseableTab> TabCollection
        {
            get
            {
                return _tabCollection;
            }
                
            set
            {
                _tabCollection = value;
                RaisePropertyChanged("TabCollection");
            }

        }

        /// <summary>
        /// Form Master Find Result
        /// </summary>
        private ObservableCollection<USP_CG_KP_M_FormMaster_H_Find_R_Result> _dataForm;
        public ObservableCollection<USP_CG_KP_M_FormMaster_H_Find_R_Result> DataForm
        {
            get
            {
                return _dataForm;
            }
            set
            {
                _dataForm = value;
                RaisePropertyChanged("DataForm");
            }
        }
        
       
        private object _dataMenuHold;
        public object DataMenuHold
        {
            get { return _dataMenuHold; }
            set
            {
                _dataMenuHold = value;
                RaisePropertyChanged("DataMenuHold");
            }
        }


        private object _menuObject;
        public object MenuObject
        {
            get { return _menuObject; }
            set 
            { 
            _menuObject = value;
            RaisePropertyChanged("MenuObject");
            }
        }

        private bool _successStatusV;

        public  bool SuccessStatusV
        {
            get{return this._successStatusV;}
            set {
                 this._successStatusV = value;
                 if (this._successStatusV == true)//Correct Login will call Methods under this If Statement
                 { 
                     IsMenuSelected = true;
                     EnableMenu = true;
                     IsLoginSelected = false;
                     EnableLogin = false;
                     //behold! this will set and align all the menu for us
                     setUpScreenMenu(DataBack);
                 }
                RaisePropertyChanged("SuccessStatusV");
                }
        }

      
        /// <summary>
        /// Menu Load
        /// </summary>
        private string _menuLoad;
        public string MenuLoad
        {
            get { return  _menuLoad; }
            set {  
                    _menuLoad = value;
                    RaisePropertyChanged("MenuLoad");
                }
        }

       

        public RelayCommand<PasswordBox> PasswordChanged { get; set; }

        private string _username;

        private ObservableCollection<USP_CG_KP_M_UserProfile_H_Find_Result> _dataBack;

        public ObservableCollection<USP_CG_KP_M_UserProfile_H_Find_Result> DataBack
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

        private bool _enableLogin;
        public bool EnableLogin
        {
            get {return _enableLogin;}
            set
                {
                    _enableLogin = value;
                    RaisePropertyChanged("EnableLogin");
                }
        }

        private bool _enableMenu;
        public bool EnableMenu
        {
            get { return _enableMenu; }
            set 
            { 
            _enableMenu = value;
            RaisePropertyChanged("EnableMenu");
            }
        }

        private bool _isMenuSelected;
        public bool IsMenuSelected
        {
            get { return _isMenuSelected; }
            set
            {
                _isMenuSelected = value;
                RaisePropertyChanged("IsMenuSelected");
            }
        }

        private bool _isLoginSelected;
        public bool IsLoginSelected
        {
            get { return _isLoginSelected; }
            set
            {
                _isLoginSelected = value;
                RaisePropertyChanged("IsLoginSelected");
            }
        }


        #endregion

        #region"Ctor"


        public MainViewModel(IDataServices dataServices)
        {
            //initializer
            Initalize();
            _dataServices = dataServices;
            PasswordChanged = new RelayCommand<PasswordBox>(getPassword);
            _username = string.Empty; //setting first value of username
            _password = string.Empty; //setting first value of password
            _username = "R467816"; //setting first value of username
            _enableMenu = false;
            _enableLogin = true;
            _isMenuSelected = false;
            _isLoginSelected = true;
            _visibilitySet = Visibility.Hidden;
          
        }

        #endregion 
    }
}
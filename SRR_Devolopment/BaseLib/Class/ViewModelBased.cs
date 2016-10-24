using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace SRR_Devolopment.BaseLib.Class
{
    /// <summary>
    /// Roland@2016
    /// </summary>
   public class ViewModelBased : ViewModelBase
    {

        #region "Properties"
       /// <summary>
       /// Filter Enabled
       /// </summary>
       private bool _objFilterEnabled;
       public bool ObjFilterEnabled
       {
           get
           {
               return _objFilterEnabled;
           }
           set
           {
               _objFilterEnabled = value;
               RaisePropertyChanged("ObjFilterEnabled");
           }
       }

       /// <summary>
       /// Obj Enabled
       /// </summary>
       private bool _objEnabled;
       public bool ObjEnabled
       {
           get
           {
               return _objEnabled;
           }
           set
           {
               _objEnabled = value;
               RaisePropertyChanged("ObjEnabled");
           }
       }

        /// <summary>
        /// Is New Properties
        /// </summary>
        private Boolean _isNew;
        public Boolean IsNew
        {
            get
            {
                return _isNew;
            }
            set
            {
                _isNew = value;
                RaisePropertyChanged("IsNew");
            }
        }

        /// <summary>
        /// Is Mod Properties
        /// </summary>
        private Boolean _isMod;
        public Boolean IsMod
        {
            get
            {
                return _isMod;
            }
            set
            {
                _isMod = value;
                RaisePropertyChanged("IsMod");
            }
        }

       /// <summary>
       /// Is Read Properties
       /// </summary>
       private Boolean _isRead;
       public Boolean IsRead
       {
           get
           {
               return _isRead;
           }
           set
           {
               _isRead = value;
               RaisePropertyChanged("IsRead");
           }
       }

       /// <summary>
       /// New Button Enabled Property
       /// </summary>
       private Boolean _enabledNew;
       public Boolean EnabledNew
       {
           get
           {
                return _enabledNew;
           }
           set 
           {
               _enabledNew = value;
               RaisePropertyChanged("EnabledNew");
           }
       }

       /// <summary>
       /// Modify Enabled Property
       /// </summary>
       private Boolean _enabledModify;
       public Boolean EnabledModify
       {
           get
           {
               return _enabledModify;
           }
           set
           {
               _enabledModify = value;
               RaisePropertyChanged("EnabledModify");
           }
       }

       /// <summary>
       /// Cancel Enabled Property
       /// </summary>
       private Boolean _enabledCancel;
       public Boolean EnabledCancel
       {
           get
           {
               return _enabledCancel;
           }
           set
           {
               _enabledCancel = value;
               RaisePropertyChanged("EnabledCancel");
           }
       }

       /// <summary>
       /// Save Property
       /// </summary>
       private Boolean _enabledSave;
       public Boolean EnabledSave
       {
           get
           {
               return _enabledSave;
           }
           set
           {
               _enabledSave = value;
               RaisePropertyChanged("EnabledSave");
           }
       }

       /// <summary>
       /// Process Property
       /// </summary>
       private Boolean _enabledProcess;
       public Boolean EnabledProcess
       {
           get
           {
               return _enabledProcess;
           }
           set
           {
               _enabledProcess = value;
               RaisePropertyChanged("EnabledProcess");
           }
       }

       /// <summary>
       /// Print Property
       /// </summary>
       private Boolean _enabledPrint;
       public Boolean EnabledPrint
       {
           get
           {
               return _enabledPrint;
           }
           set
           {
               _enabledPrint = value;
               RaisePropertyChanged("EnabledPrint");
           }
       }

       /// <summary>
       /// Export Property
       /// </summary>
       private Boolean _enabledExport;
       public Boolean EnabledExport
       {
           get
           {
               return _enabledExport;
           }
           set
           {
               _enabledExport = value;
               RaisePropertyChanged("EnabledExport");
           }
       }

       /// <summary>
       /// Import Property
       /// </summary>
       private Boolean _enabledImport;
       public Boolean EnabledImport
       {
           get
           {
               return _enabledImport;
           }
           set
           {
               _enabledImport = value;
               RaisePropertyChanged("EnabledImport");
           }
       }

       /// <summary>
       /// Generate Property
       /// </summary>
       private Boolean _enabledGenerate;
       public Boolean EnabledGenerate
       {
           get
           {
               return _enabledGenerate;
           }
           set
           {
               _enabledGenerate = value;
               RaisePropertyChanged("EnabledGenerate");
           }
       }


       /// <summary>
       /// Login Button
       /// </summary>
       private RelayCommand _loginBtn;
       public RelayCommand LoginBtn
       {
           get
           {
               if (_loginBtn == null)
               {
                   _loginBtn = new RelayCommand(LoginButton);
               }
               return _loginBtn;
           }
       }
       /// <summary>
       /// New Button
       /// </summary>
        private RelayCommand _buttonNew;
        public  RelayCommand ButtonNew
        {
            get { 
                    if(_buttonNew == null)
                    {
                        _buttonNew = new RelayCommand(newButton);
                        
                    }
                    return _buttonNew;
                }
        }
       /// <summary>
       /// Cancel Button
       /// </summary>
        private RelayCommand _buttonCancel;
        public RelayCommand ButtonCancel
        {
            get
            {
                if (_buttonCancel == null)
                {
                    _buttonCancel = new RelayCommand(cancelButton);

                }
                return _buttonCancel;
            }
        }
       /// <summary>
       /// Button Save
       /// </summary>
        private RelayCommand _buttonSave;
        public RelayCommand ButtonSave
        {
            get
            {
                if (_buttonSave == null)
                {
                    _buttonSave = new RelayCommand(saveButton);

                }
                return _buttonSave;
            }
        }
       /// <summary>
       /// Process
       /// </summary>
        private RelayCommand _buttonProcess;
        public RelayCommand ButtonProcess
        {
            get
            {
                if (_buttonProcess == null)
                {
                    _buttonProcess = new RelayCommand(processButton);

                }
                return _buttonProcess;
            }
        }

       /// <summary>
       /// Button Generate
       /// </summary>
        private RelayCommand _buttonGenerate;
        public RelayCommand ButtonGenerate
        {
            get
            {
                if (_buttonGenerate == null)
                {
                    _buttonGenerate = new RelayCommand(generateButton);

                }
                return _buttonGenerate;
            }
        }
       /// <summary>
       /// Import Button
       /// </summary>
        private RelayCommand _buttonImport;
        public RelayCommand ButtonImport
        {
            get
            {
                if (_buttonImport == null)
                {
                    _buttonImport = new RelayCommand(importButton);

                }
                return _buttonImport;
            }
        }

       /// <summary>
       /// Export Button
       /// </summary>
        private RelayCommand _buttonExport;
        public RelayCommand ButtonExport
        {
            get
            {
                if (_buttonExport == null)
                {
                    _buttonExport = new RelayCommand(exportButton);

                }
                return _buttonExport;
            }
        }
       /// <summary>
       /// Print Button
       /// </summary>
        private RelayCommand _buttonPrint;
        public RelayCommand ButtonPrint
        {
            get
            {
                if (_buttonPrint == null)
                {
                    _buttonPrint = new RelayCommand(printButton);

                }
                return _buttonPrint;
            }
        }

        private RelayCommand _buttonModify;
        public RelayCommand ButtonModify
        {
            get
            {
                if (_buttonModify == null)
                {
                    _buttonModify = new RelayCommand(modifyButton);

                }
                return _buttonModify;
            }
        }

        #endregion

        #region "Methods"

        
        

         public virtual void newButton()
        {
            
        }

         public virtual void LoginButton()
         {

         }
         public virtual void cancelButton()
         {

         }

         public virtual void saveButton()
         {

         }

         public virtual void processButton()
         {

         }

         public virtual void generateButton()
         {

         }

         public virtual void importButton()
         {

         }

         public virtual void exportButton()
         {

         }

         public virtual void printButton()
         {

         }

       public virtual void modifyButton()
         {

         }

       public void getFirstAccess(bool isRead,bool isWrite,bool isModify)
         {
           if(isWrite == true && isModify == true)
           {// write,modify and read meaning true
               isRead = true;
               IsNew = true;
               IsMod = true;
               EnabledNew = true;
               EnabledModify = false;
               EnabledCancel = false;
               EnabledSave = false;
               EnabledProcess = true;
               EnabledPrint = false;
               EnabledExport = false;
               EnabledImport = false;
               EnabledGenerate = true;

           }
           else if(isWrite == true && isModify == false)
           {//you can write but you cannot modify
               isRead = true;
               IsNew = true;
               IsMod = true;
               EnabledNew = true;
               EnabledModify = false;
               EnabledCancel = false;
               EnabledSave = false;
               EnabledProcess = true;
               EnabledPrint = false;
               EnabledExport = false;
               EnabledImport = false;
               EnabledGenerate = true;
           }
           else if(isWrite == false && isModify == true)
           {//you cannot mod but you can add
               isRead = true;
               IsNew = true;
               IsMod = true;
               EnabledNew = false;
               EnabledModify = false;
               EnabledCancel = false;
               EnabledSave = false;
               EnabledProcess = true;
               EnabledPrint = false;
               EnabledExport = false;
               EnabledImport = false;
               EnabledGenerate = true;
           }
           else if(isWrite == false && isModify == false)
           {//you can read
               isRead = true;
               IsNew = true;
               IsMod = true;
               EnabledNew = false;
               EnabledModify = false;
               EnabledCancel = false;
               EnabledSave = false;
               EnabledProcess = true;
               EnabledPrint = false;
               EnabledExport = false;
               EnabledImport = false;
               EnabledGenerate = true;
           }
           else if(isWrite == false && isModify == false && isRead == false)
           {//can not do anything, even to find data
               isRead = true;
               IsNew = true;
               IsMod = true;
               EnabledNew = false;
               EnabledModify = false;
               EnabledCancel = false;
               EnabledSave = false;
               EnabledProcess = false;
               EnabledPrint = false;
               EnabledExport = false;
               EnabledImport = false;
               EnabledGenerate = false;
           }
         }

        #endregion

    }
}

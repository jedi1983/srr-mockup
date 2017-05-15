using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using SRR_Devolopment.BaseLib.Class;
using System.Drawing;
using SRR_Devolopment.Model;
using System.Windows.Controls.Primitives;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Entity;



namespace SRR_Devolopment.BaseLib.Class
{
    public class TextBoxWithSearch : TextBox,INotifyPropertyChanged
    {

#region "Private CTOR"
        public TextBoxWithSearch()
        {
            FirstRun = true;
            OldValueText = string.Empty;
            GetEntityNameLink = string.Empty;
        }

#endregion
      
#region "Inotified Property Implementations"
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChangedTest(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #endregion

#region "Dependency Properties Setup"


        /// <summary>
        /// ItemsSource Binding
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }


        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(TextBoxWithSearch), new PropertyMetadata(new PropertyChangedCallback(OnItemsSourcePropertyChanged)));

        private static void OnItemsSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as TextBoxWithSearch;
            if (control != null)
                control.OnItemsSourceChanged((IEnumerable)e.OldValue, (IEnumerable)e.NewValue);
        }



        private void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            // Remove handler for oldValue.CollectionChanged
            var oldValueINotifyCollectionChanged = oldValue as INotifyCollectionChanged;

            if (null != oldValueINotifyCollectionChanged)
            {
                oldValueINotifyCollectionChanged.CollectionChanged -= new NotifyCollectionChangedEventHandler(newValueINotifyCollectionChanged_CollectionChanged);
            }
            // Add handler for newValue.CollectionChanged (if possible)
            var newValueINotifyCollectionChanged = newValue as INotifyCollectionChanged;
            if (null != newValueINotifyCollectionChanged)
            {
                newValueINotifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(newValueINotifyCollectionChanged_CollectionChanged);
            }

        }

        void newValueINotifyCollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Do your stuff here.
            //Not Implemented Yet
        }
        //end ItemsSource

        /// <summary>
        /// This Is To Set is This Is In Edit Mode
        /// </summary>
        public static readonly DependencyProperty GetIsEditingMode = DependencyProperty.Register("IsInEditMode",typeof(bool),typeof(TextBoxWithSearch), new PropertyMetadata(new PropertyChangedCallback(OnIsInEditModePropertyChanged)));
        public bool IsInEditMode
        {
            get
            {
                return (bool)GetValue(GetIsEditingMode);
            }
            set
            {
                SetValue(GetIsEditingMode, value);
            }
        }

         private static void OnIsInEditModePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as TextBoxWithSearch;
            if (control != null)
                control.OnIsInEditModeItemChanged((bool)e.OldValue, (bool)e.NewValue);
        }



        private void OnIsInEditModeItemChanged(Object oldValue, Object newValue)
        {
            // Remove handler for oldValue.CollectionChanged
            var oldValueINotifyCollectionChanged = oldValue as INotifyPropertyChanged;

            if (null != oldValueINotifyCollectionChanged)
            {
                oldValueINotifyCollectionChanged.PropertyChanged -= new PropertyChangedEventHandler(newValueINotifyCollectionChanged_IsInEditMode);
            }
            // Add handler for newValue.CollectionChanged (if possible)
            var newValueINotifyCollectionChanged = newValue as INotifyPropertyChanged;
            if (null != newValueINotifyCollectionChanged)
            {
                newValueINotifyCollectionChanged.PropertyChanged += new PropertyChangedEventHandler(newValueINotifyCollectionChanged_IsInEditMode);
            }

        }

        void newValueINotifyCollectionChanged_IsInEditMode(object sender, PropertyChangedEventArgs e)
        {
            //Do your stuff here.
            //Not Implemented Yet
        }

        /// <summary>
        /// Get Selected Item For Name
        /// </summary>
        public static readonly DependencyProperty GetSelectedItem = DependencyProperty.Register("GetSelectDropDown", typeof(Object), typeof(TextBoxWithSearch), new PropertyMetadata(new PropertyChangedCallback(OnGetSelectedItemPropertyChanged)));

        public Object GetSelectDropDown
        {
            get
            {
                return (Object)GetValue(GetSelectedItem);
            }
            set
            {
                SetValue(GetSelectedItem, value);
                //RaisePropertyChangedTest("GetSelectDropDown");
            }
        }

        private static void OnGetSelectedItemPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as TextBoxWithSearch;
            if (control != null)
                control.OnGetSelectedItemChanged((Object)e.OldValue, (Object)e.NewValue);
        }



        private void OnGetSelectedItemChanged(Object oldValue, Object newValue)
        {
            // Remove handler for oldValue.CollectionChanged
            var oldValueINotifyCollectionChanged = oldValue as INotifyPropertyChanged;

            if (null != oldValueINotifyCollectionChanged)
            {
                oldValueINotifyCollectionChanged.PropertyChanged -= new PropertyChangedEventHandler(newValueINotifyCollectionChanged_GetSelectedItem);
            }
            // Add handler for newValue.CollectionChanged (if possible)
            var newValueINotifyCollectionChanged = newValue as INotifyPropertyChanged;
            if (null != newValueINotifyCollectionChanged)
            {
                newValueINotifyCollectionChanged.PropertyChanged += new PropertyChangedEventHandler(newValueINotifyCollectionChanged_GetSelectedItem);
            }

        }

        void newValueINotifyCollectionChanged_GetSelectedItem(object sender, PropertyChangedEventArgs e)
        {
            //Do your stuff here.
            //Not Implemented Yet
        }

        #endregion

#region "Properties"

        //private property
        private Popup _getFilterPopUp = new Popup();
        private DataGrid _dataGridAsSource = new DataGrid();
        private DataGridTextColumn _dataColumnOne = new DataGridTextColumn();
        private DataGridTextColumn _dataColumnTwo = new DataGridTextColumn();

        //disposable
        bool disposed = false;

        /// <summary>
        /// Get Entity Name Link
        /// </summary>
        private string _getEntityNameLink;
        public string GetEntityNameLink
        {
            get
            {
                return _getEntityNameLink;
            }
            set
            {
                _getEntityNameLink = value;
                RaisePropertyChangedTest("GetEntityNameLink");
            }
        }

        /// <summary>
        /// Old Value Of Text
        /// </summary>
        private string _oldValueText;
        public string OldValueText
        {
            get
            {
                return _oldValueText;
            }
            set
            {
                _oldValueText = value;
                RaisePropertyChangedTest("OldValueText");
            }
        }

        /// <summary>
        /// First Run
        /// </summary>
        private bool _firstRun;
        public bool FirstRun
        {
            get
            {
                return _firstRun;
            }
            set
            {
                _firstRun = value;
                RaisePropertyChangedTest("FirstRun");
            }
        }

        public Popup GetFilterPopUp
        {
            get
            {
                return _getFilterPopUp;
            }
            set
            {
                _getFilterPopUp = value;
                RaisePropertyChangedTest("GetFilterPopUp");
            }
        }

        public DataGrid DataGridAsSource
        {
            get
            {
                return _dataGridAsSource;
            }
            set
            {
                _dataGridAsSource = value;
                RaisePropertyChangedTest("DataGridAsSource");
            }
        }

        /// <summary>
        /// Setting Up Max Width
        /// </summary>
        private int _maxPopUpWidth;
        public int MaxPopUpWidth
        {
            get
            {
                return _maxPopUpWidth;
            }
            set
            {
                _maxPopUpWidth = value;
                GetFilterPopUp.MaxWidth = MaxPopUpWidth;
                RaisePropertyChangedTest("MaxPopUpWidth");
            }
        }

        /// <summary>
        /// Setting Pop Up Max Height
        /// </summary>
        private int _maxPopUpHeight;
        public int MaxPopUpHeight
        {
            get
            {
                return _maxPopUpHeight;
            }
            set
            {
                _maxPopUpHeight = value;
                GetFilterPopUp.MaxHeight = _maxPopUpHeight;
                RaisePropertyChangedTest("MaxPopUpHeight");
            }
        }

        /// <summary>
        /// Setting Up First Column Name As Grid Data Column
        /// </summary>
        private string _setFirstColumnName;
        public string SetFirstColumnName
        {
            get
            {
                return _setFirstColumnName;
            }
            set
            {
                _setFirstColumnName = value;
                _dataColumnOne.Header = (_setFirstColumnName);
                RaisePropertyChangedTest("SetFirstColumnName");
            }
        }

        /// <summary>
        /// Setting Up Second Column Name As Grid Data Column
        /// </summary>
        private string _setSecondColumnName;
        public string SetSecondColumnName
        {
            get
            {
                return _setSecondColumnName;
            }
            set
            {
                _setSecondColumnName = value;
                _dataColumnTwo.Header = (_setSecondColumnName);
                RaisePropertyChangedTest("SetSecondColumnName");
            }
        }

        /// <summary>
        /// Setting Up Second Column Bind
        /// </summary>
        private string _setSecondColumnBind;
        public string SetSecondColumnBind
        {
            get
            {
                return _setSecondColumnBind;
            }
            set
            {
                _setSecondColumnBind = value;
                _dataColumnTwo.Binding = new Binding(_setSecondColumnBind);
                RaisePropertyChangedTest("SetSecondColumnBind");
            }
        }

        /// <summary>
        /// Setting Up First Column Bind
        /// </summary>
        private string _setFirstColumnBind;
        public string SetFirstColumnBind
        {
            get
            {
                return _setFirstColumnBind;
            }
            set
            {
                _setFirstColumnBind = value;
                _dataColumnOne.Binding = new Binding(_setFirstColumnBind);
                RaisePropertyChangedTest("SetFirstColumnBind");
            }
        }

#endregion

#region "Methods"
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            
            //check Whether it is in Edit Mode
            if (IsInEditMode == false)
            {
                if(GetFilterPopUp.IsOpen == true)
                    GetFilterPopUp.IsOpen = false;
                return;
            }
                    
            //checking state
            if (OldValueText == Text)
            {
                return;
            }
            
            else if(OldValueText != Text)
            {
                if (GetSelectDropDown != null)
                    GetSelectDropDown = null;//set null first
            }
                
           

            //checking first run
            if(FirstRun == true)
            {
                DataGridAsSource.Columns.Add(_dataColumnOne);
                DataGridAsSource.Columns.Add(_dataColumnTwo);
                DataGridAsSource.SelectionMode = DataGridSelectionMode.Single;
                DataGridAsSource.AutoGenerateColumns = false;
                DataGridAsSource.SelectionChanged += getData_SelectionChanged;
                FirstRun = false;
            }

            string dataTest = Text;
           

            Type cd = ItemsSource.GetType();
            var NameAssembly = cd.AssemblyQualifiedName;

            var GetNameModel = NameAssembly.Split();

            //find the Entity Types of IEnumerable
            if (GetNameModel[0].Contains("CGL_KP_M_Member_H"))
            {
                GetEntityNameLink = "CGL_KP_M_Member_H";
                DataGridAsSource.SelectedItem = null;
                ICollection<CGL_KP_M_Member_H> dataQuery = (ICollection<CGL_KP_M_Member_H>)ItemsSource;
                var linqToSql = from tbl in dataQuery where tbl.Name.ToLower().StartsWith(Text) select tbl;
                DataGridAsSource.ItemsSource = linqToSql;
                
            }


            if (IsFocused)
            {
                GetFilterPopUp.Child = DataGridAsSource;
                GetFilterPopUp.Placement = PlacementMode.Right;
                GetFilterPopUp.PlacementTarget = this;
                GetFilterPopUp.IsOpen = true;
            }
            

            //check data
            
            //if (!IsFocused)
            //{
            //    // Do stuff here
            //}

        }

        //protected override void OnLostFocus(RoutedEventArgs e)
        //{
        //    //if (GetFilterPopUp.IsOpen == true)
        //    //    GetFilterPopUp.IsOpen = false;

        //}

        //protected override void OnGotFocus(RoutedEventArgs e)
        //{

        //}

       

        private void getData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid _dataFromSearch = (DataGrid)sender;

            GetSelectDropDown = _dataFromSearch.SelectedItem;
            if (GetFilterPopUp.IsOpen == true)
            {
                GetFilterPopUp.IsOpen = false;

                //specific for Member
                if (GetEntityNameLink.Contains("CGL_KP_M_Member_H") && GetSelectDropDown != null)
                {
                    CGL_KP_M_Member_H testData = (CGL_KP_M_Member_H)GetSelectDropDown;
                    OldValueText = testData.Name;
                    Text = testData.Name;
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
                if (GetFilterPopUp.IsOpen == true)
                    GetFilterPopUp.IsOpen = false;

                //cleans Up Selected Items
                GetSelectDropDown = null;

                //CleansUp
                ItemsSource = null;
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }
        
     
       
#endregion

    }
}

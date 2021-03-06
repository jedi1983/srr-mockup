﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SRR_Devolopment.BaseLib;
using System.Windows.Media;
using System.Windows.Shapes;


namespace SRR_Devolopment.BaseLib.Class
{
    /// <summary>
    /// Closeable Tab Inherited from TabItem Class and Interfaced of InotifyPropertyChanged(Will Be Implemented if there is another function) @Roland 2016
    /// </summary>
    public class CloseableTab : TabItem ,INotifyPropertyChanged,IDisposable

    {

        
        //Disposable Implementation
        //disposable
        bool disposed = false;

        //event helper
        public event PropertyChangedEventHandler PropertyChanged;

        //public property Closed Button
        private string _closedForm;
        public string ClosedForm
        {
            get
            {
                return _closedForm;
            }
            set
            {
                if(value != _closedForm)
                {
                    _closedForm = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //Public Property
        private IList<Grid_Data_Add_Remove> _addedData;
        public IList<Grid_Data_Add_Remove> AddedData
        {
            get
            {
                return _addedData;
            }
            set
            {
                if(value != _addedData)
                {
                    _addedData = value;
                    NotifyPropertyChanged();
                }
                
            }
        }

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void SetHeader(UIElement header)
        {
            
            var dockPanel = new StackPanel();
            dockPanel.Orientation = Orientation.Horizontal;
            dockPanel.Children.Add(header);
            TextBlock dd = (TextBlock)header;
          
            //start Geometrical Shape
            
            // line 1
            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(1, 9);
            //line 2
            PathFigure myPathFigure2 = new PathFigure();
            myPathFigure2.StartPoint = new Point(1, 1);

            //segment 1
            LineSegment myLineSegment = new LineSegment();
            myLineSegment.Point = new Point(9, 1);

            //segment 2
            LineSegment myLineSegment2 = new LineSegment();
            myLineSegment2.Point = new Point(9, 9);

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(myLineSegment);

            PathSegmentCollection myPathSegmentCOllection2 = new PathSegmentCollection();
            myPathSegmentCOllection2.Add(myLineSegment2);

            myPathFigure.Segments = myPathSegmentCollection;
            myPathFigure2.Segments = myPathSegmentCOllection2;

            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);
            myPathFigureCollection.Add(myPathFigure2);

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            Path myPath = new Path();
            myPath.Stroke = Brushes.Black;
            myPath.StrokeThickness = 2;
            myPath.Data = myPathGeometry;
            
            //end

            DataButton = new Button();
            dd.Height = DataButton.Height;//
            DataButton.Content = myPath;//
            dockPanel.Children.Add(DataButton);
            
            // Set the header
            Header = dockPanel;
        }


        private Button _dataButton;
        public Button DataButton
        {
            get
            {
                return _dataButton;
            }
            set
            {
                _dataButton = value;
                NotifyPropertyChanged();
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
                UserControl _controlGet = (UserControl)this.Content;
                TextBoxWithSearch _txtBoxSearch = (TextBoxWithSearch)_controlGet.FindName("txWithSearchBoxComp");

                if (_txtBoxSearch != null)
                    _txtBoxSearch.Dispose();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

    }
}

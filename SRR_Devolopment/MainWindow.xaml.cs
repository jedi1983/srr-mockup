using System.Windows;
using SRR_Devolopment.ViewModel;
using System.Reflection;
using System;//add
using System.Windows.Controls;
using SRR_Devolopment.Views;
using SRR_Devolopment.BaseLib.Class;
using SRR_Devolopment.BaseLib.UserControl;

namespace SRR_Devolopment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //Closing += (s, e) => ViewModelLocator.Cleanup();
            //var p = System.Windows.Application.LoadComponent(new Uri("/SRR_Devolopment;component/Views/ucLoginPage.xaml", UriKind.RelativeOrAbsolute));
            ////TestContent.Content = p;
            //groupBoxMenu.Content = p;
        }

        private void OnPlusTabClick(object sender, RoutedEventArgs e)
        {
            //var p = System.Windows.Application.LoadComponent(new Uri("/SRR_Devolopment;component/Views/ucLoginPage.xaml", UriKind.RelativeOrAbsolute));
            var p = System.Windows.Application.LoadComponent(new Uri("/SRR_Devolopment;component/Views/ucPageOne.xaml", UriKind.RelativeOrAbsolute));
            // Create the header
            var header = new TextBlock { Text = "Tab!  " };

            // Create the content
            //var content = new TextBlock
            //{
            //    Text = string.Format("Tab numero {0}-o",
            //        uxTabs.Items.Count + 1)
            //};

            //// Create the tab
            //var tab = new CloseableTab();
            //tab.SetHeader(header);
            //tab.Content = p;
            //// Add to TabControl
            //uxTabs.Items.Add(tab);

        }
    }
}
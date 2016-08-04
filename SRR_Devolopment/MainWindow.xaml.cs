using System.Windows;
using SRR_Devolopment.ViewModel;
//using SRR_Devolopment.Views;

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
            Closing += (s, e) => ViewModelLocator.Cleanup();

           // TestContent.Content = new Views.ucLoginPage();
        }
    }
}
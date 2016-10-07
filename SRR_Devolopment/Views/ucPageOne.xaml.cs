using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SRR_Devolopment.Views
{
    /// <summary>
    /// Interaction logic for ucPageOne.xaml
    /// </summary>
    public partial class ucPageOne : UserControl
    {
        public ucPageOne()
        {
            
            InitializeComponent();
            StackPanel thisIsPanel = new StackPanel();
            StackPanel thisIsPanel2 = new StackPanel();
            StackPanel thisIsPanel3 = new StackPanel();
            Button test1 = new Button();
            Button test2 = new Button();
            Button test3 = new Button();
            Button test4 = new Button();
            test1.Content = "satu";
            test2.Content = "Dua";
            test3.Content = "satu";
            test4.Content = "Dua";
            thisIsPanel.Children.Add(test1);
            thisIsPanel.Children.Add(test2);
            thisIsPanel2.Children.Add(test3);
            thisIsPanel2.Children.Add(test4);
            Expander newTest = new Expander();
            Expander newTest2 = new Expander();
            newTest.Header = "Menu Test";
            newTest.Content = thisIsPanel;
            newTest2.Header = "Menu Dua";
            newTest2.Content = thisIsPanel2;
            thisIsPanel3.Children.Add(newTest);
            thisIsPanel3.Children.Add(newTest2);
            xTest.Content = thisIsPanel3;
            
            
        }
    }
}

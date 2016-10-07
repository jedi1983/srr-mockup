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
    /// Interaction logic for ucTreeListMenu.xaml
    /// </summary>
    public partial class ucTreeListMenu : UserControl
    {
        public ucTreeListMenu()
        {


            TreeView testingOne = new TreeView();
            InitializeComponent();
            TreeViewItem xx = new TreeViewItem();
            TreeViewItem xa = new TreeViewItem();
            //xa.MouseLeftButtonUp += toolStripClick;
            xa.Header = "sub Menu Test";
            xx.Header = "Test Code";
            xx.Items.Add(xa);
            testingOne.Items.Add(xx);
           // testingOne.SelectedItemChanged += toolStripClick;
        }

        //public void toolStripClick(object sender, System.EventArgs e)
        //{
        //    //TreeViewItem aa = new TreeViewItem();
        //    //aa = (TreeViewItem)sender;
        //    TreeView aa = new TreeView();
        //    aa = (TreeView)sender;
        //    TreeViewItem bb = new TreeViewItem();
        //    bb = (TreeViewItem)aa.SelectedItem;
        //    bool cc = false; 
        //    cc=bb.IsDescendantOf(testingOne);
        //    if(cc == true)
        //        MessageBox.Show("is Descendant ");
        //    string test = (string)bb.Header;
        //    //test = "TEST";
        //    MessageBox.Show("Menu Selected " + test);
        //}
    }
}

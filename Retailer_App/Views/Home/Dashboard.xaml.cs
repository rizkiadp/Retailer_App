using Retailer_App.ViewModels;
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
using System.Windows.Shapes;

namespace Retailer_App.Views.Home
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void MenuUsers_Click(object sender, RoutedEventArgs e)
        {
            PnlContect.Children.Clear();
            PnlContect.Children.Add (new UserViewModels());
        }

        private void MenuInventory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuStock_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NameProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

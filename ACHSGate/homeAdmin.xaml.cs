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

namespace ACHSGate
{
    /// <summary>
    /// Interaction logic for homeAdmin.xaml
    /// </summary>
    public partial class homeAdmin : Window
    {
        public homeAdmin()
        {
            InitializeComponent();
        }

        private void resigter_click(object sender, RoutedEventArgs e)
        {
            registerVehicle vehi = new registerVehicle();
            vehi.Show();
        }

        private void costAdding_Click(object sender, RoutedEventArgs e)
        {
            costAdding cost = new costAdding();
            cost.ShowDialog();
        }

        private void calculator_Click(object sender, RoutedEventArgs e)
        {
            paymentCalc cal = new paymentCalc();
            cal.ShowDialog();
        }

        private void brandAdd_Click(object sender, RoutedEventArgs e)
        {
            commonInfo common = new commonInfo();
            common.ShowDialog();
        }

        private void sellVehicle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void viewCost_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sign_off_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.ShowDialog();
        }

        private void costAdd_Click(object sender, RoutedEventArgs e)
        {
            costAdding cost = new costAdding();
            cost.ShowDialog();
        }
    }
}

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
    /// Interaction logic for paymentCalc.xaml
    /// </summary>
    public partial class paymentCalc : Window
    {
        public paymentCalc()
        {
            InitializeComponent();
        }

        private void cal_Click(object sender, RoutedEventArgs e)
        {
            double loanAmount =Convert.ToDouble(txtLoanAmount.Text);
            double percentage = Convert.ToDouble(cmbRate.Text);
            double month = Convert.ToDouble(cmbMonths.Text);

            double intrate = (loanAmount * percentage) / 100;
            double monthlyFee = loanAmount / month;

            double total = Math.Round(intrate + monthlyFee, 2);
            txtperMonth.Text = total.ToString();
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            clearDetails();
        }

        public void clearDetails()
        {
            txtLoanAmount.Text = "";
            cmbRate.Text = "";
            cmbMonths.Text = "";
            txtperMonth.Text = "";
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

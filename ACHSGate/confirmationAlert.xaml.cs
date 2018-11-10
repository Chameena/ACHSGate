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
    /// Interaction logic for cofirmationAlert.xaml
    /// </summary>
    public partial class confirmationAlert : Window
    {
        private bool response;
        public confirmationAlert(string message)
        {
            InitializeComponent();
            alertText.Text = message;
            response = false;
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            response = false;
            this.Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            response = true;
            this.Close();
        }

        public bool GetConfirmation()
        {
            this.ShowDialog();
            return response;
        }
    }
}

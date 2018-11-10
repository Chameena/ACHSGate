using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace ACHSGate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            username.Focus();
        }
        public string user;
        public string pass;
       // public string today=  DateTime.Now.ToString("MM/dd/yyyy");
        DateTime today = DateTime.Today;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!password.Password.Equals("") && !username.Text.Equals(""))
            {
              UserLogin();
            }
         }

        public void UserLogin()
        {
            user = username.Text;
            pass = password.Password;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from [dbo].[user] where username='" + user + "' and password= '" + pass + "' and actdate <='"+today+"' ", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                homeAdmin home = new homeAdmin();
                this.Close();
                home.ShowDialog();
                
            }
            else
            {
                alert alert = new alert("Login Failed");
                alert.ShowDialog();
            }
            con.Close();
        }
        private void username_KeyUp(object sender, KeyEventArgs e)
        {
            if (!username.Text.Equals(""))
            {
                if (e.Key == Key.Enter)
                {
                    password.Focus();
                }
            }
        }

        private void password_KeyUp(object sender, KeyEventArgs e)
        {
            if (!password.Password.Equals("") && !username.Text.Equals(""))
            {
                if (e.Key == Key.Enter)
                {
                    UserLogin();
                }
            }
        }
    }
}

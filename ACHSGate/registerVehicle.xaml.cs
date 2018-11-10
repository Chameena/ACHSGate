using ACHSGate.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for registerVehicle.xaml
    /// </summary>
    public partial class registerVehicle : Window
    {
        public registerVehicle()
        {
            InitializeComponent();
            insuranceLbl.Content = "Insurance \r\nValid Till :";
            revenuelbl.Content = "Revenue License \r\nValid Till :";
            dpDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            cmbType.SelectedIndex = 0;

        }

        public string type;
        public void loadBrandCmb()
        {

            ComboBoxItem item = (ComboBoxItem)cmbType.SelectedItem;
            type = item.Content.ToString();

            List<brand> brandList = list.Brand(0, type);
            cmbBrand.ItemsSource = brandList;
            cmbBrand.DisplayMemberPath = "name";
            cmbBrand.SelectedValuePath = "brandKey";
        }

        public void loadNameCmb()
        {

            ComboBoxItem item = (ComboBoxItem)cmbType.SelectedItem;
            type = item.Content.ToString();

            List<brand> nameList = list.Brand(Convert.ToInt32(cmbBrand.SelectedValue), type);
            cmbName.ItemsSource = nameList;
            cmbName.DisplayMemberPath = "name";
            cmbName.SelectedValuePath = "name";
        }

        private void save_click(object sender, RoutedEventArgs e)
        { 
            if (txtvehiNo.Text == "" || txtengNo.Text == "" || txtchassiNo.Text == "" || cmbType.Text == "" || cmbBrand.Text=="" || cmbName.Text == "" || txtiniprice.Text == "" || txtremarks.Text == "" || dpInsuranceDate.Text == "" || dpRevenueDate.Text == "" || txtfullname.Text == "" || txtnic.Text == "" || txttpNo.Text == "" || dpDate.Text == "" || txtaddress.Text == "")
            {
                alert alert = new alert("Fill all the details!");
                alert.ShowDialog();
            }
            else {

                string vehicleNo = txtvehiNo.Text;
                string engNo = txtengNo.Text;
                string chassiNo = txtchassiNo.Text;
                string type = cmbType.Text;
                string brand = cmbBrand.Text;
                string name = cmbName.Text;
                float price = (float)Convert.ToDouble(txtiniprice.Text);
                string remarks = txtremarks.Text;

                //string revenueDateOld = dpRevenueDate.SelectedDate.ToString();
                //string[] Rdate = revenueDateOld.Split(' ');
                //string revenueDate = Rdate[0];
                string revenueDate = dpRevenueDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                //DateTime.ParseExact(dpRevenueDate.Text, "yyyy/MM/dd", CultureInfo.CreateSpecificCulture("en-UK"));

                //string insuranceDateOld = dpInsuranceDate.SelectedDate.ToString();
                //string[] Idate = insuranceDateOld.Split(' ');
                //string insuranceDate = Idate[0];
                string insuranceDate = dpInsuranceDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                //DateTime.ParseExact(dpInsuranceDate.Text, "yyyy/MM/dd", CultureInfo.CreateSpecificCulture("en-UK"));


                string fullname = txtfullname.Text;
                string nic = txtnic.Text;
                int tpNo = Convert.ToInt32(txttpNo.Text);
                //string dateOld = dpDate.SelectedDate.ToString();
                // string[] Ddate = dateOld.Split(' ');
                // string date = Ddate[0];
                string date = dpDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                //DateTime.ParseExact(dpDate.Text, "yyyy/MM/dd", CultureInfo.CreateSpecificCulture("en-UK"));

                string testDate = "";

                string address = txtaddress.Text;

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[vehicle] ([vehicleNo],[engineNo],[chassiNo],[type],[brand],[name],[initialPrice],[insuranceDate],[revenueDate]) VALUES ('" + vehicleNo + "','" + engNo + "','" + chassiNo + "','" + type + "','" + brand + "','" + name + "', '" + price + "', '" + insuranceDate + "', '" + revenueDate + "') ", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[owner] ([vehicleNo],[preName],[preAddress],[preNic],[preTpno],[buyDate],[sellDate],[nextName] ,[nextAddress],[nextNic] ,[nextTpno]) VALUES ('" + vehicleNo + "','" + fullname + "','" + address + "','" + nic + "','" + tpNo + "','" + date + "','" + testDate + "','null','null','null',0)", con);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    alert alert = new alert("Added Successfully");
                    alert.ShowDialog();
                    clearFields();
                }
                catch (Exception ex)
                {
                    alert alert = new alert("Adding Failed");
                    alert.ShowDialog();
                }
               }
        }

        private void cmbBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadNameCmb();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            clearFields();
        }

        private void clearFields()
        {
            txtvehiNo.Text = "";
            txtengNo.Text = "";
            txtchassiNo.Text = "";
            cmbType.SelectedIndex = 0;
            cmbBrand.ItemsSource = null;
            cmbName.ItemsSource = null;
            txtiniprice.Text = "";
            txtremarks.Text = "";
            dpInsuranceDate.Text = "";
            dpRevenueDate.Text = "";

            txtfullname.Text = "";
            txtnic.Text = "";
            txttpNo.Text = "";
            dpDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtaddress.Text = "";
        }

        private void txtiniprice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9.]+");
        }

        private void txttpNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9.]+");
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadBrandCmb();
        }
    }
}

using ACHSGate.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
    /// Interaction logic for registerVehicle.xaml
    /// </summary>
    public partial class registerVehicle : Window
    {
        public registerVehicle()
        {
            InitializeComponent();
            insuranceLbl.Content = "Insurance \r\nValid Till :";
            revenuelbl.Content = "Revenue License \r\nValid Till :";

            loadBrandCmb();
        }

        public void loadBrandCmb()
        {
            List<brand> brandList = list.Brand(0);
            cmbBrand.ItemsSource = brandList;
            cmbBrand.DisplayMemberPath = "name";
            cmbBrand.SelectedValuePath = "brandKey";
        }

        public void loadNameCmb()
        {
            List<brand> nameList = list.Brand(Convert.ToInt32(cmbBrand.SelectedValue));
            cmbName.ItemsSource = nameList;
            cmbName.DisplayMemberPath = "name";
            cmbName.SelectedValuePath = "name";
        }

        private void save_click(object sender, RoutedEventArgs e)
        {
            string vehicleNo = txtvehiNo.Text;
            string engNo = txtengNo.Text;
            string chassiNo = txtchassiNo.Text;
            string type = cmbType.Text;
            string brand = cmbBrand.Text;
            string name = cmbName.Text;
            float price = (float)Convert.ToDouble(txtiniprice.Text);
            string remarks = txtremarks.Text;
            DateTime revenueDate = DateTime.ParseExact(dpRevenueDate.Text, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-UK"));
            
            DateTime insuranceDate = DateTime.ParseExact(dpInsuranceDate.Text, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-UK"));

            
            string fullname = txtfullname.Text;
            string nic = txtnic.Text;
            int tpNo = Convert.ToInt32(txttpNo.Text) ;
            DateTime date = DateTime.ParseExact(dpDate.Text, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-UK"));
            

            string address = txtaddress.Text;

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[vehicle] ([vehicleNo],[engineNo],[chassiNo],[type],[brand],[name],[initialPrice],[insuranceDate],[revenueDate]) VALUES ('"+vehicleNo+"','"+engNo+"','"+chassiNo+"','"+type+"','"+brand+"','"+name+"', '"+price+"', '"+ revenueDate+ "', '"+ insuranceDate+ "') ", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[owner] ([vehicleNo],[preName],[preAddress],[preNic],[preTpno],[buyDate],[sellDate],[nextName] ,[nextAddress],[nextNic] ,[nextTpno]) VALUES ('" + vehicleNo + "','" + fullname + "','" + address + "','" + nic + "','" + tpNo + "','" + date + "','N/A','N/A','N/A','N/A','N/A')", con);
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
            cmbType.Text = "";
            cmbBrand.Text = "";
            cmbName.Text = "";
            txtiniprice.Text = "";
            txtremarks.Text = "";

            txtfullname.Text = "";
            txtnic.Text = "";
            txttpNo.Text = "";
            dpDate.Text = "";
            txtaddress.Text = "";
        }
    }
}

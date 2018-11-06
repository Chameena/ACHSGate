using ACHSGate.Class;
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
using System.Windows.Shapes;

namespace ACHSGate
{
    /// <summary>
    /// Interaction logic for commonInfo.xaml
    /// </summary>
    public partial class commonInfo : Window
    {
        public commonInfo()
        {
            InitializeComponent();
        }

        public  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString);

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadBrandCmb();
        }

        public string type;
        public string brand;
        public void loadBrandCmb()
        {
            ComboBoxItem item = (ComboBoxItem)cmbType.SelectedItem;
            type = item.Content.ToString();

            List<brand> brandList = list.Brand(0, type);
            cmbBrand.ItemsSource = brandList;
            cmbBrand.DisplayMemberPath = "name";
            cmbBrand.SelectedValuePath = "brandKey";
        }

        private void cmbBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadNameCmb();
        }
        public void loadNameCmb()
        {
            List<brand> nameList = list.Brand(Convert.ToInt32(cmbBrand.SelectedValue), type);
            cmbName.ItemsSource = nameList;
            cmbName.DisplayMemberPath = "name";
            cmbName.SelectedValuePath = "name";
        }
        public int oldBrandKey;
        public int NewBrandKey;
       public  int parentKey;



        private void AddBrand_Click(object sender, RoutedEventArgs e)
        {
            if (cmbType.Text=="" || txtBrand.Text == string.Empty)
            {
                alert alert1 = new alert("Both Type and Brand are required!");
                alert1.ShowDialog();
            }
            else
            {
                ComboBoxItem item = (ComboBoxItem)cmbType.SelectedItem;
                type = item.Content.ToString();

                con.Open();
                SqlCommand cmd = new SqlCommand("select MAX(brandKey) from brandComboLoad", con);
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        oldBrandKey = Convert.ToInt32(reader[0]);
                        NewBrandKey = oldBrandKey + 1;
                    }
                }

                string newBrand = txtBrand.Text;

                SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[brandComboLoad] ([parentKey],[brandKey],[name],[type]) VALUES ('0','" + NewBrandKey + "','" + newBrand + "','" + type + "')", con);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();
                con.Close();

                alert alert = new alert("Added Successfully");
                alert.ShowDialog();
                txtBrand.Text = "";
            }
        }

        private void AddName_Click(object sender, RoutedEventArgs e)
        {
            if (cmbType.Text == "" || cmbBrand.Text == "" || txtName.Text == string.Empty)
            {
                alert alert1 = new alert("Type, Brand and Name are Required! ");
                alert1.ShowDialog();
            }
            else
            {
                brand = cmbBrand.Text;

                ComboBoxItem item1 = (ComboBoxItem)cmbType.SelectedItem;
                type = item1.Content.ToString();

                con.Open();
                SqlCommand cmd = new SqlCommand("select brandKey from brandComboLoad where name='" + brand + "' and parentKey=0 and type='" + type + "'", con);
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        parentKey = Convert.ToInt32(reader[0]);
                    }
                }
                string name = txtName.Text;

                SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[brandComboLoad] ([parentKey],[brandKey],[name],[type]) VALUES ('" + parentKey + "','0','" + name + "','" + type + "')", con);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();
                con.Close();

                alert alert = new alert("Added Successfully");
                alert.ShowDialog();
                txtName.Text = "";
            }
           }
        }
}

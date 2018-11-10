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
    /// Interaction logic for viewVehicleInfo.xaml
    /// </summary>
    public partial class viewVehicleInfo : Window
    {
        public viewVehicleInfo()
        {
            InitializeComponent();
            revLbl.Content = "Revenue License \r\nValid Till :";
            insuLbl.Content = " Insurance \r\nValid :";
            loadVehiclNo();
        }

        private void viewcost_Click(object sender, RoutedEventArgs e)
        {
            costAdding cost = new costAdding(cmbVehiNo.Text);;
            cost.ShowDialog();
        }

        public void loadVehiclNo()
        {
            List<vehicle> nameList = list.vehicle();
            cmbVehiNo.ItemsSource = nameList;
            cmbVehiNo.DisplayMemberPath = "vehicleNo";
            cmbVehiNo.SelectedValuePath = "vehicleNo";
        }

        public void loadInformation()
        {
            vehicle vehicleInfo = new vehicle();
            vehicleInfo = list.vehicleInformation(cmbVehiNo.SelectedValue.ToString());

            txtname.Text = vehicleInfo.name;
            txtBrand.Text = vehicleInfo.brand;
            txteng.Text = vehicleInfo.engNo;
            txtchassy.Text = vehicleInfo.chassiNo;
            txtType.Text = vehicleInfo.type;
            txtPrice.Text = vehicleInfo.price.ToString();
            txtownernic.Text = vehicleInfo.nic;
            txtownerphone.Text = vehicleInfo.tpNo.ToString();
            txtownerAddress.Text = vehicleInfo.preAddress;
           txtownername.Text = vehicleInfo.preName;

            string revenueDateOld = vehicleInfo.revDate; 
            string[] Rdate = revenueDateOld.Split(' ');
            txtrevenue.Text = Rdate[0];

            string buyDateOld = vehicleInfo.buyDate;
            string[] Bdate = revenueDateOld.Split(' ');
            txtbuyDate.Text = Bdate[0];

            string insuDateOld = vehicleInfo.insuDate;
            string[] Idate = revenueDateOld.Split(' ');;
            txtinsu.Text = Idate[0];

            calculateCost();
            totalCost = costFromDB + vehicleInfo.price;
            txtTotal.Text = totalCost.ToString();
        }

        public int costFromDB;
        public int totalCost;

        public void calculateCost()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select sum(costPrice) from cost where vehicleNo='"+ cmbVehiNo.SelectedValue.ToString() + "'", con);
            cmd.CommandType = CommandType.Text;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    costFromDB = Convert.ToInt32(reader[0]);
                   
                }
            }

            con.Close();
        }

        private void cmbVehiNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadInformation();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

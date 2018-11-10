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
    /// Interaction logic for sellVehicle.xaml
    /// </summary>
    public partial class sellVehicle : Window
    {
        public sellVehicle(string vehicleNo)
        {
            InitializeComponent();
            sellingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            if (vehicleNo != null)
            {
                loadVehiclNo();
                cmbReg.Text = vehicleNo;
            }
            else
            {
                loadVehiclNo();
            }
        }

        public void loadVehiclNo()
        {
            List<vehicle> nameList = list.vehicle();
            cmbReg.ItemsSource = nameList;
            cmbReg.DisplayMemberPath = "vehicleNo";
            cmbReg.SelectedValuePath = "vehicleNo";
        }

        private void viewcost_Click(object sender, RoutedEventArgs e)
        {
            costAdding cost = new costAdding(cmbReg.SelectedValue.ToString());
            cost.ShowDialog();
        }

        public int costFromDB;
        public int totalCost;
        public void calculateCost()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select sum(costPrice) from cost where vehicleNo='" + cmbReg.SelectedValue.ToString() + "'", con);
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

        private void cmbReg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtsellingprice1.Text = "";
            cmbRate.Text = "";
            loadCostText();
        }

        public void loadCostText()
        {
            vehicle vehicleInfo = new vehicle();
            vehicleInfo = list.vehicleInformation(cmbReg.SelectedValue.ToString());
            
            calculateCost();
            totalCost = costFromDB + vehicleInfo.price;
            txttotal.Text = totalCost.ToString();
        }

        private void cal_Click(object sender, RoutedEventArgs e)
        {

            ComboBoxItem item = (ComboBoxItem)cmbRate.SelectedItem;
            decimal rate = Convert.ToDecimal(item.Content);

            decimal amount = Convert.ToDecimal( txttotal.Text);
          
            decimal costAdd = (amount * rate) / 100;
            decimal totalPrice = amount + costAdd;

            txtsellingprice1.Text = totalPrice.ToString();

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = txtnewname.Text;
            string nic = txtnewnic.Text;
            string address = txtnewaddress.Text;
            int tpNo = Convert.ToInt32(txtnewphone.Text);
            string sellDate = sellingDate.SelectedDate.Value.ToString("yyyy-MM-dd");
            string vehicNo = cmbReg.SelectedValue.ToString();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("update owner set sellDate='"+sellDate+ "', nextName='" + name + "',nextAddress='" + address + "',nextNic='" + nic + "',nextTpno='" + tpNo + "' where vehicleNo='" + vehicNo + "' ", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();

                alert alert = new alert("Added Successfully");
                alert.ShowDialog();
                clearDetails();
            }
            catch (Exception ex)
            {
                alert alert = new alert("Adding Failed");
                alert.ShowDialog();
            }
        }
        public void clearDetails()
        {
            txtnewname.Text ="";
            txtnewnic.Text = "";
            txtnewaddress.Text = "";
            txtnewphone.Text = "";
            sellingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            
        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

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
    /// Interaction logic for costAdding.xaml
    /// </summary>
    public partial class costAdding : Window
    {
        public costAdding()
        {
            InitializeComponent();
            loadVehicleNoCmb();
        }
      public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString);

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            foreach (object itm in costGrid.SelectedItems)
            {
                con.Open();
                cost item = (cost)itm;
                SqlCommand cmd = new SqlCommand("Delete from [dbo].[cost] where costId = '"+item.costId+"' ", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
             }
            loadDetailsGrid();
        }

        public void loadDetailsGrid()
        {
            List<cost> cost = list.cost(cmbvehiNo.SelectedValue.ToString());
            costGrid.ItemsSource = cost;
        }

        public void loadVehicleNoCmb()
        {
            List<vehicle> nameList = list.vehicle();
            cmbvehiNo.ItemsSource = nameList;
            cmbvehiNo.DisplayMemberPath = "vehicleNo";
            cmbvehiNo.SelectedValuePath = "vehicleNo";
        }

        public int OldcostId;
        public int NewcostId;

        private void add_Click(object sender, RoutedEventArgs e)
        {
            string vehicleNo = cmbvehiNo.Text;
            string desc = txtdesc.Text;
            decimal value = Math.Round(Convert.ToDecimal(txtvalue.Text),2);
            string date = dpDate.SelectedDate.Value.ToString("yyyy-MM-dd"); 
            try
            {
                 con.Open();

                SqlCommand cmd1 = new SqlCommand("select MAX(costId) from cost", con);
                cmd1.CommandType = CommandType.Text;
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        OldcostId = Convert.ToInt32(reader[0]);
                        NewcostId = OldcostId + 1;
                    }
                }
                
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[cost] ([vehicleNo],[date],[costType],[costPrice]) VALUES ('" + vehicleNo + "','" + date + "','" + desc + "','" + value + "')", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                loadDetailsGrid();
            }

            catch (Exception ex)
            {
                alert alert = new alert("Adding Failed");
                alert.ShowDialog();
            }
        }

        private void cmbvehiNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbvehiNo.Text == "")
            {

            }
            else
            {
                loadDetailsGrid();
            }
           
        }

        public void calculateTotal()
        {
            //decimal sum=0;
            //for (int i = 0; i < costGrid.Items.Count; ++i)
            //{
            //    sum += (decimal.Parse((costGrid.Columns[2].GetCellContent(costGrid.Items[i]) as TextBox).Text));
            //}
            List<cost> cost = list.cost(cmbvehiNo.SelectedValue.ToString());
            decimal sum = 0;
            foreach (cost p in cost)
            {
                sum += p.value;
            }
            txtTotal.Text = sum.ToString();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void clearDetails()
        {
            cmbvehiNo.Text ="";
            txtdesc.Text = "";
            txtvalue.Text = "";
            dpDate.Text = "";
            costGrid.ItemsSource = "";
            txtTotal.Text = "";
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            clearDetails();
        }

        private void costGrid_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            
        }

        private void costGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            calculateTotal();
        }
    }
}

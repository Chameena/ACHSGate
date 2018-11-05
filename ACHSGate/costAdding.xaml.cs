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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

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

        private void add_Click(object sender, RoutedEventArgs e)
        {
            string vehicleNo = cmbvehiNo.Text;
            string desc = txtdesc.Text;
            decimal value = Math.Round(Convert.ToDecimal(txtvalue.Text),2);
            DateTime date = Convert.ToDateTime(dpDate.Text);
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[cost] ([vehicleNo],[date],[costType],[costPrice]) VALUES ('" + vehicleNo + "','" + date + "','" + desc + "','" + value + "'", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                DataView dv = costGrid.ItemsSource as DataView;
                DataTable dt = dv.Table;
                DataRow dr = dt.NewRow();

                dr["costId"] = "";
                dr["desc"] = desc;
                dr["value"] = value;
                dt.Rows.Add(dr);
            }

            catch (Exception ex)
            {
                alert alert = new alert("Adding Failed");
                alert.ShowDialog();
            }
        }

        private void cmbvehiNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadDetailsGrid();
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

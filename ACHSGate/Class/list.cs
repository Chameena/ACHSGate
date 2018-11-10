using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSGate.Class
{
  public  class list
    {
        public static List<brand> Brand(int parentKey,string type)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from [dbo].[brandComboLoad] where parentKey = '"+ parentKey + "' and type ='" + type + "' ", con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            List<brand> cmp = new List<brand>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                 brand obj =  new brand();
                  
                    if (dr["parentKey"] != DBNull.Value)
                        obj.parentKey = Convert.ToInt32((dr["parentKey"]));
                    
                    if (dr["brandKey"] != DBNull.Value)
                        obj.brandKey = Convert.ToInt32((dr["brandKey"]));
               
                    if (dr["name"] != DBNull.Value)
                        obj.name = (dr["name"]).ToString();
                   
                    cmp.Add(obj);
                }
            }
            return cmp;
        }

        public static List<vehicle> vehicle()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from [dbo].[vehicle]", con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            List<vehicle> cmp = new List<vehicle>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    vehicle obj = new vehicle();

                    if (dr["vehicleNo"] != DBNull.Value)
                        obj.vehicleNo = (dr["vehicleNo"]).ToString();

                    if (dr["name"] != DBNull.Value)
                        obj.name = (dr["name"]).ToString();

                    cmp.Add(obj);
                }
            }
            return cmp;
        }

        public static vehicle vehicleInformation(string vehiNo)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT dbo.owner.*, dbo.vehicle.* FROM dbo.owner INNER JOIN dbo.vehicle ON dbo.owner.vehicleNo = dbo.vehicle.vehicleNo WHERE dbo.vehicle.vehicleNo = '" + vehiNo + "'", con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            vehicle obj = new vehicle();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   
                    if (dr["vehicleNo"] != DBNull.Value)
                        obj.vehicleNo = (dr["vehicleNo"]).ToString();

                    if (dr["name"] != DBNull.Value)
                        obj.name = (dr["name"]).ToString();

                    if (dr["chassiNo"] != DBNull.Value)
                        obj.chassiNo = (dr["chassiNo"]).ToString();

                    if (dr["engineNo"] != DBNull.Value)
                        obj.engNo = (dr["engineNo"]).ToString();

                    if (dr["type"] != DBNull.Value)
                        obj.type = (dr["type"]).ToString();

                    if (dr["brand"] != DBNull.Value)
                        obj.brand = (dr["brand"]).ToString();

                    if (dr["initialPrice"] != DBNull.Value)
                        obj.price = Convert.ToInt32(dr["initialPrice"]);

                    if (dr["revenueDate"] != DBNull.Value)
                        obj.revDate = (dr["revenueDate"]).ToString();

                    if (dr["insuranceDate"] != DBNull.Value)
                        obj.insuDate = (dr["insuranceDate"]).ToString();

                    if (dr["preName"] != DBNull.Value)
                        obj.preName = (dr["preName"]).ToString();

                    if (dr["preAddress"] != DBNull.Value)
                        obj.preAddress = (dr["preAddress"]).ToString();

                    if (dr["preNic"] != DBNull.Value)
                        obj.nic = (dr["preNic"]).ToString();

                    if (dr["preTpno"] != DBNull.Value)
                        obj.tpNo = Convert.ToInt32(dr["preTpno"]);

                    if (dr["buyDate"] != DBNull.Value)
                        obj.buyDate = (dr["buyDate"]).ToString();

                    return obj;
                }
            }
            return obj;
        }
        public static List<cost> cost(string vehicleNo)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconnect"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from [dbo].[cost] where vehicleNo = '"+ vehicleNo + "' ", con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            List<cost> cmp = new List<cost>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cost obj = new cost();

                    if (dr["vehicleNo"] != DBNull.Value)
                        obj.vehicleNo = (dr["vehicleNo"]).ToString();

                    if (dr["costType"] != DBNull.Value)
                        obj.desc = (dr["costType"]).ToString();

                    if (dr["costPrice"] != DBNull.Value)
                        obj.value =Convert.ToDecimal((dr["costPrice"]).ToString());

                    if (dr["costId"] != DBNull.Value)
                        obj.costId = Convert.ToInt32((dr["costId"]).ToString());

                    cmp.Add(obj);
                }
            }
            return cmp;
        }
        
    }
}

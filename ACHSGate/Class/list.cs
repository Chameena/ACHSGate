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

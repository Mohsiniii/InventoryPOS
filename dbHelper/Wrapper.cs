using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace dbHelper
{
    public class Wrapper
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataSet ds;
        DataTable dt;
        string conString;
        bool isConnected = false;

        #region constructor
        public Wrapper(String cStr)
        {
            conString = cStr;
            con = new SqlConnection();
            cmd = new SqlCommand();
            adapter = new SqlDataAdapter();
        }
        #endregion constructor

        private bool connectToDatabase()
        {
            con.ConnectionString = conString;
            if (!isConnected)
            {
                try
                {
                    con.Open();
                    isConnected = true;
                } catch(Exception e)
                {
                    isConnected = false;
                }
            }
            return isConnected;
        }

        public DataSet GetDataSet(string Query) {
            DataSet ds = new DataSet();
            if (connectToDatabase())
            {
                cmd.Connection = con;
                cmd.CommandText = Query;
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            return ds;
        }

        public int executeNonQuery(string query)
        {
            int recordCount = 0;
            if (connectToDatabase())
            {
                cmd.Connection = con;
                cmd.CommandText= query;
                recordCount = Convert.ToInt32(cmd.ExecuteNonQuery());
            }
            return recordCount;
        }
    }
}

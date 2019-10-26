using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Data;

namespace QLVT.DBConnection
{
    public class DBManager
    {
        public static SqlConnection conn = new SqlConnection();
        public static String connstr;

        public static String servername;
        public static String username;
        public static String mlogin;
        public static String password;

        public static String database = "QLVT";
        public static String remotelogin = "HTKN";
        public static String remotepassword = "52401091314";
        public static String mloginDN = "";
        public static String passwordDN = "";
        public static String mGroup = "";
        public static String mHoten = "";
        public static int mChinhanh = 0;


        public static int Connect()
        {
           
            if (conn != null && conn.State == ConnectionState.Open) { }
                conn.Close();
            try
            {
                connstr = "Data Source=" + servername + ";Initial Catalog=" + database + ";User ID=" + mlogin + ";Password=" + password;
                conn.ConnectionString = connstr;
                conn.Open();
                return 1;
            }

            catch (Exception e)
            {
                //MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n ");
                return 0;
            }
        }

        public static SqlDataReader ExecSqlDataReader(String strLenh)
        {
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(strLenh, conn);
            sqlcmd.CommandType = CommandType.Text;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader();
                return myreader;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();           
                return null;
            }
        }

    }
}

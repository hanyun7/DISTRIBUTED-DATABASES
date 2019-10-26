using QLVT.DBConnection;
using System;
using System.Collections.Generic;
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

namespace QLVT
{
    /// <summary>
    /// Interaction logic for PhanQuyen.xaml
    /// </summary>
    public partial class PhanQuyen : Window
    {
        public PhanQuyen()
        {
            InitializeComponent();          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            string login = loginname.Text;
            string user = username.Text;
            string pass = password.Text;
            string role = comboBoxPQ.SelectedValue.ToString();
            DBManager.conn.Close();
            SqlDataReader myReader;
            String strLenh = "exec [dbo].[sp_TaoTaiKhoan] '" + login + "','" + pass + "','" + user + "','" + role + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);
            while (myReader.Read())
            {
                if (myReader[0].ToString().Equals("0"))
                {
                    MessageBox.Show("Phân quyền nhân viên thành công!");
                    return;
                }
                else
                {
                    MessageBox.Show("Phân quyền nhân viên thất bại!");
                    return;
                }
            }
        }

        private void PQ_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable datatable = new DataTable();

            datatable.Columns.Add("NAME", typeof(string));
            datatable.Columns.Add("VALUE", typeof(string));

            datatable.Rows.Add(new string[] { "CÔNG TY", "CONGTY" });
            datatable.Rows.Add(new string[] { "CHI NHÁNH", "CHINHANH" });
            datatable.Rows.Add(new string[] { "USER", "USER" });

            comboBoxPQ.ItemsSource = datatable.DefaultView;
            comboBoxPQ.DisplayMemberPath = "NAME";
            comboBoxPQ.SelectedValuePath = "VALUE";

            if (DBManager.mGroup.Equals("CONGTY"))
            {
                comboBoxPQ.SelectedValue = "CONGTY";
                comboBoxPQ.IsEnabled = false;
            }
            else if (DBManager.mGroup.Equals("CHINHANH"))
            {
                comboBoxPQ.SelectedValue = "CHINHANH";
                comboBoxPQ.IsEnabled = false;
            }
        }

        private void Close_click(object sender, RoutedEventArgs e)
        {
            PhanQuyenWidow.Close();
        }
    }
}

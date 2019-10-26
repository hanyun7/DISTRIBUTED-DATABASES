using QLVT.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data;
using System.Data.SqlClient;

namespace QLVT
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();           
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable datatable = new DataTable();

            datatable.Columns.Add("NAME", typeof(string));
            datatable.Columns.Add("VALUE", typeof(string));
           
            datatable.Rows.Add(new string[] { "CHI NHÁNH 1", @"HY-PC\HANYUN1" });
            datatable.Rows.Add(new string[] { "CHI NHÁNH 2", @"HY-PC\HANYUN2" });

            comboBoxCN.ItemsSource = datatable.DefaultView;
            comboBoxCN.DisplayMemberPath = "NAME";
            comboBoxCN.SelectedValuePath = "VALUE";
        }

        private void comboBoxCN_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            DataRowView rov = (DataRowView)e.AddedItems[0];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (username.Text.Trim() == "" || FloatingPasswordBox.Password == "")
            {
                MessageBox.Show("Vui lòng nhập USERNAME và PASSWORD ");
                username.Focus();
                return;
            }
                
            DBManager.servername = comboBoxCN.SelectedValue.ToString();
            DBManager.mlogin = username.Text;
            DBManager.password = FloatingPasswordBox.Password;

            if (DBManager.Connect() == 0)
            {
                MessageBox.Show("USERNAME hoặc PASSWORD không chính xác! ");
                return;
            }
            DBManager.mChinhanh = comboBoxCN.SelectedIndex;

            SqlDataReader myReader;
            DBManager.mloginDN = DBManager.mlogin;
            DBManager.passwordDN = DBManager.password;          
            String strLenh = "exec [dbo].[sp_DangNhap] '" + DBManager.mlogin + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);
            if (myReader == null)
                return;
            myReader.Read();

            DBManager.username = myReader.GetString(0);     // Lay user name
            if (Convert.IsDBNull(DBManager.username))
            {
                MessageBox.Show("Bạn không có quyền truy cập dữ liệu");
                return;
            }
            DBManager.mHoten = myReader.GetString(1);
            DBManager.mGroup = myReader.GetString(2);
            myReader.Close();
            DBManager.conn.Close();
            MessageBox.Show("Đăng nhập thành công!\n" + "TÀI KHOẢN : " + DBManager.mloginDN + "\n" + "NHÓM QUYỀN : " + DBManager.mGroup);
            LoginWidow.Close();
        }
    }
}

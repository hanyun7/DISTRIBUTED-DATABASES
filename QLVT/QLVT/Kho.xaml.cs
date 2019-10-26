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
    /// Interaction logic for Kho.xaml
    /// </summary>
    public partial class Kho : Window
    {
        QLVT.QLVTDataSet1 qLVTDataSet1;
        QLVT.QLVTDataSet1TableAdapters.KHOTableAdapter qLVTDataSet1KHOTableAdapter;
        int flag = 0;

        public Kho()
        {
            InitializeComponent();
        }

        private void KhoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            qLVTDataSet1 = ((QLVT.QLVTDataSet1)(this.FindResource("qLVTDataSet1")));
            // Load data into the table KHO. You can modify this code as needed.
            qLVTDataSet1KHOTableAdapter = new QLVT.QLVTDataSet1TableAdapters.KHOTableAdapter();
            qLVTDataSet1KHOTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1KHOTableAdapter.Fill(qLVTDataSet1.KHO);
            System.Windows.Data.CollectionViewSource kHOViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("kHOViewSource")));
            kHOViewSource.View.MoveCurrentToFirst();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            string makho = BoxMAKHO.Text;
            string tenkho = BoxTENKHO.Text;
            string diachi = BoxDIACHI.Text;
            string chinhanh = comboBoxCN1.SelectedValue.ToString();

            SqlDataReader myReader;
            String strLenh;

            strLenh = "exec [dbo].[sp_KiemTraKho] '" + makho + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);

            while (myReader.Read())
            {
                if (myReader[0].ToString().Equals("1"))
                {
                    MessageBox.Show("MAKHO đã tồn tại");
                    return;
                }
                else
                {
                    DBManager.conn.Close();
                    strLenh = "exec [dbo].[sp_ThemKho] '" + makho + "',N'" + tenkho + "',N'" + diachi + "','" + chinhanh + "'";
                    myReader = DBManager.ExecSqlDataReader(strLenh);
                    if (myReader == null)
                    {
                        MessageBox.Show("Thêm kho thất bại!");
                        return;
                    }

                    myReader.Read();
                    MessageBox.Show("Thêm kho thành công!");
                    flag = 1;
                }
            }
            
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            string makho = BoxMAKHO.Text;
            string tenkho = BoxTENKHO.Text;
            string diachi = BoxDIACHI.Text;
            string chinhanh = comboBoxCN1.SelectedValue.ToString();

            SqlDataReader myReader;
            String strLenh = "exec [dbo].[sp_ChinhSuaKho] '" + makho + "',N'" + tenkho + "',N'" + diachi + "','" + chinhanh + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);
            if (myReader == null)
            {
                MessageBox.Show("Chỉnh sửa kho thất bại!");
                return;
            }

            myReader.Read();
            MessageBox.Show("Chỉnh sửa kho thành công!");
            flag = 2;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            string makho = BoxMAKHO.Text;
            SqlDataReader myReader;
            String strLenh = "exec [dbo].[sp_XoaKho] '" + makho + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);
            if (myReader == null)
            {
                MessageBox.Show("Xóa kho thất bại!");
                return;
            }

            myReader.Read();
            MessageBox.Show("Xóa kho thành công!");
            flag = 3;
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            qLVTDataSet1KHOTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1KHOTableAdapter.Fill(qLVTDataSet1.KHO);
            System.Windows.Data.CollectionViewSource kHOViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("kHOViewSource")));
            kHOViewSource.View.MoveCurrentToFirst();
        }

        private void comboBoxCN_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            DataRowView rov = (DataRowView)e.AddedItems[0];

            DBManager.servername = comboBoxCN.SelectedValue.ToString();

            if (comboBoxCN.SelectedIndex != DBManager.mChinhanh)
            {
                DBManager.mlogin = DBManager.remotelogin;
                DBManager.password = DBManager.remotepassword;
            }
            else
            {
                DBManager.mlogin = DBManager.mloginDN;
                DBManager.password = DBManager.passwordDN;
            }

            if (DBManager.Connect() == 0)
                MessageBox.Show("Lỗi kết nối về chi nhánh mới");
            else
            {
                qLVTDataSet1KHOTableAdapter.Connection.ConnectionString = DBManager.connstr;
                qLVTDataSet1KHOTableAdapter.Fill(qLVTDataSet1.KHO);
                System.Windows.Data.CollectionViewSource kHOViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("kHOViewSource")));
                kHOViewSource.View.MoveCurrentToFirst();
            }
        }

        private void selected(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                BoxMAKHO.Text = row_selected["MAKHO"].ToString();
                BoxTENKHO.Text = row_selected["TENKHO"].ToString();
                BoxDIACHI.Text = row_selected["DIACHI"].ToString();

                if (comboBoxCN.SelectedIndex == 0)
                {
                    comboBoxCN1.SelectedIndex = 0;
                }
                else
                {
                    comboBoxCN1.SelectedIndex = 1;
                }
            }
        }

        private void SearchMAKHO(object sender, KeyEventArgs e)
        {
            if (khoSearch.Text != "" && kHODataGrid.ItemsSource != null)
            {
                string makho = khoSearch.Text;

                SqlDataReader myReader;
                String strLenh = "exec [dbo].[sp_TimKiemKho] '" + makho + "'";
                myReader = DBManager.ExecSqlDataReader(strLenh);

                DataTable datatable4 = new DataTable();
                datatable4.Load(myReader);
                kHODataGrid.ItemsSource = datatable4.DefaultView;

            }
            if (khoSearch.Text == "")
            {
                kHODataGrid.ItemsSource = qLVTDataSet1.KHO.DefaultView;
            }
        }

        private void KHO_Loaded(object sender, RoutedEventArgs e)
        {

            DataTable datatable = new DataTable();

            datatable.Columns.Add("NAME", typeof(string));
            datatable.Columns.Add("VALUE", typeof(string));

            datatable.Rows.Add(new string[] { "CHI NHÁNH 1", @"HY-PC\HANYUN1" });
            datatable.Rows.Add(new string[] { "CHI NHÁNH 2", @"HY-PC\HANYUN2" });

            comboBoxCN.ItemsSource = datatable.DefaultView;
            comboBoxCN.DisplayMemberPath = "NAME";
            comboBoxCN.SelectedValuePath = "VALUE";

            comboBoxCN.SelectedIndex = DBManager.mChinhanh;

            if (DBManager.mGroup.Equals("CONGTY"))
            {
                comboBoxCN.IsEnabled = true;
            }
            else
            {
                comboBoxCN.IsEnabled = false;
            }

            DataTable datatable1 = new DataTable();

            datatable1.Columns.Add("NAME", typeof(string));
            datatable1.Columns.Add("VALUE", typeof(string));

            datatable1.Rows.Add(new string[] { "CHI NHÁNH 1", "CN1" });
            datatable1.Rows.Add(new string[] { "CHI NHÁNH 2", "CN2" });

            comboBoxCN1.ItemsSource = datatable1.DefaultView;
            comboBoxCN1.DisplayMemberPath = "NAME";
            comboBoxCN1.SelectedValuePath = "VALUE";
        }
    }
}

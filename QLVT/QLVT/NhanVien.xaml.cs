using QLVT.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for NhanVien.xaml
    /// </summary>
    public partial class NhanVien : Window
    {

        QLVT.QLVTDataSet1 qLVTDataSet1;
        QLVT.QLVTDataSet1TableAdapters.NHANVIENTableAdapter qLVTDataSet1NHANVIENTableAdapter;
        int flag = 0;
        string original_nv;

        public NhanVien()
        {
            InitializeComponent();

        }

        private void QLNV_Loaded(object sender, RoutedEventArgs e)
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
                qLVTDataSet1NHANVIENTableAdapter.Connection.ConnectionString = DBManager.connstr;
                qLVTDataSet1NHANVIENTableAdapter.Fill(qLVTDataSet1.NHANVIEN);
                System.Windows.Data.CollectionViewSource nHANVIENViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("nHANVIENViewSource")));
                nHANVIENViewSource.View.MoveCurrentToFirst();

            }
        }

        private void NhanVienWidow_Loaded(object sender, RoutedEventArgs e)
        {
            qLVTDataSet1 = ((QLVT.QLVTDataSet1)(this.FindResource("qLVTDataSet1")));
            // Load data into the table NHANVIEN. You can modify this code as needed.        
            qLVTDataSet1NHANVIENTableAdapter = new QLVT.QLVTDataSet1TableAdapters.NHANVIENTableAdapter();
            qLVTDataSet1NHANVIENTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1NHANVIENTableAdapter.Fill(qLVTDataSet1.NHANVIEN);
            System.Windows.Data.CollectionViewSource nHANVIENViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("nHANVIENViewSource")));
            nHANVIENViewSource.View.MoveCurrentToFirst();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            int manv = 0;
            float luong = 0;
            DateTime? ns = BoxBD.SelectedDate;
            String ngaysinh = Convert.ToDateTime(ns).ToString("yyyy/MM/dd");
            if (int.TryParse(BoxMANV.Text, out manv)) {; }
            string ho = BoxHO.Text;
            string ten = BoxTEN.Text;
            string diachi = BoxDIACHI.Text;
            if (float.TryParse(BoxLUONG.Text, out luong)) {;}
            string chinhanh = comboBoxCN1.SelectedValue.ToString();
            SqlDataReader myReader;
            String strLenh;

            strLenh = "exec [dbo].[sp_KiemTraNhanVien] " + manv.ToString();
            myReader = DBManager.ExecSqlDataReader(strLenh);

            while(myReader.Read())
            {
                if (myReader[0].ToString().Equals("1"))
                {
                    MessageBox.Show("MANV đã tồn tại");
                    return;
                }
                else
                {
                    DBManager.conn.Close();
                    strLenh = "exec [dbo].[sp_ThemNhanVien] " + manv.ToString() + ",N'" + ho + "',N'" + ten + "',N'" + diachi + "','" + ngaysinh + "'," + luong + ",N'" + chinhanh + "'";
                    myReader = DBManager.ExecSqlDataReader(strLenh);
                    if (myReader == null)
                    {
                        MessageBox.Show("Thêm nhân viên thất bại!");
                        return;
                    }

                    myReader.Read();
                    MessageBox.Show("Thêm nhân viên thành công!");
                    flag = 1;
                }
            }
            
        }

        private void selected(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                BoxMANV.Text = row_selected["MANV"].ToString();
                BoxHO.Text = row_selected["HO"].ToString();
                BoxTEN.Text = row_selected["TEN"].ToString();
                BoxDIACHI.Text = row_selected["DIACHI"].ToString();
                BoxBD.SelectedDate = row_selected["NGAYSINH"] as DateTime?;
                BoxLUONG.Text = row_selected["LUONG"].ToString();

                original_nv = BoxHO.Text + " " + BoxTEN.Text + " " + BoxDIACHI.Text + " " + BoxBD.SelectedDate.ToString() + " " + BoxLUONG.Text;

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

        private void SearchMANV(object sender, KeyEventArgs e)
        {
            if (nhanvienSearch.Text != "" && nHANVIENDataGrid.ItemsSource != null)
            {
                string manv = nhanvienSearch.Text;
               
                SqlDataReader myReader;
                String strLenh = "exec [dbo].[sp_TimKiemNhanVien] " + manv;
                myReader = DBManager.ExecSqlDataReader(strLenh);

                DataTable datatable2 = new DataTable();
                datatable2.Load(myReader);
                nHANVIENDataGrid.ItemsSource = datatable2.DefaultView;
            }
            if (nhanvienSearch.Text == "")
            {
                nHANVIENDataGrid.ItemsSource = qLVTDataSet1.NHANVIEN.DefaultView;
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            int manv = 0;
            float luong = 0;
            DateTime? ns = BoxBD.SelectedDate;
            String ngaysinh = Convert.ToDateTime(ns).ToString("yyyy/MM/dd");
            if (int.TryParse(BoxMANV.Text, out manv)) {; }
            string ho = BoxHO.Text;
            string ten = BoxTEN.Text;
            string diachi = BoxDIACHI.Text;
            if (float.TryParse(BoxLUONG.Text, out luong)) {; }
            string chinhanh = comboBoxCN1.SelectedValue.ToString();
            
            SqlDataReader myReader;
            String strLenh = "exec [dbo].[sp_ChinhSuaNhanVien] " + manv.ToString() + ",N'" + ho + "',N'" + ten + "',N'" + diachi + "','" + ngaysinh + "'," + luong + ",N'" + chinhanh + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);
            if (myReader == null)
            {
                MessageBox.Show("Chỉnh sửa nhân viên thất bại!");
                return;
            }

            myReader.Read();
            MessageBox.Show("Chỉnh sửa nhân viên thành công!");
            flag = 3;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            string manv = BoxMANV.Text;
            SqlDataReader myReader;
            String strLenh = "exec [dbo].[sp_XoaNhanVien] " + manv;
            myReader = DBManager.ExecSqlDataReader(strLenh);
            if (myReader == null)
            {
                MessageBox.Show("Xóa nhân viên thất bại!");
                return;
            }

            myReader.Read();
            MessageBox.Show("Xóa nhân viên thành công!");
            flag = 2;
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            qLVTDataSet1NHANVIENTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1NHANVIENTableAdapter.Fill(qLVTDataSet1.NHANVIEN);
            System.Windows.Data.CollectionViewSource nHANVIENViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("nHANVIENViewSource")));
            nHANVIENViewSource.View.MoveCurrentToFirst();
        }

        private void ButtonInDSNV_Click(object sender, RoutedEventArgs e)
        {
            ReportNhanVien reportnhanvien = new ReportNhanVien();
            reportnhanvien.ShowDialog();
        }

        private void ButtonUndo_Click(object sender, RoutedEventArgs e)
        {
            //string manv = BoxMANV.Text;
            //SqlDataReader myReader;
            //String strLenh;
            //if (flag == 1)
            //{
            //    DBManager.conn.Close();
            //    strLenh = "exec [dbo].[sp_UndoThemNhanVien] " + manv;
            //    myReader = DBManager.ExecSqlDataReader(strLenh);
            //}
            //else if (flag == 2)
            //{
            //    DBManager.conn.Close();
            //    strLenh = "exec [dbo].[sp_UndoXoaNhanVien] " + manv;
            //    myReader = DBManager.ExecSqlDataReader(strLenh);
            //}
            //else if (flag == 3)
            //{
            //    DBManager.conn.Close();
            //    string[] originalnv = original_nv.Split(' ');
            //    strLenh = "exec [dbo].[sp_UndoChinhSuaNhanVien] N'" + originalnv[0] + "',N'" + originalnv[1] + "',N'" + originalnv[2] + "','" + originalnv[3] + "'," + originalnv[4] ;
            //    myReader = DBManager.ExecSqlDataReader(strLenh);
            //}
        }

        private void ButtonPQNV_Click(object sender, RoutedEventArgs e)
        {
            PhanQuyen phanquyenwindow = new PhanQuyen();
            phanquyenwindow.ShowDialog();
        }
    }
}

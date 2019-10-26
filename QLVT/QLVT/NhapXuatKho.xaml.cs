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
    /// Interaction logic for NhapXuatKho.xaml
    /// </summary>
    public partial class NhapXuatKho : Window
    {
        QLVT.QLVTDataSet1 qLVTDataSet1;
        QLVT.QLVTDataSet1TableAdapters.NHANVIENTableAdapter qLVTDataSet1NHANVIENTableAdapter;
        QLVT.QLVTDataSet1TableAdapters.VATTUTableAdapter qLVTDataSet1VATTUTableAdapter;
        QLVT.QLVTDataSet1TableAdapters.KHOTableAdapter qLVTDataSet1KHOTableAdapter;
        int flag = 0;

        DataTable datatable2;

        public NhapXuatKho()
        {
            InitializeComponent();
        }

        private void NXVT_Loaded(object sender, RoutedEventArgs e)
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
          
        }

        private void PhieuNhapXuat_Loaded(object sender, RoutedEventArgs e)
        {
            qLVTDataSet1 = ((QLVT.QLVTDataSet1)(this.FindResource("qLVTDataSet1")));
            // Load data into the table NHANVIEN. You can modify this code as needed.
            qLVTDataSet1NHANVIENTableAdapter = new QLVT.QLVTDataSet1TableAdapters.NHANVIENTableAdapter();
            qLVTDataSet1NHANVIENTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1NHANVIENTableAdapter.Fill(qLVTDataSet1.NHANVIEN);
            System.Windows.Data.CollectionViewSource nHANVIENViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("nHANVIENViewSource")));
            nHANVIENViewSource.View.MoveCurrentToFirst();
            // Load data into the table VATTU. You can modify this code as needed.
            qLVTDataSet1VATTUTableAdapter = new QLVT.QLVTDataSet1TableAdapters.VATTUTableAdapter();
            qLVTDataSet1VATTUTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1VATTUTableAdapter.Fill(qLVTDataSet1.VATTU);
            System.Windows.Data.CollectionViewSource vATTUViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("vATTUViewSource")));
            vATTUViewSource.View.MoveCurrentToFirst();
            // Load data into the table KHO. You can modify this code as needed.
            qLVTDataSet1KHOTableAdapter = new QLVT.QLVTDataSet1TableAdapters.KHOTableAdapter();
            qLVTDataSet1KHOTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1KHOTableAdapter.Fill(qLVTDataSet1.KHO);
            System.Windows.Data.CollectionViewSource kHOViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("kHOViewSource")));
            kHOViewSource.View.MoveCurrentToFirst();

            DataTable datatable1 = new DataTable();
            datatable1.Columns.Add("ID", typeof(string));
            datatable1.Columns.Add("VALUE", typeof(string));

            datatable1.Rows.Add(new string[] { "N", "N" });
            datatable1.Rows.Add(new string[] { "X", "X" });

            comboBoxPHIEU.ItemsSource = datatable1.DefaultView;
            comboBoxPHIEU.DisplayMemberPath = "ID";
            comboBoxPHIEU.SelectedValuePath = "VALUE";
            //

            comboBoxMAVT.DisplayMemberPath = "TENVT";
            comboBoxMAVT.SelectedValuePath = "MAVT";

            comboBoxMAKHO.DisplayMemberPath = "TENKHO";
            comboBoxMAKHO.SelectedValuePath = "MAKHO";

            comboBoxMANV.DisplayMemberPath = "MANV";
            comboBoxMANV.SelectedValuePath = "MANV";

            comboBoxMANV.SelectedValue = DBManager.username;
            comboBoxMANV.IsEnabled = false;
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

                qLVTDataSet1VATTUTableAdapter.Connection.ConnectionString = DBManager.connstr;
                qLVTDataSet1VATTUTableAdapter.Fill(qLVTDataSet1.VATTU);
                System.Windows.Data.CollectionViewSource vATTUViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("vATTUViewSource")));
                vATTUViewSource.View.MoveCurrentToFirst();

                qLVTDataSet1KHOTableAdapter.Connection.ConnectionString = DBManager.connstr;
                qLVTDataSet1KHOTableAdapter.Fill(qLVTDataSet1.KHO);
                System.Windows.Data.CollectionViewSource kHOViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("kHOViewSource")));
                kHOViewSource.View.MoveCurrentToFirst();
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            string maphieu = BoxPHIEU.Text;
            string mavt = comboBoxMAVT.SelectedValue.ToString();
            string soluong = BoxSOLUONG.Text;
            string dongia = BoxDONGIA.Text;
            string makho = comboBoxMAKHO.SelectedValue.ToString();
            string manv = comboBoxMANV.SelectedValue.ToString();
            string kh = BoxHOTENKH.Text;
            string loai = comboBoxPHIEU.SelectedValue.ToString();
            DateTime? nn = NGAYNHAP.SelectedDate;
            String ngaynhap = Convert.ToDateTime(nn).ToString("yyyy/MM/dd");
            float tongtien = 0;
            string tt = tongtien.ToString();

            datatable2 = new DataTable();

            SqlDataReader myReader;
            String strLenh;

            strLenh = "exec [dbo].[sp_KiemTraPhatSinh] '" + maphieu + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);

            while (myReader.Read())
            {
                if (myReader[0].ToString().Equals("1"))
                {
                    MessageBox.Show("PHIEU đã tồn tại");       
                    if (flag == 0)
                    {
                        return;
                    }                             
                }
                else
                {
                    DBManager.conn.Close();
                    strLenh = "exec [dbo].[sp_ThemPhatSinh] '" + maphieu + "','" + ngaynhap + "','" + loai + "',N'" + kh + "'," + tt + "," + manv + ",'" + makho + "'";
                    myReader = DBManager.ExecSqlDataReader(strLenh);
                    if (myReader == null)
                    {
                        return;
                    }

                    myReader.Read();
                    flag = 1;
                }
            }

            DBManager.conn.Close();
            strLenh = "exec [dbo].[sp_ThemCT_PhatSinh] '" + maphieu + "','" + mavt + "'," + soluong + "," + dongia;
            myReader = DBManager.ExecSqlDataReader(strLenh);

            if (myReader == null)
            {
                MessageBox.Show("Thêm thất bại");
                return;
            }
            MessageBox.Show("Thêm thành công");

            DBManager.conn.Close();
            strLenh = "exec [dbo].[sp_HienThiCT_PhatSinh] '" + maphieu + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);

            datatable2.Load(myReader);

            nHAPXUATKHODataGrid.ItemsSource = datatable2.DefaultView;

            DBManager.conn.Close();
            strLenh = "select [dbo].[PhatSinh].[THANHTIEN] FROM [dbo].[PhatSinh] WHERE [dbo].[PhatSinh].[PHIEU] = '" + maphieu + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);

            while(myReader.Read())
            {
                TONGTIEN.Text = myReader["THANHTIEN"].ToString();
            }

        }

        private void selected(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {               
                comboBoxMAVT.SelectedValue = row_selected["MAVT"];
                BoxSOLUONG.Text = row_selected["SOLUONG"].ToString();
                BoxDONGIA.Text = row_selected["DONGIA"].ToString();
            }
        }

        private void SearchPHIEU(object sender, KeyEventArgs e)
        {
            if (BoxPHIEU.Text != "")
            {
                string maphieu = BoxPHIEU.Text;
                DBManager.conn.Close();
                SqlDataReader myReader;
                String strLenh;
                strLenh = "exec [dbo].[sp_SelectPhatSinh] '" + maphieu + "'";
                myReader = DBManager.ExecSqlDataReader(strLenh);

                if (myReader.HasRows)
                {
                    DataTable datatable2 = new DataTable();
                    while (myReader.Read())
                    {
                        comboBoxMANV.SelectedValue = myReader["MANV"].ToString();
                        BoxHOTENKH.Text = myReader["HOTENKH"].ToString();
                        comboBoxPHIEU.SelectedValue = myReader["LOAI"].ToString();
                        comboBoxMAKHO.SelectedValue = myReader["MAKHO"].ToString();
                        NGAYNHAP.SelectedDate = myReader["NGAY"] as DateTime?;
                        TONGTIEN.Text = myReader["THANHTIEN"].ToString();
                    }
                    
                    DBManager.conn.Close();
                    strLenh = "exec [dbo].[sp_HienThiCT_PhatSinh] '" + maphieu + "'";
                    myReader = DBManager.ExecSqlDataReader(strLenh);

                    datatable2.Load(myReader);

                    nHAPXUATKHODataGrid.ItemsSource = datatable2.DefaultView;

                }
                else
                {
                    return;
                }
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            string maphieu = BoxPHIEU.Text;
            string mavt = comboBoxMAVT.SelectedValue.ToString();
            SqlDataReader myReader;
            String strLenh = "exec [dbo].[sp_XoaCT_PhatSinh] '" + maphieu + "','" + mavt + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);
            if (myReader == null)
            {
                MessageBox.Show("Xóa chi tiết phát sinh thất bại!");
                return;
            }

            myReader.Read();
            MessageBox.Show("Xóa chi tiết phát sinh thành công!");

            datatable2 = new DataTable();
            DBManager.conn.Close();
            strLenh = "exec [dbo].[sp_HienThiCT_PhatSinh] '" + maphieu + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);

            datatable2.Load(myReader);

            nHAPXUATKHODataGrid.ItemsSource = datatable2.DefaultView;

            DBManager.conn.Close();
            strLenh = "select [dbo].[PhatSinh].[THANHTIEN] FROM [dbo].[PhatSinh] WHERE [dbo].[PhatSinh].[PHIEU] = '" + maphieu + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);

            while (myReader.Read())
            {
                TONGTIEN.Text = myReader["THANHTIEN"].ToString();
            }
        }
    }
}

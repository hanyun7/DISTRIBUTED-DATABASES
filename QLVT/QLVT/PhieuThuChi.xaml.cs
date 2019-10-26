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
    /// Interaction logic for PhieuThuChi.xaml
    /// </summary>
    public partial class PhieuThuChi : Window
    {

        QLVT.QLVTDataSet1 qLVTDataSet1;
        QLVT.QLVTDataSet1TableAdapters.NHANVIENTableAdapter qLVTDataSet1NHANVIENTableAdapter;
        QLVT.QLVTDataSet1TableAdapters.KHOTableAdapter qLVTDataSet1KHOTableAdapter;

        public PhieuThuChi()
        {
            InitializeComponent();
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

                qLVTDataSet1KHOTableAdapter.Connection.ConnectionString = DBManager.connstr;
                qLVTDataSet1KHOTableAdapter.Fill(qLVTDataSet1.KHO);
                System.Windows.Data.CollectionViewSource kHOViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("kHOViewSource")));
                kHOViewSource.View.MoveCurrentToFirst();
            }
        }

        private void PHIEUTC_Loaded(object sender, RoutedEventArgs e)
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

        private void PhieuThuChiWindow_Loaded(object sender, RoutedEventArgs e)
        {

            qLVTDataSet1 = ((QLVT.QLVTDataSet1)(this.FindResource("qLVTDataSet1")));
            // Load data into the table NHANVIEN. You can modify this code as needed.
            qLVTDataSet1NHANVIENTableAdapter = new QLVT.QLVTDataSet1TableAdapters.NHANVIENTableAdapter();
            qLVTDataSet1NHANVIENTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1NHANVIENTableAdapter.Fill(qLVTDataSet1.NHANVIEN);
            System.Windows.Data.CollectionViewSource nHANVIENViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("nHANVIENViewSource")));
            nHANVIENViewSource.View.MoveCurrentToFirst();
            // Load data into the table KHO. You can modify this code as needed.
            qLVTDataSet1KHOTableAdapter = new QLVT.QLVTDataSet1TableAdapters.KHOTableAdapter();
            qLVTDataSet1KHOTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1KHOTableAdapter.Fill(qLVTDataSet1.KHO);
            System.Windows.Data.CollectionViewSource kHOViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("kHOViewSource")));
            kHOViewSource.View.MoveCurrentToFirst();

            DataTable datatable1 = new DataTable();
            datatable1.Columns.Add("ID", typeof(string));
            datatable1.Columns.Add("VALUE", typeof(string));

            datatable1.Rows.Add(new string[] { "T", "T" });
            datatable1.Rows.Add(new string[] { "C", "C" });

            comboBoxPHIEU.ItemsSource = datatable1.DefaultView;
            comboBoxPHIEU.DisplayMemberPath = "ID";
            comboBoxPHIEU.SelectedValuePath = "VALUE";
            //

            comboBoxMAKHO.DisplayMemberPath = "TENKHO";
            comboBoxMAKHO.SelectedValuePath = "MAKHO";

            comboBoxMANV.DisplayMemberPath = "MANV";
            comboBoxMANV.SelectedValuePath = "MANV";

            comboBoxMANV.SelectedValue = DBManager.username;           
            //comboBoxMANV.IsEnabled = false;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            string maphieu = BoxPHIEU.Text;
            string makho = comboBoxMAKHO.SelectedValue.ToString();
            string manv = comboBoxMANV.SelectedValue.ToString();
            string kh = BoxHOTENKH.Text;
            string loai = comboBoxPHIEU.SelectedValue.ToString();
            DateTime? nn = NGAYNHAP.SelectedDate;
            String ngaynhap = Convert.ToDateTime(nn).ToString("yyyy/MM/dd");
            string tongtien = TONGTIEN.Text;

            SqlDataReader myReader;
            String strLenh;
            strLenh = "exec [dbo].[sp_KiemTraPhatSinh] '" + maphieu + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);

            while (myReader.Read())
            {
                if (myReader[0].ToString().Equals("1"))
                {
                    MessageBox.Show("PHIEU đã tồn tại");
                    return;
                }
                else
                {
                    DBManager.conn.Close();
                    strLenh = "exec [dbo].[sp_ThemPhatSinh] '" + maphieu + "','" + ngaynhap + "','" + loai + "',N'" + kh + "'," + tongtien + "," + manv + ",'" + makho + "'";
                    myReader = DBManager.ExecSqlDataReader(strLenh);
                    if (myReader == null)
                    {
                        MessageBox.Show("Thêm phiếu thất bại");
                        return;
                    }

                    MessageBox.Show("Thêm phiếu thành công");
                }
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
                    while (myReader.Read())
                    {
                        comboBoxMANV.SelectedValue = myReader["MANV"].ToString();
                        BoxHOTENKH.Text = myReader["HOTENKH"].ToString();
                        comboBoxPHIEU.SelectedValue = myReader["LOAI"].ToString();
                        comboBoxMAKHO.SelectedValue = myReader["MAKHO"].ToString();
                        NGAYNHAP.SelectedDate = myReader["NGAY"] as DateTime?;
                        TONGTIEN.Text = myReader["THANHTIEN"].ToString();
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
}

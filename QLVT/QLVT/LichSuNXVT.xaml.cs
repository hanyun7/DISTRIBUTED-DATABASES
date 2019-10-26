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
    /// Interaction logic for LichSuNXVT.xaml
    /// </summary>
    public partial class LichSuNXVT : Window
    {

        QLVT.QLVTDataSet1 qLVTDataSet1;
        QLVT.QLVTDataSet1TableAdapters.PHATSINHTableAdapter qLVTDataSet1PHATSINHTableAdapter;
        QLVT.QLVTDataSet1TableAdapters.CT_PHATSINHTableAdapter qLVTDataSet1CT_PHATSINHTableAdapter;
        String maphieu;
        String mavt;

        public LichSuNXVT()
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
                qLVTDataSet1PHATSINHTableAdapter.Connection.ConnectionString = DBManager.connstr;
                qLVTDataSet1PHATSINHTableAdapter.Fill(qLVTDataSet1.PHATSINH);
                System.Windows.Data.CollectionViewSource pHATSINHViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pHATSINHViewSource")));
                pHATSINHViewSource.View.MoveCurrentToFirst();

                qLVTDataSet1CT_PHATSINHTableAdapter.Connection.ConnectionString = DBManager.connstr;
                qLVTDataSet1CT_PHATSINHTableAdapter.Fill(qLVTDataSet1.CT_PHATSINH);
                System.Windows.Data.CollectionViewSource cT_PHATSINHViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cT_PHATSINHViewSource")));
                cT_PHATSINHViewSource.View.MoveCurrentToFirst();
            }
        }

        private void LichSuNXVT_Loaded(object sender, RoutedEventArgs e)
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
            // Load data into the table PHATSINH. You can modify this code as needed.
            qLVTDataSet1PHATSINHTableAdapter = new QLVT.QLVTDataSet1TableAdapters.PHATSINHTableAdapter();
            qLVTDataSet1PHATSINHTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1PHATSINHTableAdapter.Fill(qLVTDataSet1.PHATSINH);
            System.Windows.Data.CollectionViewSource pHATSINHViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pHATSINHViewSource")));
            pHATSINHViewSource.View.MoveCurrentToFirst();
            // Load data into the table CT_PHATSINH. You can modify this code as needed.
            qLVTDataSet1CT_PHATSINHTableAdapter = new QLVT.QLVTDataSet1TableAdapters.CT_PHATSINHTableAdapter();
            qLVTDataSet1CT_PHATSINHTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1CT_PHATSINHTableAdapter.Fill(qLVTDataSet1.CT_PHATSINH);
            System.Windows.Data.CollectionViewSource cT_PHATSINHViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cT_PHATSINHViewSource")));
            cT_PHATSINHViewSource.View.MoveCurrentToFirst();
        }

        private void selected(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            SqlDataReader myReader;
            String strLenh;
            DataTable datatable2 = new DataTable();
            if (row_selected != null)
            {
                maphieu = row_selected["PHIEU"].ToString();
                strLenh = "exec [dbo].[sp_HienThiCT_PhatSinh] '" + maphieu + "'";
                myReader = DBManager.ExecSqlDataReader(strLenh);

                datatable2.Load(myReader);

                cT_PHATSINHDataGrid.ItemsSource = datatable2.DefaultView;
            }
               
        }

        private void ButtonDeletePHIEU_Click(object sender, RoutedEventArgs e)
        {
            
            SqlDataReader myReader;
            String strLenh;
            DataTable datatable2 = new DataTable();           

            strLenh = "exec [dbo].[sp_HienThiCT_PhatSinh] '" + maphieu + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);

            if (myReader.HasRows)
            {
                MessageBox.Show("Không thể xóa phiếu");
                return;
            }
            else
            {
                DBManager.conn.Close();
                strLenh = "exec [dbo].[sp_XoaPhatSinh] '" + maphieu + "'";
                myReader = DBManager.ExecSqlDataReader(strLenh);
                if (myReader == null)
                {
                    MessageBox.Show("Xóa PHIẾU thất bại");
                    return;
                }
                MessageBox.Show("Xóa PHIẾU thành công");

                DBManager.conn.Close();
                qLVTDataSet1PHATSINHTableAdapter.Connection.ConnectionString = DBManager.connstr;
                qLVTDataSet1PHATSINHTableAdapter.Fill(qLVTDataSet1.PHATSINH);
                System.Windows.Data.CollectionViewSource pHATSINHViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pHATSINHViewSource")));
                pHATSINHViewSource.View.MoveCurrentToFirst();

                DBManager.conn.Close();
                qLVTDataSet1CT_PHATSINHTableAdapter.Connection.ConnectionString = DBManager.connstr;
                qLVTDataSet1CT_PHATSINHTableAdapter.Fill(qLVTDataSet1.CT_PHATSINH);
                System.Windows.Data.CollectionViewSource cT_PHATSINHViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cT_PHATSINHViewSource")));
                cT_PHATSINHViewSource.View.MoveCurrentToFirst();
            }
        }

        private void ButtonDeleteCTPHIEU_Click(object sender, RoutedEventArgs e)
        {
            SqlDataReader myReader;
            String strLenh;
            DataTable datatable2 = new DataTable();

            strLenh = "exec [dbo].[sp_XoaCT_PhatSinh] '" + maphieu + "','" + mavt + "'" ;
            myReader = DBManager.ExecSqlDataReader(strLenh);

            if (myReader == null)
            {
                MessageBox.Show("Xóa CT_PHIẾU thất bại");
                return;
            }
            MessageBox.Show("Xóa CT_PHIẾU thành công");

            DBManager.conn.Close();
            qLVTDataSet1PHATSINHTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1PHATSINHTableAdapter.Fill(qLVTDataSet1.PHATSINH);
            System.Windows.Data.CollectionViewSource pHATSINHViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pHATSINHViewSource")));
            pHATSINHViewSource.View.MoveCurrentToFirst();

            DBManager.conn.Close();
            qLVTDataSet1CT_PHATSINHTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1CT_PHATSINHTableAdapter.Fill(qLVTDataSet1.CT_PHATSINH);
            System.Windows.Data.CollectionViewSource cT_PHATSINHViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cT_PHATSINHViewSource")));
            cT_PHATSINHViewSource.View.MoveCurrentToFirst();
        }

        private void selectedCTPS(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                mavt = row_selected["MAVT"].ToString();
            }
        }

        private void ButtonALL_Click(object sender, RoutedEventArgs e)
        {
            qLVTDataSet1PHATSINHTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1PHATSINHTableAdapter.Fill(qLVTDataSet1.PHATSINH);
            System.Windows.Data.CollectionViewSource pHATSINHViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pHATSINHViewSource")));
            pHATSINHViewSource.View.MoveCurrentToFirst();

            qLVTDataSet1CT_PHATSINHTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1CT_PHATSINHTableAdapter.Fill(qLVTDataSet1.CT_PHATSINH);
            System.Windows.Data.CollectionViewSource cT_PHATSINHViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cT_PHATSINHViewSource")));
            cT_PHATSINHViewSource.View.MoveCurrentToFirst();
        }

        private void ButtonAddNX_Click(object sender, RoutedEventArgs e)
        {
            NhapXuatKho phieunhapxuatWindow = new NhapXuatKho();
            phieunhapxuatWindow.ShowDialog();
        }

        private void ButtonAddTC_Click(object sender, RoutedEventArgs e)
        {
            PhieuThuChi phieuthuchiWindow = new PhieuThuChi();
            phieuthuchiWindow.ShowDialog();
        }

        private void ButtonInDSNX_Click(object sender, RoutedEventArgs e)
        {
            ReportChiTietNX reportchitietnxWindow = new ReportChiTietNX();
            reportchitietnxWindow.ShowDialog();
        }
    }
}

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
    /// Interaction logic for VatTu.xaml
    /// </summary>
    public partial class VatTu : Window
    {
        QLVT.QLVTDataSet1 qLVTDataSet1;
        QLVT.QLVTDataSet1TableAdapters.VATTUTableAdapter qLVTDataSet1VATTUTableAdapter;
        int flag = 0;

        public VatTu()
        {
            InitializeComponent();
        }

        private void VatTuWindow_Loaded(object sender, RoutedEventArgs e)
        {

            qLVTDataSet1 = ((QLVT.QLVTDataSet1)(this.FindResource("qLVTDataSet1")));
            // Load data into the table VATTU. You can modify this code as needed.
            qLVTDataSet1VATTUTableAdapter = new QLVT.QLVTDataSet1TableAdapters.VATTUTableAdapter();
            qLVTDataSet1VATTUTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1VATTUTableAdapter.Fill(qLVTDataSet1.VATTU);
            System.Windows.Data.CollectionViewSource vATTUViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("vATTUViewSource")));
            vATTUViewSource.View.MoveCurrentToFirst();
        }

        private void selected(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                BoxMAVT.Text = row_selected["MAVT"].ToString();
                BoxTENVT.Text = row_selected["TENVT"].ToString();
                BoxDVT.Text = row_selected["DVT"].ToString();               
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            string mavt = BoxMAVT.Text;
            string tenvt = BoxTENVT.Text;
            string dvt = BoxDVT.Text;
            
            SqlDataReader myReader;
            String strLenh;

            strLenh = "exec [dbo].[sp_KiemTraVatTu] '" + mavt + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);

            while (myReader.Read())
            {
                if (myReader[0].ToString().Equals("1"))
                {
                    MessageBox.Show("MAVT đã tồn tại");
                    return;
                }
                else
                {
                    DBManager.conn.Close();
                    strLenh = "exec [dbo].[sp_ThemVatTu] '" + mavt + "',N'" + tenvt + "',N'" + dvt + "'";
                    myReader = DBManager.ExecSqlDataReader(strLenh);
                    if (myReader == null)
                    {
                        MessageBox.Show("Thêm vật tư thất bại!");
                        return;
                    }

                    myReader.Read();
                    MessageBox.Show("Thêm vật tư thành công!");
                    flag = 1;
                }
            }      
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            string mavt = BoxMAVT.Text;
            string tenvt = BoxTENVT.Text;
            string dvt = BoxDVT.Text;

            SqlDataReader myReader;
            String strLenh = "exec [dbo].[sp_ChinhSuaVatTu] '" + mavt + "',N'" + tenvt + "',N'" + dvt + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);
            if (myReader == null)
            {
                MessageBox.Show("Chỉnh sửa vật tư thất bại!");
                return;
            }

            myReader.Read();
            MessageBox.Show("Chỉnh sửa vật tư thành công!");
            flag = 2;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            string mavt = BoxMAVT.Text;

            SqlDataReader myReader;
            String strLenh = "exec [dbo].[sp_XoaVatTu] '" + mavt + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);
            if (myReader == null)
            {
                MessageBox.Show("Xóa vật tư thất bại!");
                return;
            }

            myReader.Read();
            MessageBox.Show("Xóa vật tư thành công!");
            flag = 3;
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            qLVTDataSet1VATTUTableAdapter.Connection.ConnectionString = DBManager.connstr;
            qLVTDataSet1VATTUTableAdapter.Fill(qLVTDataSet1.VATTU);
            System.Windows.Data.CollectionViewSource vATTUViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("vATTUViewSource")));
            vATTUViewSource.View.MoveCurrentToFirst();
        }

        private void SearchMAVT(object sender, KeyEventArgs e)
        {
            if (vattuSearch.Text != "" && vATTUDataGrid.ItemsSource != null)
            {
                string mavt = vattuSearch.Text;

                SqlDataReader myReader;
                String strLenh = "exec [dbo].[sp_TimKiemVatTu] '" + mavt + "'";
                myReader = DBManager.ExecSqlDataReader(strLenh);

                DataTable datatable3 = new DataTable();
                datatable3.Load(myReader);
                vATTUDataGrid.ItemsSource = datatable3.DefaultView;
            }
            if (vattuSearch.Text == "")
            {
                vATTUDataGrid.ItemsSource = qLVTDataSet1.VATTU.DefaultView;
            }
        }

        private void ButtonInDSVT_Click(object sender, RoutedEventArgs e)
        {
            ReportVatTu reportvattu = new ReportVatTu();
            reportvattu.ShowDialog();
        }
    }
}

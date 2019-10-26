using System;
using System.Collections.Generic;
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
using Microsoft.Reporting.WinForms;
using System.Data;
using System.Data.SqlClient;
using QLVT.DBConnection;

namespace QLVT
{
    /// <summary>
    /// Interaction logic for ReportNhanVien.xaml
    /// </summary>
    public partial class ReportNhanVien : Window
    {
        public ReportNhanVien()
        {
            InitializeComponent();
            
        }

        private void ButtonPreview_Click(object sender, RoutedEventArgs e)
        {
            DBManager.conn.Close();
            SqlDataReader myReader;
            String strLenh = "exec [dbo].[sp_DSNV]";
            myReader = DBManager.ExecSqlDataReader(strLenh);
            DataTable datatable = new DataTable();
            
            datatable.Load(myReader);

            ReportDataSource danhsach = new ReportDataSource("ReportNVDataSet",datatable);
            ReportViewNhanVien.Clear();
            ReportViewNhanVien.LocalReport.DataSources.Add(danhsach);
            ReportViewNhanVien.LocalReport.ReportEmbeddedResource = "QLVT.ReportNV.rdlc";
            ReportViewNhanVien.RefreshReport();

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
                ;
            }
        }

        private void DSNV_Load(object sender, RoutedEventArgs e)
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
    }
}

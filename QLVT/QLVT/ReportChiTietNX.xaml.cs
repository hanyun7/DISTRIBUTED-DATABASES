using Microsoft.Reporting.WinForms;
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
    /// Interaction logic for ReportChiTietNX.xaml
    /// </summary>
    public partial class ReportChiTietNX : Window
    {
        public ReportChiTietNX()
        {
            InitializeComponent();
        }

        private void ButtonPreview_Click(object sender, RoutedEventArgs e)
        {

            DateTime? nnfr = FR.SelectedDate;
            String fr = Convert.ToDateTime(nnfr).ToString("yyyy/MM/dd");
            DateTime? nnto = TO.SelectedDate;
            String to = Convert.ToDateTime(nnto).ToString("yyyy/MM/dd");
            SqlDataReader myReader;
            String strLenh;
            DataTable datatable = new DataTable();

            if (DBManager.mGroup.Equals("CONGTY"))
            {
                DBManager.mlogin = DBManager.remotelogin;
                DBManager.password = DBManager.remotepassword;
                if (DBManager.Connect() == 0)
                {
                    MessageBox.Show("Kết nối thất bại! ");
                    return;
                }

                DBManager.conn.Close();
                strLenh = "exec [dbo].[sp_ChiTietDSNX_ALL] '" + fr + "','" + to + "'";
                myReader = DBManager.ExecSqlDataReader(strLenh);
            }
            else {

                DBManager.conn.Close();
                strLenh = "exec [dbo].[sp_ChiTietDSNX] '" + fr + "','" + to + "'";
                myReader = DBManager.ExecSqlDataReader(strLenh);
            }                  

            datatable.Load(myReader);

            ReportDataSource danhsach = new ReportDataSource("ReportNXDataSet", datatable);
            ReportViewChiTietNX.Clear();
            ReportViewChiTietNX.LocalReport.DataSources.Add(danhsach);
            ReportViewChiTietNX.LocalReport.ReportEmbeddedResource = "QLVT.ReportNX.rdlc";
            ReportViewChiTietNX.RefreshReport();
        }
    }
}

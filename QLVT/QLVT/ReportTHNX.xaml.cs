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
    /// Interaction logic for ReportTHNX.xaml
    /// </summary>
    public partial class ReportTHNX : Window
    {
        public ReportTHNX()
        {
            InitializeComponent();
        }

        private void ButtonPreview_Click(object sender, RoutedEventArgs e)
        {
            DateTime? nnfr = FR.SelectedDate;
            String fr = Convert.ToDateTime(nnfr).ToString("yyyy/MM/dd");
            DateTime? nnto = TO.SelectedDate;
            String to = Convert.ToDateTime(nnto).ToString("yyyy/MM/dd");
            DBManager.conn.Close();
            SqlDataReader myReader;
            String strLenh = "exec [dbo].[sp_TongHop_NX] '" + DBManager.mGroup + "','"+ fr + "','" + to + "'";
            myReader = DBManager.ExecSqlDataReader(strLenh);
            DataTable datatable = new DataTable();

            datatable.Load(myReader);

            ReportDataSource danhsach = new ReportDataSource("ReportTHNXDataSet", datatable);
            ReportViewTHNX.Clear();
            ReportViewTHNX.LocalReport.DataSources.Add(danhsach);
            ReportViewTHNX.LocalReport.ReportEmbeddedResource = "QLVT.ReportTHNX.rdlc";
            ReportViewTHNX.RefreshReport();
        }
    }
}

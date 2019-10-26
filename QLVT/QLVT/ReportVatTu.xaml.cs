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
    /// Interaction logic for ReportVatTu.xaml
    /// </summary>
    public partial class ReportVatTu : Window
    {
        public ReportVatTu()
        {
            InitializeComponent();
        }

        private void ButtonPreview_Click(object sender, RoutedEventArgs e)
        {
            DBManager.conn.Close();
            SqlDataReader myReader;
            String strLenh = "exec [dbo].[sp_DSVT]";
            myReader = DBManager.ExecSqlDataReader(strLenh);
            DataTable datatable = new DataTable();

            datatable.Load(myReader);

            ReportDataSource danhsach = new ReportDataSource("ReportVTDataSet", datatable);
            ReportViewVatTu.Clear();
            ReportViewVatTu.LocalReport.DataSources.Add(danhsach);
            ReportViewVatTu.LocalReport.ReportEmbeddedResource = "QLVT.ReportVT.rdlc";
            ReportViewVatTu.RefreshReport();
        }
    }
}

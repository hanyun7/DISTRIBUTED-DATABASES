using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QLVT.DBConnection
{
    public class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = "HY-PC";

            string database = "QLVT";
            string username = "sa";
            string password = "52401091314";

            return DBManager.GetDBConnection(datasource, database, username, password);
        }
    }
}

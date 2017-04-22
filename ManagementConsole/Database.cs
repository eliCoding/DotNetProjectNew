using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementConsole
{
    class Database
    {
        SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(@"Server=tcp:mike-jac.database.windows.net,1433;Initial Catalog=POS;
         Persist Security Info=False;User ID=dbadmin;Password=Elmira2000;MultipleActiveResultSets=False;
       Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            conn.Open();
        }

    }
}

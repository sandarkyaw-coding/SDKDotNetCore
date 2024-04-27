using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKDotNetCore.ConsoleApp
{
    internal static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder ConnectionStringBuilder = new SqlConnectionStringBuilder()
        { 
         DataSource = ".",
         InitialCatalog = "SDKDotNetCore",
         UserID = "sa",
         Password = "sasa@123",
         TrustServerCertificate = true,
        };
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorkLibrary.Orm
{
    public class MyDbContext
    {
        private string _connectionString;
        protected SqlConnection connection;
        public MyDbContext()
        {
            _connectionString = "Server=localhost;Database=master;Trusted_Connection=True";
            connection = new SqlConnection(_connectionString);
        }
    }
}

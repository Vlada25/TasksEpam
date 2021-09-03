using DatabaseWorkLibrary.Orm;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorkLibrary
{
    public class Crud<T>
    {
        private SqlConnection _connection;
        private List<PropertyInfo> _properties;
        public Crud(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;
            _properties = typeof(T).GetProperties().ToList();
        }
        public void Create(T dbObject)
        {
            Console.WriteLine(dbObject.GetType());
        }
        public void Read(int id)
        {

        }
    }
}

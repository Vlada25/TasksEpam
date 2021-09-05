using BookLibrary;
using DatabaseWorkLibrary.Crud;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorkLibrary
{
    public class CrudMethods<T> : ICrudMethods<T> where T : BaseDBObject
    {
        private SqlConnection _connection;
        private List<PropertyInfo> _properties;

        public CrudMethods(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;
            _properties = typeof(T).GetProperties().ToList();
        }

        /// <summary>
        /// Creation object
        /// </summary>
        /// <param name="obj"> Object BaseDBObject </param>
        public void Create(T obj)
        {
            string sqlInsertCommand = $"SET IDENTITY_INSERT [{typeof(T).Name}] ON; INSERT INTO [{typeof(T).Name}] (";

            List<PropertyInfo> propertyColumns = _properties.Where(property => !property.PropertyType.IsClass || property.PropertyType == typeof(string)).ToList();

            sqlInsertCommand += string.Join(",", propertyColumns.Select(property => $"[{property.Name}]"));

            sqlInsertCommand += ")";
            sqlInsertCommand += "VALUES (";
            sqlInsertCommand += string.Join(",", propertyColumns.Select(property => $"@{property.Name}"));

            sqlInsertCommand += ");";
            sqlInsertCommand += $"SET IDENTITY_INSERT [{typeof(T).Name}] OFF;";

            var sqlCommand = new SqlCommand(sqlInsertCommand, _connection);

            foreach (PropertyInfo property in propertyColumns)
            {
                sqlCommand.Parameters.AddWithValue($"@{property.Name}", $"{property.GetValue(obj)}");
            }

            _connection.Open();
            sqlCommand.ExecuteNonQuery();
            _connection.Close();
        }

        /// <summary>
        /// Reading object from database by id
        /// </summary>
        /// <param name="id"> Object id </param>
        /// <returns> Object BaseDBObject </returns>
        public T Read(int id)
        {
            _connection.Open();
            object obj = null;

            string sqlSelectCommand = $"SELECT * FROM [{typeof(T).Name}] WHERE Id = @Id;";
            SqlCommand sqlCommand = new SqlCommand(sqlSelectCommand, _connection);

            sqlCommand.Parameters.AddWithValue("@Id", $"{id}");

            SqlDataReader reader = sqlCommand.ExecuteReader();

            int count = reader.FieldCount;

            if (reader.HasRows)
            {
                reader.Read();
                obj = BaseDBObject.CreateDBObject<T>();

                for (int i = 0; i < count; i++)
                {
                    var fieldName = reader.GetName(i);
                    var propInfo = typeof(T).GetProperty(fieldName);
                    propInfo?.SetValue(obj, reader.GetValue(i));
                }
            }

            _connection.Close();

            return (T)obj;
        }

        /// <summary>
        /// Updating database object 
        /// </summary>
        /// <param name="id"> Object id </param>
        /// <param name="obj"> Object BaseDBObject </param>
        public void Update(int id, T obj)
        {
            string sqlUpdateCommand = $"UPDATE [{typeof(T).Name}] SET ";

            List<PropertyInfo> propertyColumns = _properties.Where(property => (!property.PropertyType.IsClass || (property.PropertyType == typeof(string))) &&
                                                                               (property.Name != nameof(BaseDBObject.Id))).ToList();

            sqlUpdateCommand += string.Join(",", propertyColumns.Where(prop => (prop.Name != nameof(BaseDBObject.Id)))
                                                                .Select(property => string.Format($"[{property.Name}] = @{property.Name} ")));

            sqlUpdateCommand += $"WHERE [ID] = @{nameof(id)};";

            SqlCommand sqlCommand = new SqlCommand(sqlUpdateCommand, _connection);

            foreach (PropertyInfo property in propertyColumns)
            {
                sqlCommand.Parameters.AddWithValue($"@{property.Name}", $"{property.GetValue(obj)}");
            }

            sqlCommand.Parameters.AddWithValue($"@{nameof(id)}", $"{id}");

            _connection.Open();
            sqlCommand.ExecuteNonQuery();
            _connection.Close();
        }

        /// <summary>
        /// Delete object from database
        /// </summary>
        /// <param name="id"> Object id </param>
        public void Delete(int id)
        {
            string sqlDeleteCommand = $"DELETE FROM [{typeof(T).Name}] WHERE ID = @ID;";

            SqlCommand sqlCommand = new SqlCommand(sqlDeleteCommand, _connection);

            sqlCommand.Parameters.AddWithValue("@ID", $"{id}");

            _connection.Open();
            sqlCommand.ExecuteNonQuery();
            _connection.Close();
        }
    }
}

using BookLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorkLibrary.Orm
{
    public class MyDbSet<T> : IEnumerable<T> where T : BaseDBObject
    {
        private CrudMethods<T> _crudMethods;
        private List<T> _listDBObject;
        private SqlConnection _connection;

        public MyDbSet(SqlConnection sqlConnection)
        {
            _connection = sqlConnection ?? throw new Exception($"{nameof(SqlConnection)} cannot be null.");
            _crudMethods = new CrudMethods<T>(_connection);
            _listDBObject = ReadTable().ToList();
        }

        /// <summary>
        /// Adding object to the tabke
        /// </summary>
        /// <param name="item"> Selected item </param>
        public void Add(T item)
        {
            _crudMethods.Create(item);
            _listDBObject.Add(item);
        }

        /// <summary>
        /// Delete an object from the table
        /// </summary>
        /// <param name="id"> Object id </param>
        public void Delete(int id)
        {
            _crudMethods.Delete(id);

            var deletedModel = _listDBObject.FirstOrDefault(o => o.Id == id);
            _listDBObject.Remove(deletedModel);
        }

        /// <summary>
        /// Updating an object
        /// </summary>
        /// <param name="id"> Object id </param>
        /// <param name="item"> New properties </param>
        public void Update(int id, T item)
        {
            _crudMethods.Update(id, item);

            var updatedModel = _listDBObject.FirstOrDefault(o => o.Id == id);
            updatedModel = item;
        }

        /// <summary>
        /// Reading object
        /// </summary>
        /// <param name="id"> Object id </param>
        /// <returns></returns>
        public T Read(int id) => _listDBObject.FirstOrDefault(o => o.Id == id);

        public IEnumerator<T> GetEnumerator() => _listDBObject.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Reading table from database
        /// </summary>
        /// <returns> List of data </returns>
        public IEnumerable<T> ReadTable()
        {
            _connection.Open();

            var sqlSelectCommand = $"SELECT * FROM [{typeof(T).Name}]";
            var sqlCommand = new SqlCommand(sqlSelectCommand, _connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            var list = new List<T>();
            var obj = BaseDBObject.CreateDBObject<T>();

            int columnsNumber = reader.FieldCount;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (var i = 0; i < columnsNumber; i++)
                    {
                        string fieldName = reader.GetName(i);
                        PropertyInfo propInfo = obj.GetType().GetProperty(fieldName);

                        if (!(reader.GetValue(i) is DBNull))
                        {
                            propInfo?.SetValue(obj, reader.GetValue(i));
                        }
                    }

                    list.Add((T)obj);
                    obj = BaseDBObject.CreateDBObject(typeof(T).FullName);
                }
            }

            _connection.Close();

            return list;
        }

    }
}

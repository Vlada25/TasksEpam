using BookLibrary;
using DatabaseWorkLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;

namespace UnitTestTask5
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateTest()
        {
            string connectionString = @"Server=localhost;Database=master;Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);

            CrudMethods<User> crudMethods = new CrudMethods<User>(connection);
            var user = new User(1, "Leonenko", "Vladislava", "Jurievna", false, new DateTime(2003, 02, 25));

            crudMethods.Create(user);

            User result = crudMethods.Read(user.Id);
            crudMethods.Delete(user.Id);

            Assert.AreEqual(result, user);
        }
    }
}

using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace ExecutionTask5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string DBName = "librarydb";
            string connectionString = $"Server=localhost;Database={DBName};Trusted_Connection=True;";
            //await Task.Run(() => CreateDB(DBName));

        }

        static async Task CreateDB(string name)
        {
            string connectionString = "Server=localhost;Database=master;Trusted_Connection=True;";
            string creationString = $"CREATE DATABASE {name}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("Подключение открыто");

                SqlCommand command = new SqlCommand();

                command.CommandText = creationString;
                command.Connection = connection;
                await command.ExecuteNonQueryAsync();

                Console.WriteLine("db was created");
            }
            Console.WriteLine("Подключение закрыто...");
        }
    }
}

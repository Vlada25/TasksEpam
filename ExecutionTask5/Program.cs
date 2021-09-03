using BookLibrary;
using DatabaseWorkLibrary;
using DatabaseWorkLibrary.Orm;
using Microsoft.Data.SqlClient;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ExecutionTask5
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryDb db = new LibraryDb();
            Crud<DbSet> crud = new Crud<DbSet>(db.Connection);
            crud.Create(db.Users);
        }
    }
}

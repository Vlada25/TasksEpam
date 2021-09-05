using BookLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWorkLibrary.Crud
{
    public interface ICrudMethods<T> where T : BaseDBObject
    {
        void Create(T obj);
        T Read(int id);
        void Update(int id, T obj);
        void Delete(int id);
    }
}

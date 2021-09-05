using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public class User : BaseDBObject
    {
        public string Surname { get; }
        public string Name { get; }
        public string Patronymic { get; }
        public bool Sex { get; }
        public DateTime Birthday { get; }

        public User(int id, string name, string surname, string patronymic, bool sex)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Sex = sex;
        }
    }
}

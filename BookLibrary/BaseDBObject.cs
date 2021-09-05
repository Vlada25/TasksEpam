using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public abstract class BaseDBObject
    {
        public int Id { get; set; }

        /// <summary>
        /// Creating BaseDBObject object
        /// </summary>
        /// <returns></returns>
        public static BaseDBObject CreateDBObject<T>() where T : BaseDBObject
        {
            return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Creating an object
        /// </summary>
        /// <param name="fullName"> Full name of object </param>
        /// <returns> Object BaseDBObject </returns>
        public static BaseDBObject CreateDBObject(string fullName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetType(fullName).FullName;
            return (BaseDBObject)Activator.CreateInstanceFrom(assembly.Location, type).Unwrap();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

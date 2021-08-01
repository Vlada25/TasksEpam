using System;
using System.Collections.Generic;

namespace ClassLibraryBistro
{
    public class Dish : IDish
    {
        public string Name { get; }
        public Manager.Menu Type { get; }
        public int CountOfPortions { get; }
        public int NeedPortions;
        public bool IsDishDone;
        public Dish(string name, Manager.Menu type, int countOfPortions)
        {
            Name = name;
            Type = type;
            CountOfPortions = countOfPortions;
            NeedPortions = CountOfPortions;
            IsDishDone = false;
        }
        public override string ToString()
        {
            return $"{Type}: {Name} - {CountOfPortions}";
        }

        public override bool Equals(object obj)
        {
            return obj is Dish dish &&
                   Name == dish.Name &&
                   Type == dish.Type &&
                   CountOfPortions == dish.CountOfPortions &&
                   NeedPortions == dish.NeedPortions &&
                   IsDishDone == dish.IsDishDone;
        }

        public override int GetHashCode()
        {
            int hashCode = 357897830;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + CountOfPortions.GetHashCode();
            hashCode = hashCode * -1521134295 + NeedPortions.GetHashCode();
            hashCode = hashCode * -1521134295 + IsDishDone.GetHashCode();
            return hashCode;
        }
    }
}

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
    }
}

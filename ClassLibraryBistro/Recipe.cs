using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryBistro
{
    public class Recipe
    {
        public struct KitchenDirections
        {
            public List<string> NamesOfIngredients;
            public ChiefCooker.CookOperations CookOperations;
            public int SpentMinutes;
            public SizesOfPeaces SizeOfPieces;
        }
        public struct Ingredient
        {
            public string Name;
            public double Weight;
            public Ingredient(string name, double weight)
            {
                Name = name;
                Weight = weight;
            }
        }
        public struct SizesOfPeaces
        {
            public int StartSize;
            public int EndSize;
        }

        public string Name;
        public Manager.Menu DishType;
        public int CountOfPortions;
        public bool IsRecipeCompleted = false;
        public int CountOfOperations = 0;
        public string WrittenRecipe = "";
        public List<Ingredient> Ingredients = new List<Ingredient>();
        public List<KitchenDirections> Directions = new List<KitchenDirections>();

        public Recipe() { }
        public Recipe(string name, Manager.Menu type, int countOfPortions)
        {
            Name = name;
            DishType = type;
            CountOfPortions = countOfPortions;
            WrittenRecipe += $"\nRecipe of {Convert.ToString(type).ToLower()} {name}";
        }
        public override string ToString()
        {
            if (!IsRecipeCompleted)
            {
                return $"{WrittenRecipe}\n...INCOMPLETED...";
            }
            return WrittenRecipe;
        }
    }
}

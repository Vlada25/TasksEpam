using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryBistro
{
    public class Recipe
    {
        public struct KitchenActions
        {
            public List<string> NamesOfIngredients;
            public ChiefCooker.CookOperations CookOperation;
            public int Minutes;
            public ChiefCooker.KitchenDevices Device;
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
        public bool IsRecipeCompleted = false;
        public int CountOfOperations = 0;
        public int SpentMinutes = 0;
        public double PriceOfDish;
        public string WrittenRecipe = "";
        public List<Ingredient> Ingredients = new List<Ingredient>();
        public List<KitchenActions> Actions = new List<KitchenActions>();

        public Recipe() { }
        public Recipe(string name, Manager.Menu type)
        {
            Name = name;
            DishType = type;
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

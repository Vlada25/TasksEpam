using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ClassLibraryBistro;

namespace ExecutionTask3
{
    class FileReader
    {
        const string FILE_PRODUCTS = @"E:\Epam May 2021\TasksEpam\Products.xml",
            FILE_RECIPES = @"E:\Epam May 2021\TasksEpam\Recipes.xml";

        public static List<ChiefCooker.Product> ReadProducts()
        {
            string name;
            double price = 0;
            int startTemp = 0, endTemp = 0;
            List<ChiefCooker.Product> products = new List<ChiefCooker.Product>();

            XmlDocument document = new XmlDocument();
            document.Load(FILE_PRODUCTS);
            XmlElement element = document.DocumentElement;

            foreach (XmlNode xnode in element)
            {
                name = xnode.Attributes.GetNamedItem("name").Value;
                foreach (XmlNode childnode in xnode)
                {
                    if (childnode.Name == "price")
                    {
                        price = Convert.ToDouble(childnode.InnerText);
                    }
                    else if (childnode.Name == "startTemperature")
                    {
                        startTemp = Convert.ToInt32(childnode.InnerText);
                    }
                    else if (childnode.Name == "endTemperature")
                    {
                        endTemp = Convert.ToInt32(childnode.InnerText);
                    }
                }
                products.Add(new ChiefCooker.Product(name, price, new ChiefCooker.StorageConditions(startTemp, endTemp)));
            }
            return products;
        }
        public static List<Recipe.Ingredient> ReadRecipeIngredients(string name)
        {
            List<Recipe.Ingredient> ingredients = new List<Recipe.Ingredient>();

            XmlDocument document = new XmlDocument();
            document.Load(FILE_RECIPES);
            XmlElement element = document.DocumentElement;

            foreach (XmlNode xnode in element)
            {
                if (xnode.Attributes.GetNamedItem("name").Value == name)
                {
                    foreach (XmlNode childnode in xnode)
                    {
                        string ingredientName = childnode.Attributes.GetNamedItem("name").Value;
                        double weight = Convert.ToDouble(childnode.Attributes.GetNamedItem("weight").Value);
                        ingredients.Add(new Recipe.Ingredient(ingredientName, weight));
                    }
                }
            }
            return ingredients;
        }
    }
}

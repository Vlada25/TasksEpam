using System;
using System.Collections.Generic;
using static ClassLibraryBistro.ChiefCooker;
using static ClassLibraryBistro.Recipe;
using System.Text;
using System.Xml;
using ClassLibraryBistro;

namespace ExecutionTask3
{
    class FileReader
    {
        const string FILE_PRODUCTS = @"E:\Epam May 2021\TasksEpam\Products.xml",
            FILE_RECIPES = @"E:\Epam May 2021\TasksEpam\Recipes.xml";

        /// <summary>
        /// Reading list of products from file
        /// </summary>
        /// <returns> List of products </returns>
        public static List<Product> ReadProducts()
        {
            string name;
            double price = 0;
            int startTemp = 0, endTemp = 0;
            List<Product> products = new List<Product>();

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
                products.Add(new Product(name, price, new StorageConditions(startTemp, endTemp)));
            }
            return products;
        }

        /// <summary>
        /// Reading recipe ingredients from file
        /// </summary>
        /// <param name="name"> Name of recipe </param>
        /// <returns> List of ingredients </returns>
        public static List<Ingredient> ReadRecipeIngredients(string name)
        {
            List<Ingredient> ingredients = new List<Ingredient>();

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
                        ingredients.Add(new Ingredient(ingredientName, weight));
                    }
                }
            }
            return ingredients;
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

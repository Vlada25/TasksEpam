using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ClassLibraryBistro;

namespace ExecutionTask3
{
    class FileReader
    {
        const string FILENAME = @"E:\Epam May 2021\TasksEpam\Products.xml";

        public static List<ChiefCooker.Product> ReadProducts()
        {
            string name;
            double price = 0;
            int startTemp = 0, endTemp = 0;
            List<ChiefCooker.Product> products = new List<ChiefCooker.Product>();

            XmlDocument document = new XmlDocument();
            document.Load(FILENAME);
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
    }
}

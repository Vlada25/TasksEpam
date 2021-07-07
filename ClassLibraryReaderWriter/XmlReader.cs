using ClassLibraryCarPark;
using ClassLibraryCarPark.Types_of_trailers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ClassLibraryReaderWriter
{
    public class XmlReader
    {
        public void ReadInfo(List<Semitrailer> listOfTrailers)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(@"E:\Epam May 2021\TasksEpam\CarPark1.xml");
            XmlElement element = xml.DocumentElement;
            foreach (XmlNode xnode in element)
            {
                //Console.WriteLine(xnode.Attributes.GetNamedItem("Name").Value);
                double Weight = 0, Volume = 0;
                string type = "";
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "maxWeight")
                    {
                        Weight = Convert.ToDouble(childnode.InnerText);
                    }
                    if (childnode.Name == "maxVolume")
                    {
                        Volume = Convert.ToDouble(childnode.InnerText);
                    }
                    if (childnode.Name == "type")
                    {
                        type = childnode.InnerText;
                    }
                }
                switch (type)
                {
                    case "Refrigerator" : 
                        listOfTrailers.Add(new Refrigerator(Weight, Volume));
                        break;
                    case "TiltSemitrailer":
                        listOfTrailers.Add(new TiltSemitrailer(Weight, Volume));
                        break;
                    case "TankTruck":
                        listOfTrailers.Add(new TankTruck(Weight, Volume));
                        break;
                }
            }
        }
    }
}

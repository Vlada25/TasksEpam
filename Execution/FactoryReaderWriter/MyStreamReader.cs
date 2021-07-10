using ClassLibraryCarPark;
using ClassLibraryCarPark.Types_of_trailers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Execution.FactoryReaderWriter
{
    class MyStreamReader : MyReader
    {
        const string FILENAME = @"E:\Epam May 2021\Vlada25\TasksEpam\CarPark1.xml";

        public override void Read(List<Semitrailer> listOfTrailers, List<TruckTractor> listOfTractors, List<Cargo> listOfCargo)
        {
            string contents;
            XmlDocument xdoc = new XmlDocument();
            using (StreamReader streamReader = new StreamReader(FILENAME, Encoding.UTF8))
            {
                contents = streamReader.ReadToEnd();
            }
            xdoc.LoadXml(contents);
            XmlElement xmlElement = xdoc.DocumentElement;
            foreach (XmlNode xnode in xmlElement)
            {
                if (xnode.Name == "trailer")
                {
                    AddTrailer(xnode, listOfTrailers);
                }
                else if (xnode.Name == "tractor")
                {
                    AddTractor(xnode, listOfTractors);
                }
                else if (xnode.Name == "cargo")
                {
                    AddCargo(xnode, listOfCargo);
                }
            }
        }

        /// <summary>
        /// Reading information about trailer and adding it to the list
        /// </summary>
        /// <param name="xnode"> Allows to read information </param>
        /// <param name="listOfTrailers"> All trailers </param>
        private void AddTrailer(XmlNode xnode, List<Semitrailer> listOfTrailers)
        {
            string trailerType = xnode.Attributes.GetNamedItem("type").Value;
            double weight = Convert.ToDouble(xnode.Attributes.GetNamedItem("maxWeight").Value);
            double volume = Convert.ToDouble(xnode.Attributes.GetNamedItem("maxVolume").Value);
            switch (trailerType)
            {
                case "Refrigerator":
                    listOfTrailers.Add(new Refrigerator(weight, volume));
                    break;
                case "TankTruck":
                    listOfTrailers.Add(new TankTruck(weight, volume));
                    break;
                case "TiltSemitrailer":
                    listOfTrailers.Add(new TiltSemitrailer(weight, volume));
                    break;
                default: throw new Exception("Invalid type");
            }
        }

        /// <summary>
        /// Reading information about tractor and adding it to the list
        /// </summary>
        /// <param name="xnode"> Allows to read information </param>
        /// <param name="listOfTractors"> All tractors </param>
        private void AddTractor(XmlNode xnode, List<TruckTractor> listOfTractors)
        {
            string model = xnode.Attributes.GetNamedItem("model").Value;
            double fuelConsumption = Convert.ToDouble(xnode.Attributes.GetNamedItem("fuelConsumption").Value);
            double carryingCapacity = Convert.ToDouble(xnode.Attributes.GetNamedItem("carryingCapacity").Value);
            listOfTractors.Add(new TruckTractor(model, fuelConsumption, carryingCapacity));
        }

        /// <summary>
        /// Reading information about cargo and adding it to the list
        /// </summary>
        /// <param name="xnode"> Allows to read information </param>
        /// <param name="listOfCargo"> All cargo </param>
        private void AddCargo(XmlNode xnode, List<Cargo> listOfCargo)
        {
            string name = xnode.Attributes.GetNamedItem("name").Value;
            double weight = Convert.ToDouble(xnode.Attributes.GetNamedItem("weight").Value);
            double volume = Convert.ToDouble(xnode.Attributes.GetNamedItem("volume").Value);
            bool isLiquid = Convert.ToBoolean(xnode.Attributes.GetNamedItem("isLiquid").Value);

            if (!Enum.TryParse(Convert.ToString(xnode.Attributes.GetNamedItem("type").Value), out Cargo.CargoTypes cargoType))
            {
                throw new Exception("Invalid type");
            }

            Cargo cargo;
            if (xnode.Attributes.Count == 7)
            {
                int startTemperature = Convert.ToInt32(xnode.Attributes.GetNamedItem("startTemp").Value);
                int endTemperature = Convert.ToInt32(xnode.Attributes.GetNamedItem("endTemp").Value);
                cargo = new Cargo(name, weight, volume, isLiquid, startTemperature, endTemperature, cargoType);
            }
            else
            {
                cargo = new Cargo(name, weight, volume, isLiquid, cargoType);
            }
            listOfCargo.Add(cargo);
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

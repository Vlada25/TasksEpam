using ClassLibraryCarPark;
using ClassLibraryCarPark.Types_of_trailers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Execution.FactoryReaderWriter
{
    class MyXmlReader : MyReader
    {
        const string FILENAME = @"E:\Epam May 2021\Vlada25\TasksEpam\CarPark1.xml";
        public override void Read(List<Semitrailer> listOfTrailers, List<TruckTractor> listOfTractors, List<Cargo> listOfCargo)
        {
            XmlReader xmlReader = XmlReader.Create(FILENAME);
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element))
                {
                    if (xmlReader.Name == "trailer")
                    {
                        AddTrailer(xmlReader, listOfTrailers);
                    }
                    else if (xmlReader.Name == "tractor")
                    {
                        AddTractor(xmlReader, listOfTractors);
                    }
                    else if (xmlReader.Name == "cargo")
                    {
                        AddCargo(xmlReader, listOfCargo);
                    }
                }
            }
        }

        /// <summary>
        /// Reading information about trailer and adding it to the list
        /// </summary>
        /// <param name="xmlReader"> Allows to read information </param>
        /// <param name="listOfTrailers"> All trailers </param>
        private void AddTrailer(XmlReader xmlReader, List<Semitrailer> listOfTrailers)
        {
            if (xmlReader.HasAttributes)
            {
                double weight = Convert.ToDouble(xmlReader.GetAttribute("maxWeight"));
                double volume = Convert.ToDouble(xmlReader.GetAttribute("maxVolume"));
                string trailerType = xmlReader.GetAttribute("type");
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
        }

        /// <summary>
        /// Reading information about tractor and adding it to the list
        /// </summary>
        /// <param name="xmlReader"> Allows to read information </param>
        /// <param name="listOfTractors"> All tractors </param>
        private void AddTractor(XmlReader xmlReader, List<TruckTractor> listOfTractors)
        {
            if (xmlReader.HasAttributes)
            {
                string model = xmlReader.GetAttribute("model");
                double fuelConsumption = Convert.ToDouble(xmlReader.GetAttribute("fuelConsumption"));
                double carryingCapacity = Convert.ToDouble(xmlReader.GetAttribute("carryingCapacity"));
                listOfTractors.Add(new TruckTractor(model, fuelConsumption, carryingCapacity));
            }
        }

        /// <summary>
        /// Reading information about cargo and adding it to the list
        /// </summary>
        /// <param name="xmlReader"> Allows to read information </param>
        /// <param name="listOfCargo"> All cargo </param>
        private void AddCargo(XmlReader xmlReader, List<Cargo> listOfCargo)
        {
            if (xmlReader.HasAttributes)
            {
                string name = xmlReader.GetAttribute("name");
                double weight = Convert.ToDouble(xmlReader.GetAttribute("weight"));
                double volume = Convert.ToDouble(xmlReader.GetAttribute("volume"));
                bool isLiquid = Convert.ToBoolean(xmlReader.GetAttribute("isLiquid"));
                
                if (!Enum.TryParse(Convert.ToString(xmlReader.GetAttribute("type")), out Cargo.CargoTypes cargoType))
                {
                    throw new Exception("Invalid type");
                }

                Cargo cargo;
                if (xmlReader.AttributeCount == 7)
                {
                    int startTemperature = Convert.ToInt32(xmlReader.GetAttribute("startTemp"));
                    int endTemperature = Convert.ToInt32(xmlReader.GetAttribute("endTemp"));
                    cargo = new Cargo(name, weight, volume, isLiquid, startTemperature, endTemperature, cargoType);
                }
                else
                {
                    cargo = new Cargo(name, weight, volume, isLiquid, cargoType);
                }
                listOfCargo.Add(cargo);
            }
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

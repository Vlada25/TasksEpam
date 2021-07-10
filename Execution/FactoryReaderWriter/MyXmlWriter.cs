using ClassLibraryCarPark;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Execution.FactoryReaderWriter
{

    class MyXmlWriter : MyWriter
    {
        const string FILENAME = @"E:\Epam May 2021\Vlada25\TasksEpam\CarPark1.xml";
        readonly XmlWriter xmlWriter = XmlWriter.Create(FILENAME);

        public override void Write(List<Semitrailer> listOfTrailers, List<TruckTractor> listOfTractors, List<Cargo> listOfCargo)
        {
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("carpark");

            WriteTrailer(xmlWriter, listOfTrailers);
            WriteTractor(xmlWriter, listOfTractors);
            WriteCargo(xmlWriter, listOfCargo);

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        /// <summary>
        /// Writing trailer to the new xml file
        /// </summary>
        /// <param name="xmlWriter"> Writes information </param>
        /// <param name="listOfTrailers"> All trailers </param>
        private void WriteTrailer(XmlWriter xmlWriter, List<Semitrailer> listOfTrailers)
        {
            foreach (Semitrailer trailer in listOfTrailers)
            {
                xmlWriter.WriteStartElement("trailer");
                xmlWriter.WriteAttributeString("type", Convert.ToString(trailer.GetTypeOfTrailer()));
                xmlWriter.WriteAttributeString("maxWeight", Convert.ToString(trailer.MaxWeight));
                xmlWriter.WriteAttributeString("maxVolume", Convert.ToString(trailer.MaxVolume));
                xmlWriter.WriteEndElement();
            }
        }

        /// <summary>
        /// Writing tractor to the new xml file
        /// </summary>
        /// <param name="xmlWriter"> Writes information </param>
        /// <param name="listOfTractors"> All tractors </param>
        private void WriteTractor(XmlWriter xmlWriter, List<TruckTractor> listOfTractors)
        {
            foreach (TruckTractor tractor in listOfTractors)
            {
                xmlWriter.WriteStartElement("tractor");
                xmlWriter.WriteAttributeString("model", tractor.Model);
                xmlWriter.WriteAttributeString("fuelConsumption", Convert.ToString(tractor.CountFuelConsumption()));
                xmlWriter.WriteAttributeString("carryingCapacity", Convert.ToString(tractor.CarryingCapacity));
                xmlWriter.WriteEndElement();
            }
        }

        /// <summary>
        /// Writing cargo to the new xml file
        /// </summary>
        /// <param name="xmlWriter"> Writes information </param>
        /// <param name="listOfCargo"> All cargo </param>
        private void WriteCargo(XmlWriter xmlWriter, List<Cargo> listOfCargo)
        {
            foreach (Cargo cargo in listOfCargo)
            {
                xmlWriter.WriteStartElement("cargo");
                xmlWriter.WriteAttributeString("name", cargo.Name);
                xmlWriter.WriteAttributeString("weight", Convert.ToString(cargo.Weight));
                xmlWriter.WriteAttributeString("volume", Convert.ToString(cargo.Volume));
                xmlWriter.WriteAttributeString("isLiquid", Convert.ToString(cargo.IsLiquid));
                xmlWriter.WriteAttributeString("type", Convert.ToString(cargo.TypeOfCargo));
                if (cargo.WasTemperatureSet)
                {
                    xmlWriter.WriteAttributeString("startTemp", Convert.ToString(cargo.StartTemperature));
                    xmlWriter.WriteAttributeString("endTemp", Convert.ToString(cargo.EndTemperature));
                }
                xmlWriter.WriteEndElement();
            }
        }

        public override bool Equals(object obj)
        {
            return obj is MyXmlWriter writer &&
                   base.Equals(obj) &&
                   EqualityComparer<XmlWriter>.Default.Equals(xmlWriter, writer.xmlWriter);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), xmlWriter);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

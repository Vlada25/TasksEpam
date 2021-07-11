﻿using ClassLibraryCarPark;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Execution.FactoryReaderWriter
{
    class MyStreamWriter : MyWriter
    {
        const string FILENAME = @"E:\Epam May 2021\Vlada25\TasksEpam\NewCarPark.xml";
        public override void Write(List<Semitrailer> listOfTrailers, List<TruckTractor> listOfTractors, List<Cargo> listOfCargo)
        {
            FileStream fileStream;
            StreamWriter streamWriter;
            XmlTextWriter xmlTextWriter;

            try
            {
                fileStream = new FileStream(FILENAME, FileMode.Create, FileAccess.Write, FileShare.None);

                streamWriter = new StreamWriter(fileStream);
                xmlTextWriter = new XmlTextWriter(streamWriter)
                {
                    Formatting = Formatting.Indented
                };
                xmlTextWriter.WriteStartDocument();
                xmlTextWriter.WriteStartElement("carpark");

                WriteTrailer(xmlTextWriter, listOfTrailers);
                WriteTractor(xmlTextWriter, listOfTractors);
                WriteCargo(xmlTextWriter, listOfCargo);

                xmlTextWriter.WriteEndDocument();
                xmlTextWriter.Flush();
                xmlTextWriter.Close();
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Writing trailer to the new xml file
        /// </summary>
        /// <param name="xmlTextWriter"> Writes information </param>
        /// <param name="listOfTrailers"> All trailers </param>
        private void WriteTrailer(XmlTextWriter xmlTextWriter, List<Semitrailer> listOfTrailers)
        {
            foreach (Semitrailer trailer in listOfTrailers)
            {
                string type = Convert.ToString(trailer.GetTypeOfTrailer());
                string maxWeight = Convert.ToString(trailer.MaxWeight);
                string maxVolume = Convert.ToString(trailer.MaxVolume);

                xmlTextWriter.WriteStartElement("trailer");
                xmlTextWriter.WriteAttributeString("type", type);
                xmlTextWriter.WriteAttributeString("maxWeight", maxWeight);
                xmlTextWriter.WriteAttributeString("maxVolume", maxVolume);
                if (trailer.ListOfCargo.Count != 0)
                {
                    string trailerCargo = "";
                    foreach (Cargo cargo in trailer.ListOfCargo)
                    {
                        trailerCargo += cargo.Name + " ";
                    }
                    xmlTextWriter.WriteAttributeString("cargo", trailerCargo);
                }
                xmlTextWriter.WriteEndElement();
            }
        }

        /// <summary>
        /// Writing tractor to the new xml file
        /// </summary>
        /// <param name="xmlTextWriter"> Writes information </param>
        /// <param name="listOfTractors"> All tractors </param>
        private void WriteTractor(XmlTextWriter xmlTextWriter, List<TruckTractor> listOfTractors)
        {
            foreach (TruckTractor tractor in listOfTractors)
            {
                string model = Convert.ToString(tractor.Model);
                string fuelConsumption = Convert.ToString(tractor.CountFuelConsumption());
                string carryingCapacity = Convert.ToString(tractor.CarryingCapacity);

                xmlTextWriter.WriteStartElement("tractor");
                xmlTextWriter.WriteAttributeString("model", model);
                xmlTextWriter.WriteAttributeString("fuelConsumption", fuelConsumption);
                xmlTextWriter.WriteAttributeString("carryingCapacity", carryingCapacity);
                xmlTextWriter.WriteEndElement();
            }
        }

        /// <summary>
        /// Writing cargo to the new xml file
        /// </summary>
        /// <param name="xmlTextWriter"> Writes information </param>
        /// <param name="listOfCargo"> All cargo </param>
        private void WriteCargo(XmlTextWriter xmlTextWriter, List<Cargo> listOfCargo)
        {
            foreach (Cargo cargo in listOfCargo)
            {
                string name = cargo.Name;
                string weight = Convert.ToString(cargo.Weight);
                string volume = Convert.ToString(cargo.Volume);
                string isLiquid = Convert.ToString(cargo.IsLiquid);
                string type = Convert.ToString(cargo.TypeOfCargo);

                xmlTextWriter.WriteStartElement("cargo");
                xmlTextWriter.WriteAttributeString("name", name);
                xmlTextWriter.WriteAttributeString("weight", weight);
                xmlTextWriter.WriteAttributeString("volume", volume);
                xmlTextWriter.WriteAttributeString("isLiquid", isLiquid);
                xmlTextWriter.WriteAttributeString("type", type);

                if (cargo.WasTemperatureSet)
                {
                    string startTemp = Convert.ToString(cargo.StartTemperature);
                    string endTemp = Convert.ToString(cargo.EndTemperature);

                    xmlTextWriter.WriteAttributeString("startTemp", startTemp);
                    xmlTextWriter.WriteAttributeString("endTemp", endTemp);
                }

                xmlTextWriter.WriteEndElement();
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

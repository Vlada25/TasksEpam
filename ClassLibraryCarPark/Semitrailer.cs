using System;
using System.Collections.Generic;

namespace ClassLibraryCarPark
{
    public abstract class Semitrailer : ITrailer
    {
        public enum TypesOfTrailers
        {
            TankTruck,
            Refrigerator,
            TiltSemitrailer
        }
        public string Number;
        public TruckTractor JoinedTractor;
        private double _freeMass;
        private double _freeVolume;
        protected TypesOfTrailers typeOfTrailer;
        protected static List<string> existNumbers = new List<string>();
        public List<Cargo> ListOfCargo = new List<Cargo>();
        public double MaxWeight { get; }
        public double MaxVolume { get; }
        public Semitrailer(double maxWeight, double maxVolume)
        {
            Number = GenerateNumber();
            existNumbers.Add(Number);
            MaxVolume = maxVolume;
            MaxWeight = maxWeight;
            _freeMass = maxWeight;
            _freeVolume = maxVolume;
        }
        public abstract void LoadTrailer(Cargo cargo);
        public TypesOfTrailers GetTypeOfTrailer()
        {
            return typeOfTrailer;
        }
        public double GetWeihgtOfAllCargo()
        {
            return MaxWeight - _freeMass;
        }
        public void UnloadAll()
        {
            _freeMass = MaxWeight;
            _freeVolume = MaxVolume;
            ListOfCargo.Clear();
        }
        public void UnloadTrailer(Cargo cargo)
        {
            if (!ListOfCargo.Contains(cargo))
            {
                throw new Exception("There is no such load in this trailer");
            }
            ListOfCargo.Remove(cargo);
            _freeMass += cargo.Weight;
            _freeVolume += cargo.Volume;
        }
        public void UnloadTrailer(Cargo cargo, int percentOfCargo)
        {
            if (!ListOfCargo.Contains(cargo))
            {
                throw new Exception("There is no such load in this trailer");
            }
            if (percentOfCargo > 100)
            {
                throw new Exception("Percentage can't be > 100");
            }
            else if (percentOfCargo == 100)
            {
                ListOfCargo.Remove(cargo);
            }
            _freeMass += cargo.Weight * percentOfCargo / 100;
            _freeVolume += cargo.Volume * percentOfCargo / 100;
        }
        public void JoingWithTractor(TruckTractor tractor)
        {
            if (!tractor.isFree)
            {
                throw new Exception("This tractor is already taken");
            }
            if (tractor.carryingCapacity < MaxWeight - _freeMass)
            {
                throw new Exception("Carrying capacity of the tractor must be no less than Weight of the trailer");
            }
            JoinedTractor = tractor;
            tractor.isFree = false;
        }
        public void UnhookFromTractor()
        {
            if (JoinedTractor == null)
            {
                throw new Exception("The trailer is already free");
            }
            JoinedTractor.isFree = true;
            JoinedTractor = null;
        }
        protected void ChangeWeightAndVolume(double Weight, double Volume)
        {
            if (_freeMass < Weight || _freeVolume < Volume)
            {
                throw new Exception("Not enough space");
            }
            _freeMass -= Weight;
            _freeVolume -= Volume;
        }
        private string GenerateNumber()
        {
            string res;
            Random rand = new Random();
            int x = rand.Next(0, 10000);
            if (x < 10)
            {
                res = "000" + x;
            }
            else if (x < 100)
            {
                res = "00" + x;
            }
            else if (x < 1000)
            {
                res = "0" + x;
            }
            else
            {
                res = x + "";
            }
            if (existNumbers.Contains(res))
            {
                throw new Exception("This Number is already exist");
            }
            return res;
        }
        public override string ToString()
        {
            string result = "";
            result += $"Trailer #{Number}\nType of trailer: {typeOfTrailer}\n" +
                $"Carrying capacity: {MaxWeight}\nMaximum Volume: {MaxVolume}\nCargo: ";
            if (ListOfCargo.Count == 0)
            {
                result += "trailer is empty";
            }
            else
            {
                for (int i = 0; i < ListOfCargo.Count; i++)
                {
                    result += ListOfCargo[i].ToString();
                }
            }
            result += $"\nFree space:\n\tweight: {Math.Round(_freeMass / MaxWeight * 100, 2)}%" 
                + $"\n\tvolume: {Math.Round(_freeVolume / MaxVolume * 100, 2)}%";
            if (JoinedTractor != null)
            {
                result += $"\nJoined with tractor #{JoinedTractor.Number}";
            }
            result += "\n";
            return result;
        }

        public override bool Equals(object obj)
        {
            return obj is Semitrailer semitrailer &&
                   Number == semitrailer.Number &&
                   EqualityComparer<TruckTractor>.Default.Equals(JoinedTractor, semitrailer.JoinedTractor) &&
                   _freeMass == semitrailer._freeMass &&
                   _freeVolume == semitrailer._freeVolume &&
                   typeOfTrailer == semitrailer.typeOfTrailer &&
                   EqualityComparer<List<Cargo>>.Default.Equals(ListOfCargo, semitrailer.ListOfCargo) &&
                   MaxWeight == semitrailer.MaxWeight &&
                   MaxVolume == semitrailer.MaxVolume;
        }

        public override int GetHashCode()
        {
            int hashCode = 958523410;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Number);
            hashCode = hashCode * -1521134295 + EqualityComparer<TruckTractor>.Default.GetHashCode(JoinedTractor);
            hashCode = hashCode * -1521134295 + _freeMass.GetHashCode();
            hashCode = hashCode * -1521134295 + _freeVolume.GetHashCode();
            hashCode = hashCode * -1521134295 + typeOfTrailer.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Cargo>>.Default.GetHashCode(ListOfCargo);
            hashCode = hashCode * -1521134295 + MaxWeight.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxVolume.GetHashCode();
            return hashCode;
        }
    }
}

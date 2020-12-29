using System;

namespace ProjetSimulationReseaux
{
    public abstract class Fuel : IMarket
    {
        internal double BasePrice;    //$/uF
        public string Name;
        public double CurrentPrice; //$/uF
        public double PwDensity;    //MW/uF
        public double CO2Density;   //CO2/uF
        private static readonly Random Random = new Random();

        public double RandomValue { get; set; }
        public double PriceFactor { get; set; } = 1;

        public Fuel(string name)
        {
            Name = name;
        }

        public void RandomizePriceFactor()
        {
            RandomValue = Random.NextDouble();
            if (RandomValue >= 0.5)
            {
                PriceFactor += (RandomValue - 0.5) / 20;
                if (PriceFactor > 4)
                {
                    PriceFactor = 4;
                }
            }
            else
            {
                PriceFactor -= RandomValue / 20;
                if (PriceFactor < 0.25)
                {
                    PriceFactor = 0.2;
                }
            }
        }

        public void Update(int timePassed)
        {          
            UpdatePrice(timePassed);
        }

        public void UpdatePrice(int timePassed)
        {
            RandomizePriceFactor();
            CurrentPrice = BasePrice * PriceFactor;
        }
        public override string ToString()
        {
            return Name;
        }
    }

    public class Coal : Fuel
    {
        public Coal(string name) : base(name)
        {
            BasePrice = 1;      //$/uF
            CurrentPrice = 0;       //$/uF
            PwDensity = 2.5;    //MW/uF
            CO2Density = 3;   //CO2/uF
        }
    }

    public class Gas : Fuel
    {
        public Gas(string name) : base(name)
        {
            BasePrice = 2.5;      //$/uF
            CurrentPrice = 0;       //$/uF
            PwDensity = 5;    //MW/uF
            CO2Density = 1;   //CO2/uF
        }
    }

    public class Uranium : Fuel
    {
        public Uranium(string name) : base(name)
        {
            BasePrice = 150;      //$/uF
            CurrentPrice = 0;       //$/uF
            PwDensity = 1000;    //MW/uF
            CO2Density = 0;   //CO2/uF
        }
    }
}
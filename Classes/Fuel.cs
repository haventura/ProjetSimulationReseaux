using System;

namespace ProjetSimulationReseaux
{
    /// <summary>
    /// The abstract Fuel Super-Class. Implement the IMarket Interface.
    /// See <see cref="Grid"/>, <see cref="FueledPlant"/>, <see cref="IMarket"/>.
    /// </summary>
    public abstract class Fuel : IMarket
    {
        internal double BasePrice;
        public string Name;
        public double CurrentPrice;
        public double PwDensity;
        public double CO2Density;
        private static readonly Random Random = new Random();

        internal double RandomValue { get; set; }
        public double PriceFactor { get; set; } = 1;

        /// <summary>
        /// The abstract Fuel Super-Class. Implement the IMarket Interface.
        /// See <see cref="Grid"/>, <see cref="FueledPlant"/>, <see cref="IMarket"/>.
        /// </summary>
        public Fuel(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Apply a small random variation on the price of the Fuel.
        /// </summary>
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

    /// <summary>
    /// Used to fuel CoalPlant, inherit from the Fuel class.
    /// Price varies randomly over time.
    /// Low price and power density per unit, but polute enormously.
    /// See <see cref="Grid"/>, <see cref="Fuel"/>, <see cref="CoalPlant"/>, <see cref="IMarket"/>.
    /// </summary>
    public class Coal : Fuel
    {
        /// <summary>
        /// Used to fuel CoalPlant, inherit from the Fuel class.
        /// Price varies randomly over time.
        /// Low price and power density per unit, but polute enormously.
        /// See <see cref="Grid"/>, <see cref="Fuel"/>, <see cref="CoalPlant"/>, <see cref="IMarket"/>.
        /// </summary>
        public Coal(string name) : base(name)
        {
            BasePrice = 1;
            CurrentPrice = 0;
            PwDensity = 2.5;
            CO2Density = 3;
        }
    }

    /// <summary>
    /// Used to fuel GasPlant, inherit from the Fuel class.
    /// Price varies randomly over time.
    /// moderate price and power density per unit, and moderate polution.
    /// See <see cref="Grid"/>, <see cref="Fuel"/>, <see cref="GasPlant"/>, <see cref="IMarket"/>.
    /// </summary>
    public class Gas : Fuel
    {
        /// <summary>
        /// Used to fuel GasPlant, inherit from the Fuel class.
        /// Price varies randomly over time.
        /// moderate price and power density per unit, and moderate polution.
        /// See <see cref="Grid"/>, <see cref="Fuel"/>, <see cref="GasPlant"/>, <see cref="IMarket"/>.
        /// </summary>
        public Gas(string name) : base(name)
        {
            BasePrice = 2.5;
            CurrentPrice = 0;
            PwDensity = 5;
            CO2Density = 1;
        }
    }

    /// <summary>
    /// Used to fuel UraniumPlant, inherit from the Fuel class.
    /// Price varies randomly over time.
    /// Very high price and power density per unit, doesn't polute.
    /// See <see cref="Grid"/>, <see cref="Fuel"/>, <see cref="UraniumPlant"/>, <see cref="IMarket"/>.
    /// </summary>
    public class Uranium : Fuel
    {
        /// <summary>
        /// Used to fuel UraniumPlant, inherit from the Fuel class.
        /// Price varies randomly over time.
        /// Very high price and power density per unit, doesn't polute.
        /// See <see cref="Grid"/>, <see cref="Fuel"/>, <see cref="UraniumPlant"/>, <see cref="IMarket"/>.
        /// </summary>
        public Uranium(string name) : base(name)
        {
            BasePrice = 150;
            CurrentPrice = 0;
            PwDensity = 1000;
            CO2Density = 0;
        }
    }
}
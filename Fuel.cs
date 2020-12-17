using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSimulationReseaux
{

    public abstract class Fuel : IMarket
    {

        public double BasePrice;    //$/uF
        public double CurrentPrice; //$/uF
        public double PwDensity;    //MW/uF
        public double CO2Density;   //CO2/uF

        public Random Random { get; set; }
        public double RandomValue { get; set; }
        public double PriceFactor { get; set; } = 1;
        public Fuel()
        {

        }

        public void RandomizePriceFactor()
        {
            RandomValue = Random.NextDouble();
            if(RandomValue >= 0.5)
            {
                PriceFactor += RandomValue-0.5 / 10;
            }
            else
            {
                PriceFactor -= RandomValue / 10;
            }
        }

        public void Update(int timePassed)
        {
            UpdatePrice();
        }

        public void UpdatePrice()
        {
            CurrentPrice = BasePrice * PriceFactor;
        }
    }
    public class Coal : Fuel
    {
        public Coal()
        {
            BasePrice = 1;      //$/uF
            CurrentPrice = 0;       //$/uF
            PwDensity = 2.5;    //MW/uF
            CO2Density = 3;   //CO2/uF
        }
    }
    public class Gas : Fuel
    {
        public Gas()
        {
            BasePrice = 2.5;      //$/uF
            CurrentPrice = 0;       //$/uF
            PwDensity = 5;    //MW/uF
            CO2Density = 1;   //CO2/uF
        }
    }
    public class Uranium : Fuel
    {
        public Uranium()
        {
            BasePrice = 150;      //$/uF
            CurrentPrice = 0;       //$/uF
            PwDensity = 1000;    //MW/uF
            CO2Density = 0;   //CO2/uF
        }
    }
}

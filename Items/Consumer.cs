using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProjetSimulationReseaux
{
    public abstract class Consumer : Node 
    {
        public int MaxPwRequest;     //MW/uT
        public int PwRequest;            //0-1 * maxPwConsumption;
        //public int PwRequest;
        public int PwCost;               //$/uT

        public Consumer(string name, int maxPwRequest, Point location)
        {
            Name = name;
            MaxPwRequest = maxPwRequest;
            Location = location;

            MaxInput = 1;
            MaxOutput = 0;          
        }    
        public override void Update(int timePassed)
        {
            UpdateWeather(timePassed);
            UpdatePwRequest();
        }     
        public abstract void UpdatePwRequest();
    }
    public class City : Consumer
    {
        public City(string name, int maxPwConsumption, Point location) : base(name, maxPwConsumption, location)
        {

        }
        public override void UpdatePwRequest()
        {
            PwRequest = (int)((0.4 * (1 - SunPercentage) + 0.6 * (1 - TemperaturePercentage)) * MaxPwRequest);
        }
    }
    public class Factory : Consumer
    {
        public Factory(string name, int maxPwConsumption, Point location) : base(name, maxPwConsumption, location)
        {
            
        }
        public override void UpdatePwRequest()
        {
            PwRequest = (int)(SunPercentage * MaxPwRequest);
        }
    }
}

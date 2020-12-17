using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProjetSimulationReseaux
{
    public abstract class Plant : Node 
    {  
        public int MaxPwProduction;     //MW/uT
        public int PwProduction;

        public double FlatOperatingCost;
        public double OperatingCost;      //$/uT       
        public bool IsOn;
        public bool UseFuel;

        public Plant(string name, int maxPwProduction, Point location)
        {
            Name = name;
            MaxPwProduction = maxPwProduction;
            Location = location;

            MaxInput = 0;
            MaxOutput = 1;
            
            IsOn = true;
        }
        public abstract void UpdatePwProduction();
        
    }

    public abstract class DirtyPlant : Plant
    {
        public int MinPwProduction;     //MW/uT
        public int TargetPwProduction;
        public Fuel Fuel;
        public double FuelConsumption;    //uF/uT
        public double CO2Emission;        //g/MW
        public double RampingCapabilityPercent; //%(MaxPwProduction - MinPwProduction)/uT OR MW/uT
        public int RampingCapabilityMW;
        public int ColdStartTime;         //uT
        public DirtyPlant(string name, int minPwProduction, int maxPwProduction, Fuel fuel, Point location) : base(name,  maxPwProduction, location)
        {
            MinPwProduction = minPwProduction;
            Fuel = fuel;
            TargetPwProduction = MinPwProduction;
            PwProduction = MinPwProduction;
            UseFuel = true;
        }
        
        public override void Update(int timePassed)
        {          
            UpdatePwProduction();
            UpdateFuelConsumption();
            UpdateOperatingCost();
            UpdateCO2Emission();         
        }
        public void Start()
        {
            IsOn = true;
            PwProduction = MinPwProduction;
        }
        public void Stop()
        {
            IsOn = false;
            PwProduction = 0;
        }
        public override void UpdatePwProduction()
        {
            if (IsOn)
            {
                if (TargetPwProduction > PwProduction)
                {
                    PwProduction += RampingCapabilityMW;
                }

                else if (TargetPwProduction < PwProduction)
                {
                    PwProduction -= RampingCapabilityMW;
                }
            }
        }
        public void SetTargetPwProduction(int targetPw)
        {
            if (targetPw > MaxPwProduction)
            {
                TargetPwProduction = MaxPwProduction;
            }
            else if (targetPw < MinPwProduction)
            {
                TargetPwProduction = MinPwProduction;
            }
            else
            {
                TargetPwProduction = targetPw;
            }           
        }
        private void UpdateFuelConsumption()
        {
            FuelConsumption = PwProduction / Fuel.PwDensity;
        }
        private void UpdateOperatingCost()
        {
            OperatingCost = FuelConsumption * Fuel.CurrentPrice;
        }
        private void UpdateCO2Emission()
        {
            CO2Emission = FuelConsumption * Fuel.CO2Density;
        }
    }
    public abstract class GreenPlant : Plant
    {
        public GreenPlant(string name, int maxPwProduction, Point location) : base(name, maxPwProduction, location)
        {
            UseFuel = false;
        }
        public override void Update(int timePassed)
        {
            UpdateWeather(timePassed);
            UpdatePwProduction();
        }
    }

    public class CoalPlant : DirtyPlant
    {
        public CoalPlant(string name, int minPwProduction, int maxPwProduction, Coal fuel, Point location) : base(name, minPwProduction, maxPwProduction, fuel, location)
        {
            MaxInput = 0;
            MaxOutput = 1;

            RampingCapabilityPercent = 0.1;
            RampingCapabilityMW = (int)(RampingCapabilityPercent * (MaxPwProduction - MinPwProduction));
            ColdStartTime = 2;
            FlatOperatingCost = 500;
        }

    }

    public class GasPlant : DirtyPlant
    {
        public GasPlant(string name, int minPwProduction, int maxPwProduction, Gas fuel, Point location) : base(name, minPwProduction, maxPwProduction, fuel, location)
        {
            RampingCapabilityPercent = 0.2;
            RampingCapabilityMW = (int)(RampingCapabilityPercent * (MaxPwProduction - MinPwProduction));
            ColdStartTime = 3;
            FlatOperatingCost = 700;
        }
    }

    public class UraniumPlant : DirtyPlant
    {
        public UraniumPlant(string name, int minPwProduction, int maxPwProduction, Uranium fuel, Point location) : base(name, minPwProduction, maxPwProduction, fuel, location)
        {
            RampingCapabilityPercent = 0.03;
            RampingCapabilityMW = (int)(RampingCapabilityPercent * (MaxPwProduction - MinPwProduction));
            ColdStartTime = 6;
            FlatOperatingCost = 1500;
        }
    }

    public class WindFarm : GreenPlant
    {
        public WindFarm(string name, int maxPwProduction,Point location) : base(name, maxPwProduction, location)
        {
            
        }
        public override void UpdatePwProduction()
        {
            PwProduction = (int)(WindPercentage * MaxPwProduction);
        }
    }
    public class SolarFarm : GreenPlant
    {
        public SolarFarm(string name, int maxPwProduction, Point location) : base(name, maxPwProduction, location)
        {

        }
        public override void UpdatePwProduction()
        {
            PwProduction = (int)(SunPercentage * MaxPwProduction);
        }
    }
}

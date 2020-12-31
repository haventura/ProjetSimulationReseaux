using System.Drawing;

namespace ProjetSimulationReseaux
{
    /// <summary>
    /// The abstract Plant Super-Class, inherit from Node.
    /// Caracterised by its maximum power production, its operating cost and wether or not it uses Fuel.
    /// </summary>
    public abstract class Plant : Node
    {
        public int MaxPwProduction;
        public int PwProduction;

        protected double FlatOperatingCost;
        public double OperatingCost;
        public bool IsOn;
        public bool UseFuel;

        /// <summary>
        /// The abstract Plant Super-Class, inherit from Node.
        /// Caracterised by its maximum power production, its operating cost and wether or not it uses Fuel.
        /// </summary>
        protected Plant(string name, int maxPwProduction, Point location)
        {
            Name = name;
            MaxPwProduction = maxPwProduction;
            Location = location;

            MaxInput = 0;
            MaxOutput = 1;

            IsOn = true;
        }

        /// <summary>
        /// Updates the amount of power produced by the Plant.
        /// </summary>
        protected abstract void UpdatePwProduction();
    }

    /// <summary>
    /// Inherit from the Plant class, a FueledPlant uses Fuel to produce Power.
    /// The amount of power produced is based on the power density of said fuel and the plant fuel consumption per unit of time.
    /// The targeted amount of power produced can be selected (between a min and max value) the plant will adjust its production at each cycle until it reaches the targeted power output.
    /// The rate at wich the power output change is dependant on a ramping capability factor.
    /// Also produce CO2 based on the CO2 density of the fuel used.
    /// See <see cref="Plant"/>.
    /// </summary>
    public abstract class FueledPlant : Plant
    {
        public int MinPwProduction;
        protected int TargetPwProduction;
        protected Fuel Fuel;
        protected double FuelConsumption;
        public double CO2Emission;
        protected double RampingCapabilityPercent;
        protected int RampingCapabilityMW;
        protected int ColdStartTime;

        /// <summary>
        /// Inherit from the Plant class, a FueledPlant uses Fuel to produce Power.
        /// The amount of power produced is based on the power density of said fuel and the plant fuel consumption per unit of time.
        /// The targeted amount of power produced can be selected (between a min and max value) the plant will adjust its production at each cycle until it reaches the targeted power output.
        /// The rate at wich the power output change is dependant on a ramping capability factor.
        /// Also produce CO2 based on the CO2 density of the fuel used.
        /// A FueledPlant can also be stopped/restarted.
        /// See <see cref="Plant"/>.
        /// </summary>
        protected FueledPlant(string name, int minPwProduction, int maxPwProduction, Fuel fuel, Point location) : base(name, maxPwProduction, location)
        {
            MinPwProduction = minPwProduction;
            Fuel = fuel;
            TargetPwProduction = MinPwProduction;
            PwProduction = MinPwProduction;
            UseFuel = true;
        }

        /// <summary>
        /// Updates each of the plant variable based on the time passed.
        /// </summary>
        public override void Update(int timePassed)
        {
            UpdatePwProduction();
            UpdateFuelConsumption();
            UpdateOperatingCost();
            UpdateCO2Emission();
        }

        /// <summary>
        /// Starts the Plant power production.
        /// </summary>
        public void Start()
        {
            IsOn = true;
            PwProduction = MinPwProduction;
        }

        /// <summary>
        /// Stops the Plant power production.
        /// </summary>
        public void Stop()
        {
            IsOn = false;
            PwProduction = 0;
        }

        /// <summary>
        /// Updates the Plant power production based on the targeted power production.
        /// </summary>
        protected override void UpdatePwProduction()
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

        /// <summary>
        /// Sets the targeted power production of the plant, the amount of times needed to reach that value depend on the ramping capability of the plant.
        /// </summary>
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

        /// <summary>
        /// Updates the Plant fuel consumption based on the current power production.
        /// </summary>
        private void UpdateFuelConsumption()
        {
            FuelConsumption = PwProduction / Fuel.PwDensity;
        }

        /// <summary>
        /// Updates the Plant operating cost based on a flat operating cost and the fuel consumption.
        /// </summary>
        private void UpdateOperatingCost()
        {
            if (IsOn)
            {
                OperatingCost = FlatOperatingCost + (FuelConsumption * Fuel.CurrentPrice);
            }
            else
            {
                OperatingCost = 0;
            }
        }

        /// <summary>
        /// Updates the Plant CO2 emission based the fuel consumption.
        /// </summary>
        private void UpdateCO2Emission()
        {
            CO2Emission = FuelConsumption * Fuel.CO2Density;
        }
    }

    /// <summary>
    /// Inherit from the Plant class, a GreenPlant power production is based solely on the weather at that Plant location.
    /// Cannot be stopped/restarted.
    /// See <see cref="Plant"/>.
    /// </summary>
    public abstract class GreenPlant : Plant
    {
        /// <summary>
        /// Inherit from the Plant class, a GreenPlant power production is based solely on the weather at that Plant location.
        /// Cannot be stopped/restarted.
        /// See <see cref="Plant"/>.
        /// </summary>
        protected GreenPlant(string name, int maxPwProduction, Point location) : base(name, maxPwProduction, location)
        {
            UseFuel = false;
        }

        /// <summary>
        /// Updates the plant variables based on the current weather at its location.
        /// </summary>
        public override void Update(int timePassed)
        {
            UpdateWeather(timePassed);
            UpdatePwProduction();
            UpdateOperatingCost();
        }

        /// <summary>
        /// Updates the plant operating cost based on a flat value.
        /// </summary>
        private void UpdateOperatingCost()
        {
            if (IsOn)
            {
                OperatingCost = FlatOperatingCost;
            }
            else
            {
                OperatingCost = 0;
            }
        }
    }

    /// <summary>
    /// Inherit from the FueledPlant class, a CoalPlant use Coal as Fuel.
    /// Cheap to operates but polutes a lot, and a relatively agile power production.
    /// See <see cref="Plant"/>, <see cref="FueledPlant"/>.
    /// </summary>
    public class CoalPlant : FueledPlant
    {
        /// <summary>
        /// Inherit from the FueledPlant class, a CoalPlant use Coal as Fuel.
        /// Cheap to operates but polutes a lot, and a relatively agile power production.
        /// See <see cref="Plant"/>, <see cref="FueledPlant"/>.
        /// </summary>
        public CoalPlant(string name, int minPwProduction, int maxPwProduction, Coal fuel, Point location) : base(name, minPwProduction, maxPwProduction, fuel, location)
        {
            RampingCapabilityPercent = 0.02;
            RampingCapabilityMW = (int)(RampingCapabilityPercent * (MaxPwProduction - MinPwProduction));
            ColdStartTime = 2;
            FlatOperatingCost = 500;
        }
    }

    /// <summary>
    /// Inherit from the FueledPlant class, a GasPlant use Gas as Fuel.
    /// Relatively cheap to operates with medium amount of polution, along with quite an agile power production.
    /// See <see cref="Plant"/>, <see cref="FueledPlant"/>.
    /// </summary>
    public class GasPlant : FueledPlant
    {
        /// <summary>
        /// Inherit from the FueledPlant class, a GasPlant use Gas as Fuel.
        /// Relatively cheap to operates with medium amount of polution, along with quite an agile power production.
        /// See <see cref="Plant"/>, <see cref="FueledPlant"/>.
        /// </summary>
        public GasPlant(string name, int minPwProduction, int maxPwProduction, Gas fuel, Point location) : base(name, minPwProduction, maxPwProduction, fuel, location)
        {
            RampingCapabilityPercent = 0.04;
            RampingCapabilityMW = (int)(RampingCapabilityPercent * (MaxPwProduction - MinPwProduction));
            ColdStartTime = 3;
            FlatOperatingCost = 700;
        }
    }

    /// <summary>
    /// Inherit from the FueledPlant class, a UraniumPlant use Uranium as Fuel.
    /// Very expensive to operates but doesn't polute, very low agility in terms of power production.
    /// See <see cref="Plant"/>, <see cref="FueledPlant"/>.
    /// </summary>
    public class UraniumPlant : FueledPlant
    {
        /// <summary>
        /// Inherit from the FueledPlant class, a UraniumPlant use Uranium as Fuel.
        /// Very expensive to operates but doesn't polute, very low agility in terms of power production.
        /// See <see cref="Plant"/>, <see cref="FueledPlant"/>.
        /// </summary>
        public UraniumPlant(string name, int minPwProduction, int maxPwProduction, Uranium fuel, Point location) : base(name, minPwProduction, maxPwProduction, fuel, location)
        {
            RampingCapabilityPercent = 0.005;
            RampingCapabilityMW = (int)(RampingCapabilityPercent * (MaxPwProduction - MinPwProduction));
            ColdStartTime = 6;
            FlatOperatingCost = 1500;
        }
    }

    /// <summary>
    /// Inherit from the GreenPlant class, a WindFarm produces power based on the amount of wind at its location.
    /// Cheap and clean, but cannot adjust its power output.
    /// See <see cref="Plant"/>, <see cref="GreenPlant"/>, <see cref="IWeather"/>.
    /// </summary>
    public class WindFarm : GreenPlant
    {
        /// <summary>
        /// Inherit from the GreenPlant class, a WindFarm produces power based on the amount of wind at its location.
        /// Cheap and clean, but cannot adjust its power output.
        /// See <see cref="Plant"/>, <see cref="GreenPlant"/>, <see cref="IWeather"/>.
        /// </summary>
        public WindFarm(string name, int maxPwProduction, Point location) : base(name, maxPwProduction, location)
        {
            FlatOperatingCost = 300;
        }

        protected override void UpdatePwProduction()
        {
            PwProduction = (int)(WindPercentage * MaxPwProduction);
        }
    }

    /// <summary>
    /// Inherit from the GreenPlant class, a SolarFarm produces power based on the amount of sunshine at its location.
    /// Cheap and clean, but cannot adjust its power output.
    /// See <see cref="Plant"/>, <see cref="GreenPlant"/>, <see cref="IWeather"/>.
    /// </summary>
    public class SolarFarm : GreenPlant
    {
        /// <summary>
        /// Inherit from the GreenPlant class, a SolarFarm produces power based on the amount of sunshine at its location.
        /// Cheap and clean, but cannot adjust its power output.
        /// See <see cref="Plant"/>, <see cref="GreenPlant"/>, <see cref="IWeather"/>.
        /// </summary>
        public SolarFarm(string name, int maxPwProduction, Point location) : base(name, maxPwProduction, location)
        {
            FlatOperatingCost = 400;
        }

        protected override void UpdatePwProduction()
        {
            PwProduction = (int)(SunPercentage * MaxPwProduction);
        }
    }
}
using System.Drawing;

namespace ProjetSimulationReseaux
{
    /// <summary>
    /// The abstract Consumer Super-Class, derived from Node.
    /// Caracterised by its power requirement.
    /// See <see cref="Node"/>.
    /// </summary>
    public abstract class Consumer : Node
    {
        protected int MaxPwRequest;
        public int PwRequest;
        protected int PwCost;

        /// <summary>
        /// The abstract Consumer Super-Class, inherit from Node.
        /// Caracterised by its power requirement.
        /// See <see cref="Node"/>.
        /// </summary>
        protected Consumer(string name, int maxPwRequest, Point location)
        {
            Name = name;
            MaxPwRequest = maxPwRequest;
            Location = location;

            MaxInput = 1;
            MaxOutput = 0;
        }

        /// <summary>
        /// Updates the variables of the consumer Node based on the weather at its location.
        /// </summary>
        public override void Update(int timePassed)
        {
            UpdateWeather(timePassed);
            UpdatePwRequest();
        }

        protected abstract void UpdatePwRequest();
    }

    /// <summary>
    /// A City, inherit from Consumer.
    /// Caracterised by its power requirement, wich varies based on the weather (both temperature and sunshine).
    /// See <see cref="Consumer"/>, <see cref="IWeather"/>.
    /// </summary>
    public class City : Consumer
    {
        /// <summary>
        /// A City, inherit from Consumer.
        /// Caracterised by its power requirement, wich varies based on the weather (both temperature and sunshine).
        /// See <see cref="Consumer"/>, <see cref="IWeather"/>.
        /// </summary>
        public City(string name, int maxPwConsumption, Point location) : base(name, maxPwConsumption, location)
        {
        }

        /// <summary>
        /// Update the amount of power requested by the consumer node, based on the weather.
        /// </summary>
        protected override void UpdatePwRequest()
        {
            PwRequest = (int)((0.4 * (1 - SunPercentage) + 0.6 * (1 - TemperaturePercentage)) * MaxPwRequest);
        }
    }

    /// <summary>
    /// A Factory, inherit from Consumer.
    /// Caracterised by its power requirement, wich varies based on the weather (sunshine).
    /// See <see cref="Consumer"/>, <see cref="IWeather"/>.
    /// </summary>
    public class Factory : Consumer
    {
        /// <summary>
        /// A Factory, inherit from Consumer.
        /// Caracterised by its power requirement, wich varies based on the weather (sunshine).
        /// See <see cref="Consumer"/>, <see cref="IWeather"/>.
        /// </summary>
        public Factory(string name, int maxPwConsumption, Point location) : base(name, maxPwConsumption, location)
        {
        }

        /// <summary>
        /// Update the amount of power requested by the consumer node, based on the weather.
        /// </summary>
        protected override void UpdatePwRequest()
        {
            PwRequest = (int)(SunPercentage * MaxPwRequest);
        }
    }
}

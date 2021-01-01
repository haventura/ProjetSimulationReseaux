using System;
using System.Collections.Generic;
using System.Drawing;

namespace ProjetSimulationReseaux
{
    /// <summary>
    /// The Node Super-Class. Every Node on the grid derive from this class.
    /// Implement the IWeather interface.
    /// Caracterised by a name, location, and the maximum number of inputs and outputs it can have.
    /// Each Node as a dictionnary containing its input with the power Line linking the two, and another dictionnary for the output, with the power line too.
    /// See <see cref="Plant"/>, <see cref="Consumer"/>, <see cref="IWeather"/>.
    /// </summary>
    ///

    public abstract class Node : IWeather
    {
        public double SunPercentage { get; set; }
        public double WindPercentage { get; set; }
        public double TemperaturePercentage { get; set; }
        public string Name;
        public Point Location;
        public Dictionary<Node, PowerLine> Dictionary_Input = new Dictionary<Node, PowerLine>();
        public Dictionary<Node, PowerLine> Dictionary_Output = new Dictionary<Node, PowerLine>();
        protected int MaxInput;
        protected int MaxOutput;

        /// <summary>
        /// Add an input Node to the selected Node and create a PowerLine with the stated max load linking the two.
        /// The input Node will also have the selected Node added as an output.
        /// Skipped if the two Nodes are already linked.
        /// See <see cref="Plant"/>, <see cref="Consumer"/>.
        /// </summary>
        public void AddInput(Node linkedNode, int powerLineMaxLoad)
        {
            if (Dictionary_Input.Count < MaxInput && !Dictionary_Input.ContainsKey(linkedNode))
            {
                PowerLine PowerLine = new PowerLine(powerLineMaxLoad, this.Location, linkedNode.Location);
                Dictionary_Input.Add(linkedNode, PowerLine);
                linkedNode.AddOutput(this, PowerLine);
            }
        }

        protected void AddInput(Node linkedNode, PowerLine powerLine)
        {
            if (Dictionary_Input.Count < MaxInput && !Dictionary_Input.ContainsKey(linkedNode))
            {
                Dictionary_Input.Add(linkedNode, powerLine);
                linkedNode.AddOutput(this, powerLine);
            }
        }

        /// <summary>
        /// Add an output Node to the selected Node and create a PowerLine with the stated max load linking the two.
        /// The output Node will also have the selected Node added as an input.
        /// Skipped if the two Nodes are already linked.
        /// </summary>
        public void AddOutput(Node linkedNode, int powerLineMaxLoad)
        {
            if (Dictionary_Output.Count < MaxOutput && !Dictionary_Output.ContainsKey(linkedNode))
            {
                PowerLine PowerLine = new PowerLine(powerLineMaxLoad, linkedNode.Location, this.Location);
                Dictionary_Output.Add(linkedNode, PowerLine);
                linkedNode.AddInput(this, PowerLine);
            }
        }

        protected void AddOutput(Node linkedNode, PowerLine powerLine)
        {
            if (Dictionary_Input.Count < MaxInput && !Dictionary_Input.ContainsKey(linkedNode))
            {
                Dictionary_Input.Add(linkedNode, powerLine);
                linkedNode.AddInput(this, powerLine);
            }
        }

        public void CalculateSunPercent(int timePassed)
        {
            SunPercentage = (Math.Sin(timePassed / 15.9f) + 1) / 2f;
        }

        public void CalculateWindPercent(int timePassed)
        {
            WindPercentage = (Math.Sin(timePassed / 5.3f) + 1) / 2f;
        }

        public void CalculateTemperaturePercent(int timePassed)
        {
            TemperaturePercentage = 0.2 * SunPercentage / 2f + ((0.8 * Math.Sin(timePassed / 79.6f) + 1) / 4f);
        }

        public void UpdateWeather(int timePassed)
        {
            CalculateSunPercent(timePassed);
            CalculateWindPercent(timePassed);
            CalculateTemperaturePercent(timePassed);
        }

        /// <summary>
        /// Update the concerned Node based on the time passed.
        /// </summary>
        public abstract void Update(int timePassed);

        /// <summary>
        /// Return the name of the Node.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}
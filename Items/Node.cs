using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProjetSimulationReseaux
{
    public abstract class Node : IWeather
    {
        public double SunPercentage { get; set; }
        public double WindPercentage { get; set; }
        public double TemperaturePercentage { get; set; }
        public string Name;
        public Point Location;
        public Dictionary<Node, PowerLine> Dictionary_Input = new Dictionary<Node, PowerLine>();
        public Dictionary<Node, PowerLine> Dictionary_Output = new Dictionary<Node, PowerLine>();
        public int MaxInput;
        public int MaxOutput;
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
        public abstract void Update(int timePassed);
        public override string ToString()
        {
            return Name;
        }
    }
}

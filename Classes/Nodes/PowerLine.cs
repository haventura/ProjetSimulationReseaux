using System.Drawing;

namespace ProjetSimulationReseaux
{
    /// <summary>
    /// A power line used to link two nodes together, caracterised by its max load, start position and end position.
    /// Not implemented.
    /// See <see cref="Grid"/>, <see cref="Node"/>.
    /// </summary>
    public class PowerLine
    {
        public int MaxPowerLoad;
        public int CurrentPowerLoad;
        public Point StartLocation;
        public Point EndLocation;

        public PowerLine(int maxPowerLoad, Point start, Point end)
        {
            MaxPowerLoad = maxPowerLoad;
            StartLocation = start;
            EndLocation = end;
        }

        public void Update(int timePassed)
        {
        }

        public override string ToString()
        {
            return "PowerLine " + StartLocation.ToString() + "-" + EndLocation.ToString();
        }
    }
}
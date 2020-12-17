using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProjetSimulationReseaux
{
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

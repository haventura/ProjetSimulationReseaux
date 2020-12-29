using System.Drawing;

namespace ProjetSimulationReseaux
{
    public abstract class Junction : Node
    {
        public int MaxPowerLoad;

        public Junction(string name, int maxPowerLoad, Point location)
        {
            Name = name;
            MaxPowerLoad = maxPowerLoad;
            Location = location;
        }

        public override void Update(int timePassed)
        {
        }
    }

    public class ConcentrationJunction : Junction
    {
        public ConcentrationJunction(string name, int maxPowerLoad, Point location) : base(name, maxPowerLoad, location)
        {
            MaxInput = 100;
            MaxOutput = 1;
        }
    }

    public class DistributionJunction : Junction
    {
        public DistributionJunction(string name, int maxPowerLoad, Point location) : base(name, maxPowerLoad, location)
        {
            MaxInput = 1;
            MaxOutput = 100;
        }
    }
}
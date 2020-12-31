using System.Drawing;

namespace ProjetSimulationReseaux
{
    /// <summary>
    /// The abstract Junction Super-Class, derived from Node.
    /// Caracterised by its max load, name and location.
    /// See <see cref="Grid"/>, <see cref="Node"/>.
    /// </summary>
    public abstract class Junction : Node
    {
        protected int MaxPowerLoad;

        /// <summary>
        /// The abstract Junction Super-Class, derived from Node.
        /// Caracterised by its max load, name and location.
        /// See <see cref="Grid"/>, <see cref="Node"/>.
        /// </summary>
        protected Junction(string name, int maxPowerLoad, Point location)
        {
            Name = name;
            MaxPowerLoad = maxPowerLoad;
            Location = location;
        }

        public override void Update(int timePassed)
        {
        }
    }

    /// <summary>
    /// A Concentration Node, derived from Junction.
    /// Takes several Nodes as input (Plants or other Junction) and concentrate that power on a single output.
    /// Not implemented.
    /// See <see cref="Junction"/>, <see cref="DistributionJunction"/>.
    /// </summary>
    public class ConcentrationJunction : Junction
    {
        /// <summary>
        /// A Concentration Node, derived from Junction.
        /// Takes several Nodes as input (Plants or other Junction) and concentrate that power on a single output.
        /// Not implemented.
        /// See <see cref="Junction"/>, <see cref="DistributionJunction"/>.
        /// </summary>
        public ConcentrationJunction(string name, int maxPowerLoad, Point location) : base(name, maxPowerLoad, location)
        {
            MaxInput = 100;
            MaxOutput = 1;
        }
    }

    /// <summary>
    /// A Distribution Node, derived from Junction.
    /// Takes one Node as input (Plant or another Junction) and distribute that power on several outputs.
    /// Not implemented.
    /// See <see cref="Junction"/>, <see cref="ConcentrationJunction"/>.
    /// </summary>
    public class DistributionJunction : Junction
    {
        /// <summary>
        /// A Distribution Node, derived from Junction.
        /// Takes one Node as input (Plant or another Junction) and distribute that power on several outputs.
        /// Not implemented.
        /// See <see cref="Junction"/>, <see cref="ConcentrationJunction"/>.
        /// </summary>
        public DistributionJunction(string name, int maxPowerLoad, Point location) : base(name, maxPowerLoad, location)
        {
            MaxInput = 1;
            MaxOutput = 100;
        }
    }
}
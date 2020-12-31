using System.Collections.Generic;
using System.Linq;

namespace ProjetSimulationReseaux
{
    /// <summary>
    /// Simulate the behaviour of an electrical grid.
    /// Contain Lists of Nodes (Plants,Consumers,...) and PowerLines with their positions, also Fuels.
    /// See <see cref="Node"/>, <see cref="Plant"/>, <see cref="Consumer"/>, <see cref="Fuel"/>, <see cref="PowerLine"/>.
    /// </summary>
    public class Grid
    {
        public int SizeX;
        public int SizeY;

        public List<Node> List_Node;
        public List<Fuel> List_Fuel;
        public List<PowerLine> List_PowerLine;

        public int TotalPwProduction;
        public int TotalPwRequest;
        public int PwDeficit;
        public double TotalOperatingCost;
        public double TotalCO2Emission;

        public int TimePassed = 0;

        /// <summary>
        /// Simulate the behaviour of an electrical grid.
        /// Contain Lists of Nodes (Plants,Consumers,...) and PowerLines with their positions, also Fuels.
        /// See <see cref="Node"/>, <see cref="Plant"/>, <see cref="Consumer"/>, <see cref="Fuel"/>, <see cref="PowerLine"/>.
        /// </summary>
        public Grid(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            List_Fuel = new List<Fuel>();
            List_Node = new List<Node>();
            List_PowerLine = new List<PowerLine>();
        }

        /// <summary>
        /// Add all the power lines with corresponding max load to the grid based on the connected nodes already in the grid.
        /// Called only once after all the nodes have been added and linked.
        /// </summary>
        public void AddAllPowerLine()
        {
            foreach (Node node in List_Node)
            {
                foreach (KeyValuePair<Node, PowerLine> inputNode in node.Dictionary_Input)
                {
                    if (!List_PowerLine.Contains(inputNode.Value))
                    {
                        List_PowerLine.Add(inputNode.Value);
                    }
                }
                //normally useless to go through output (every node output correspond to another node input which have amready been linked) but you never know...
                foreach (KeyValuePair<Node, PowerLine> outputNode in node.Dictionary_Output)
                {
                    if (!List_PowerLine.Contains(outputNode.Value))
                    {
                        List_PowerLine.Add(outputNode.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Updates all the element in the grid, Fuels, Nodes and powerLine and increment time passed by 1.
        /// </summary>
        public void Update()
        {
            UpdateAllFuel();
            UpdateAllNode();
            UpdateAllPowerLine();
            TimePassed += 1;
        }

        /// <summary>
        /// Updates, in order, every Consumer and adds up each of their power request, then updates the plant and substract their power production from the total request.
        /// If the power production is too low/high, adjust the targeted power of the Plants.
        /// Also gather CO2 emission and operating cost.
        /// Junction and PowerLine are not implemented (yet...).
        /// </summary>
        private void UpdateAllNode()
        {
            TotalPwProduction = 0;
            TotalPwRequest = 0;
            TotalCO2Emission = 0;
            TotalOperatingCost = 0;
            foreach (Consumer consumer in List_Node.OfType<Consumer>())
            {
                consumer.Update(TimePassed);
                TotalPwRequest += consumer.PwRequest;
                PwDeficit = TotalPwRequest;
            }
            foreach (GreenPlant greenPlant in List_Node.OfType<GreenPlant>())
            {
                greenPlant.Update(TimePassed);
                TotalPwProduction += greenPlant.PwProduction;
                PwDeficit -= greenPlant.PwProduction;
                TotalOperatingCost += greenPlant.OperatingCost;
            }
            foreach (FueledPlant dirtyPlant in List_Node.OfType<FueledPlant>())
            {
                if (dirtyPlant.IsOn)
                {
                    if (PwDeficit >= dirtyPlant.MinPwProduction)
                    {
                        dirtyPlant.SetTargetPwProduction(PwDeficit);
                    }
                    else
                    {
                        dirtyPlant.Stop();
                    }
                }
                else
                {
                    if (PwDeficit >= dirtyPlant.MinPwProduction)
                    {
                        dirtyPlant.Start();
                    }
                }
                dirtyPlant.Update(TimePassed);
                TotalPwProduction += dirtyPlant.PwProduction;
                PwDeficit -= dirtyPlant.PwProduction;
                TotalCO2Emission += dirtyPlant.CO2Emission;
                TotalOperatingCost += dirtyPlant.OperatingCost;
            }
        }

        /// <summary>
        /// Updates each Fuel of the grid .
        /// </summary>
        private void UpdateAllFuel()
        {
            foreach (Fuel Fuel in List_Fuel)
            {
                Fuel.Update(TimePassed);
            }
        }

        /// <summary>
        /// Updates each PowerLines of the grid (not implemented).
        /// </summary>
        private void UpdateAllPowerLine()
        {
            foreach (PowerLine PowerLine in List_PowerLine)
            {
                PowerLine.Update(TimePassed);
            }
        }

        /// <summary>
        /// Add a Fuel object (Coal, Gas, Uranium, ...) to the grid.
        /// </summary>
        public void AddFuel(Fuel fuel)
        {
            List_Fuel.Add(fuel);
        }

        /// <summary>
        /// Add a Node object (Plant, Consumer, Junction, ...) to the grid.
        /// </summary>
        public void AddNode(Node node)
        {
            List_Node.Add(node);
        }
    }
}
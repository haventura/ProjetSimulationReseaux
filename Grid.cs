using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSimulationReseaux
{
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


        public Grid(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            List_Fuel = new List<Fuel>();
            List_Node = new List<Node>();
            List_PowerLine = new List<PowerLine>();
        }

        public void AddAllPowerLine()
        {
            foreach (Node node in List_Node)
            {
                foreach(KeyValuePair<Node, PowerLine> inputNode in node.Dictionary_Input)
                {
                    if (!List_PowerLine.Contains(inputNode.Value))
                    {                      
                        List_PowerLine.Add(inputNode.Value);                    
                    }                     
                }
                //normally useless to go through output (every node output correspond to another node input)
                foreach (KeyValuePair<Node, PowerLine> outputNode in node.Dictionary_Output)
                {
                    if (!List_PowerLine.Contains(outputNode.Value))
                    {
                        List_PowerLine.Add(outputNode.Value);
                    }
                }
            }
        }

        public void Update()
        {
            UpdateAllFuel();
            UpdateAllNode();
            UpdateAllPowerLine();
            TimePassed += 1;
        }

        public void UpdateAllNode()
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
            foreach (DirtyPlant dirtyPlant in List_Node.OfType<DirtyPlant>())
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
            /*
            foreach (Node node in List_Node)
            {
                node.Update(TimePassed);
                if (node is Plant)
                {
                    Plant Plant = node as Plant;
                    TotalPwProduction += Plant.PwProduction;
                }
                if (node is Consumer)
                {
                    Consumer Consumer = node as Consumer;
                    TotalPwRequest += Consumer.PwRequest;
                }
            }
            */
        }

        public void UpdateAllFuel()
        {
            foreach (Fuel Fuel in List_Fuel)
            {
                Fuel.Update(TimePassed);
            }
        }
        
        public void UpdateAllPowerLine()
        {
            foreach (PowerLine PowerLine in List_PowerLine)
            {
                PowerLine.Update(TimePassed);
            }
        }

        public void AddPlant(Plant plant)
        {
            List_Node.Add(plant);
        }

        public void AddFuel(Fuel fuel)
        {
            List_Fuel.Add(fuel);
        }

        public void AddConsumer(Consumer consumer)
        {
            List_Node.Add(consumer);
        }

        public void AddNode(Node node)
        {
            List_Node.Add(node);
        }

        public void AddPowerLine(PowerLine powerLine)
        {
            List_PowerLine.Add(powerLine);
        }
    }
}

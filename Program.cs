using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ProjetSimulationReseaux
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Grid Grid = new Grid(10, 10);

            Coal Coal = new Coal();
            Gas Gas = new Gas();
            Uranium Uranium = new Uranium();

            Grid.AddFuel(Coal);
            Grid.AddFuel(Gas);
            Grid.AddFuel(Uranium);

            ConcentrationJunction ConcentrationNode1 = new ConcentrationJunction("CNode1", 2000, new Point(2, 3));
            ConcentrationJunction ConcentrationNode2 = new ConcentrationJunction("CNode2", 4000, new Point(4, 7));
            DistributionJunction DistributionNode1 = new DistributionJunction("DNode1", 2000, new Point(5, 3));
            DistributionJunction DistributionNode2 = new DistributionJunction("DNode2", 4000, new Point(8, 8));

            CoalPlant CoalPlant1 = new CoalPlant("CoalPlant1", 200, 600, Coal, new Point(1, 3));
            CoalPlant CoalPlant2 = new CoalPlant("CoalPlant2", 600, 1250, Coal, new Point(1, 1));
            GasPlant GasPlant1 = new GasPlant("GasPlant1", 200, 1000, Gas, new Point(1, 4));
            UraniumPlant UraniumPlant1 = new UraniumPlant("NukePlant1", 1200, 1600, Uranium, new Point(3, 8));
            WindFarm WindFarm1 = new WindFarm("WindFarm1", 300, new Point(2, 5));           
            SolarFarm SolarFarm1 = new SolarFarm("SolarFarm1", 500, new Point(4, 8));
                       
            City City1 = new City("City1", 2200, new Point(6, 3));
            City City2 = new City("City2", 1500, new Point(8, 7));
            City City3 = new City("City3", 900, new Point(6, 2));
            Factory Factory1 = new Factory("Factory1", 1000, new Point(6, 7));

            CoalPlant1.AddOutput(ConcentrationNode1, 2000);
            CoalPlant2.AddOutput(ConcentrationNode1, 1500);
            GasPlant1.AddOutput(ConcentrationNode1, 2000);
            UraniumPlant1.AddOutput(ConcentrationNode2, 4000);                               
            WindFarm1.AddOutput(ConcentrationNode1, 2000);
            SolarFarm1.AddOutput(ConcentrationNode2, 1000);
            ConcentrationNode1.AddOutput(DistributionNode1, 6000);
            ConcentrationNode2.AddOutput(ConcentrationNode1, 4000);
            DistributionNode1.AddOutput(DistributionNode2, 3000);
            DistributionNode1.AddOutput(City1, 2000);
            DistributionNode2.AddOutput(City2, 3000);
            DistributionNode2.AddOutput(Factory1, 3000);
            DistributionNode1.AddOutput(City3, 2000);
                        
            Grid.AddNode(ConcentrationNode1);
            Grid.AddNode(ConcentrationNode2);
            Grid.AddNode(DistributionNode1);
            Grid.AddNode(DistributionNode2);

            Grid.AddNode(UraniumPlant1);
            Grid.AddNode(CoalPlant1);
            Grid.AddNode(CoalPlant2);
            Grid.AddNode(GasPlant1);                       
            Grid.AddNode(WindFarm1);                   
            Grid.AddNode(SolarFarm1);

            Grid.AddNode(City1);
            Grid.AddNode(City2);
            Grid.AddNode(City3);
            Grid.AddNode(Factory1);

            Grid.AddAllPowerLine();

            ControlCenter ControlCenter = new ControlCenter(Grid);
            Application.Run(ControlCenter);
          
        }
    }
}

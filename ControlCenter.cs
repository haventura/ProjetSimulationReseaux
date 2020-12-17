using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProjetSimulationReseaux
{
    public partial class ControlCenter : Form
    {
        Grid Grid;
        int PBXFactor;
        int PBYFactor;

        int PlantSize;
        int ConsumerSize;
        int NodeSize;
        int PowerLineWidth;


        public ControlCenter(Grid grid)
        {
            Grid = grid;
            
            InitializeComponent();
            PBXFactor = pictureBox1.Width / Grid.SizeX;
            PBYFactor = pictureBox1.Height / Grid.SizeY;
            PlantSize = 14;
            ConsumerSize = 12;
            NodeSize = 8;
            PowerLineWidth = 3;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {   
            Grid.Update();
            if(Grid.TimePassed > 200)
            {
                ChartTotal.Series["TotalPwProduction"].Points.RemoveAt(0);
                ChartTotal.Series["TotalPwRequest"].Points.RemoveAt(0);
                ChartTotal.Series["PwDeficit"].Points.RemoveAt(0);
                ChartTotal.Series["TotalCO2Emission"].Points.RemoveAt(0);
                foreach (Series series in ChartNode.Series)
                { 
                    series.Points.RemoveAt(0);                
                }
            }
            ChartTotal.Series["TotalPwProduction"].Points.AddY(Grid.TotalPwProduction);
            ChartTotal.Series["TotalPwRequest"].Points.AddY(Grid.TotalPwRequest);
            ChartTotal.Series["PwDeficit"].Points.AddY(Grid.PwDeficit);
            ChartTotal.Series["TotalCO2Emission"].Points.AddY(Grid.TotalCO2Emission);
            for (int i = 0; i < Grid.List_Node.Count; i++)
            {
                if (Grid.List_Node[i] is Plant)
                {
                    Plant Plant = Grid.List_Node[i] as Plant;
                    ChartNode.Series[Plant.Name].Points.AddY(Plant.PwProduction);
                }
                else if (Grid.List_Node[i] is Consumer)
                {
                    Consumer Consumer = Grid.List_Node[i] as Consumer;
                    ChartNode.Series[Consumer.Name].Points.AddY(Consumer.PwRequest);
                }
            }
            ChartTotal.ResetAutoValues();
            ChartNode.ResetAutoValues();
            Update();
        }

        private void ControlCenter_Load(object sender, EventArgs e)
        {
            DrawAll();
            //AddLabelInfoTable();
            AddLabelInfoMap();
            FillChartNode();
            foreach(ChartArea chartArea in ChartNode.ChartAreas)
            {
                chartArea.AxisX.Maximum = 200;
            }
            foreach (ChartArea chartArea in ChartTotal.ChartAreas)
            {
                chartArea.AxisX.Maximum = 200;
            }
        }

        private void DrawAll()
        {
            pictureBox1.Paint += new PaintEventHandler(this.pictureBox1_PaintBaseGrid);
            pictureBox1.Paint += new PaintEventHandler(this.pictureBox1_PaintPowerLine);
            pictureBox1.Paint += new PaintEventHandler(this.pictureBox1_PaintNode);          
        }

        private void AddLabelInfoTable()
        {
            int m = Grid.List_Node.Count;
            int n = Grid.List_PowerLine.Count;
            Label[] LabelsInfoTable = new Label[m+n];
            
            for (int i = 0; i < m; i++)
            {
                LabelsInfoTable[i] = new Label();
                LabelsInfoTable[i].AutoSize = true;
                LabelsInfoTable[i].Text = Grid.List_Node[i].ToString();
                LabelsInfoTable[i].Location = new Point(10, 440 + 18 * i);
                Controls.Add(LabelsInfoTable[i]);
            }
            for (int j = 0; j < n; j++)
            {
                LabelsInfoTable[m + j] = new Label();
                LabelsInfoTable[m + j].Size = new Size(200, 18);
                LabelsInfoTable[m + j].Text = Grid.List_PowerLine[j].ToString();
                LabelsInfoTable[m + j].Location = new Point(10, 440 + 18 * (m + j));
                Controls.Add(LabelsInfoTable[m + j]);
            }
        }
        private void AddLabelInfoMap()
        {
            int m = Grid.List_Node.Count;
            Label[] LabelsInfoMap = new Label[m];

            for (int i = 0; i < m; i++)
            {
                LabelsInfoMap[i] = new Label();
                LabelsInfoMap[i].AutoSize = true;
                LabelsInfoMap[i].BackColor = Color.Transparent;
                LabelsInfoMap[i].Text = Grid.List_Node[i].ToString();
                LabelsInfoMap[i].Location = new Point(PBXFactor * Grid.List_Node[i].Location.X - (NodeSize / 2) + 10, PBYFactor * Grid.List_Node[i].Location.Y - (NodeSize / 2) + 10);
                LabelsInfoMap[i].Hide();
                pictureBox1.Controls.Add(LabelsInfoMap[i]);
            }
        }

        private void FillChartNode()
        {
            byte Red = 254;
            byte Green = 0;
            byte Blue = 0;
            for (int i = 0; i < Grid.List_Node.Count; i++)
            {              
                if (Grid.List_Node[i] is Plant)
                {
                    ChartNode.Series.Add(new Series(Grid.List_Node[i].Name));
                    ChartNode.Series[Grid.List_Node[i].Name].ChartType = SeriesChartType.Line;
                    ChartNode.Series[Grid.List_Node[i].Name].Color = Color.FromArgb(Red,Green,Blue);
                }
                else if (Grid.List_Node[i] is Consumer)
                {
                    ChartNode.Series.Add(new Series(Grid.List_Node[i].Name));
                    ChartNode.Series[Grid.List_Node[i].Name].ChartType = SeriesChartType.Line;
                    ChartNode.Series[Grid.List_Node[i].Name].Color = Color.FromArgb(Red, Green, Blue);
                }

                if (Red == 254 && Green != 254 && Blue == 0)
                {
                    Green += 127;
                }
                else if (Red != 0 && Green == 254 && Blue == 0)
                {
                    Red -= 127;
                }
                else if (Red == 0 && Green == 254 && Blue != 254)
                {
                    Blue += 127;
                }
                else if (Red == 0 && Green != 0 && Blue == 254)
                {
                    Green -= 127;
                }
                else if (Red != 254 && Green == 0 && Blue == 254)
                {
                    Red += 127;
                }
                else if (Red == 254 && Green == 0 && Blue != 0)
                {
                    Blue -= 127;
                }
            }
        }

        private void pictureBox1_PaintBaseGrid(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            for (int i = 0; i < Grid.SizeX; i++)
            {
                for (int j = 0; j < Grid.SizeX; j++)
                {
                    Canvas.DrawEllipse(new Pen(Color.Gray, 2), PBXFactor * i, PBYFactor * j, 2, 2);
                    
                }
            }
        }
        private void pictureBox1_PaintNode(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            Pen Pen = new Pen(Color.Black);
            foreach (Node node in Grid.List_Node)
            {
                if(node is Junction)
                {
                    Pen.Color = Color.Black;
                    Pen.Width = NodeSize;
                    Canvas.DrawEllipse(Pen, PBXFactor * node.Location.X - (NodeSize / 2), PBYFactor * node.Location.Y - (NodeSize / 2), NodeSize, NodeSize);

                }
                else if (node is Plant)
                {
                    Pen.Color = Color.Red;
                    Pen.Width = PlantSize;
                    Canvas.DrawEllipse(Pen, PBXFactor * node.Location.X - (PlantSize / 2), PBYFactor * node.Location.Y - (PlantSize / 2), PlantSize, PlantSize);

                }
                else if (node is Consumer)
                {
                    Pen.Color = Color.Green;
                    Pen.Width = ConsumerSize;
                    Canvas.DrawEllipse(Pen, PBXFactor * node.Location.X - (ConsumerSize / 2), PBYFactor * node.Location.Y - (ConsumerSize / 2), ConsumerSize, ConsumerSize);

                }
                else
                {
                    Pen.Width = 10;
                }
            }
        }
        private void pictureBox1_PaintPowerLine(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            foreach (PowerLine PowerLine in Grid.List_PowerLine)
            {
                Canvas.DrawLine(new Pen(Color.Blue, PowerLineWidth), PBXFactor * PowerLine.StartLocation.X, PBYFactor * PowerLine.StartLocation.Y, PBXFactor * PowerLine.EndLocation.X, PBYFactor * PowerLine.EndLocation.Y);
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            foreach(Label label in pictureBox1.Controls.OfType<Label>())
            {
                label.Show();
            }           
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            foreach (Label label in pictureBox1.Controls.OfType<Label>())
            {
                label.Hide();
            }            
        }
    }
}

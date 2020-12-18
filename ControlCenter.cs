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

        int PlantSizeX;
        int PlantSizeY;
        int ConsumerSizeX;
        int ConsumerSizeY;
        int NodeSizeX;
        int NodeSizeY;
        int PowerLineWidth;     
        int GridDotSizeX;
        int GridDotSizeY;


        public ControlCenter(Grid grid)
        {
            Grid = grid;
            
            InitializeComponent();
            PBXFactor = PictureBoxGrid.Width / Grid.SizeX;
            PBYFactor = PictureBoxGrid.Height / Grid.SizeY;
            PlantSizeX = PBXFactor / 3;
            PlantSizeY = PBYFactor / 3;
            ConsumerSizeX = PBXFactor / 3;
            ConsumerSizeY = PBYFactor / 3;
            NodeSizeX = PBXFactor / 5;
            NodeSizeY = PBYFactor / 5;
            PowerLineWidth = PBXFactor / 10;
            GridDotSizeX = PBXFactor / 15;
            GridDotSizeY = PBYFactor / 15;          
        }
        private void timer1_Tick(object sender, EventArgs e)
        {   
            Grid.Update();
            UpdateAllChart();
            UpdateAllLabel();
            
            Update();
        }
        void UpdateAllChart()
        {
            if (Grid.TimePassed > 200)
            {
                ChartTotal.Series["TotalPwProduction"].Points.RemoveAt(0);
                ChartTotal.Series["TotalPwRequest"].Points.RemoveAt(0);
                ChartTotal.Series["PwDeficit"].Points.RemoveAt(0);
                ChartTotal.Series["TotalCO2Emission"].Points.RemoveAt(0);
                ChartTotal.Series["TotalOperatingCost"].Points.RemoveAt(0);
                foreach (Series series in ChartNode.Series)
                {
                    series.Points.RemoveAt(0);
                }
            }
            ChartTotal.Series["TotalPwProduction"].Points.AddY(Grid.TotalPwProduction);
            ChartTotal.Series["TotalPwRequest"].Points.AddY(Grid.TotalPwRequest);
            ChartTotal.Series["PwDeficit"].Points.AddY(Grid.PwDeficit);
            ChartTotal.Series["TotalCO2Emission"].Points.AddY(Grid.TotalCO2Emission);
            ChartTotal.Series["TotalOperatingCost"].Points.AddY(Grid.TotalOperatingCost);
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
        }
        void UpdateAllLabel()
        {
            int i = 0;
            
            foreach (Plant plant in Grid.List_Node.OfType<Plant>())
            {
                groupBoxPlantProduction.Controls[i].Text = plant.PwProduction.ToString() + " / " + plant.MaxPwProduction.ToString() + " MW";
                if(plant.IsOn)
                {
                    groupBoxPlantState.Controls[i].Text = "On";
                    groupBoxPlantState.Controls[i].BackColor = Color.Green;
                }
                else
                {
                    groupBoxPlantState.Controls[i].Text = "Off";
                    groupBoxPlantState.Controls[i].BackColor = Color.Red;
                }
                i += 1;
            }
            i = 0;
            foreach (Consumer consumer in Grid.List_Node.OfType<Consumer>())
            {
                groupBoxConsumerRequest.Controls[i].Text = consumer.PwRequest.ToString() + " MW";
                i += 1;
            }
        }

        private void ControlCenter_Load(object sender, EventArgs e)
        {
            DrawAll();
            AddLabelInfoTable();
            AddCheckBoxInfoTable();
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
            PictureBoxGrid.Paint += new PaintEventHandler(pictureBox1_PaintEdge);
            PictureBoxGrid.Paint += new PaintEventHandler(pictureBox1_PaintBaseGrid);
            PictureBoxGrid.Paint += new PaintEventHandler(pictureBox1_PaintPowerLine);
            PictureBoxGrid.Paint += new PaintEventHandler(pictureBox1_PaintNode);          
        }

        private void AddLabelInfoTable()
        {
            int PlantTotalCount = Grid.List_Node.OfType<Plant>().Count();
            int ConsumerTotalCount = Grid.List_Node.OfType<Consumer>().Count();
            Label[] LabelsInfoTablePlantsName = new Label[PlantTotalCount];
            Label[] LabelsInfoTablePlantsProduction = new Label[PlantTotalCount];
            Label[] LabelsInfoTablePlantsState = new Label[PlantTotalCount];
            Label[] LabelsInfoTableConsumerName = new Label[ConsumerTotalCount];
            Label[] LabelsInfoTableConsumerRequest = new Label[ConsumerTotalCount];
            int PlantCurrentCount = 0;
            int ConsumerCurrentCount = 0;
            
            foreach(Plant plant in Grid.List_Node.OfType<Plant>())
            {
                LabelsInfoTablePlantsName[PlantCurrentCount] = new Label();
                LabelsInfoTablePlantsName[PlantCurrentCount].AutoSize = true;
                LabelsInfoTablePlantsName[PlantCurrentCount].Text = plant.ToString();
                LabelsInfoTablePlantsName[PlantCurrentCount].Location = new Point(5, 10 + 16 * PlantCurrentCount);
                groupBoxPlantName.Controls.Add(LabelsInfoTablePlantsName[PlantCurrentCount]);
                LabelsInfoTablePlantsProduction[PlantCurrentCount] = new Label();
                LabelsInfoTablePlantsProduction[PlantCurrentCount].AutoSize = true;
                LabelsInfoTablePlantsProduction[PlantCurrentCount].Text = plant.PwProduction.ToString() + " / " + plant.MaxPwProduction.ToString() + " MW";
                LabelsInfoTablePlantsProduction[PlantCurrentCount].Location = new Point(5, 10 + 16 * PlantCurrentCount);
                groupBoxPlantProduction.Controls.Add(LabelsInfoTablePlantsProduction[PlantCurrentCount]);
                LabelsInfoTablePlantsState[PlantCurrentCount] = new Label();
                LabelsInfoTablePlantsState[PlantCurrentCount].AutoSize = true;
                if (plant.IsOn)
                {
                    LabelsInfoTablePlantsState[PlantCurrentCount].Text = "On";
                    LabelsInfoTablePlantsState[PlantCurrentCount].BackColor = Color.Green;
                }
                else
                {
                    LabelsInfoTablePlantsState[PlantCurrentCount].Text = "Off";
                    LabelsInfoTablePlantsState[PlantCurrentCount].BackColor = Color.Red;
                }
                LabelsInfoTablePlantsState[PlantCurrentCount].Location = new Point(5, 10 + 16 * PlantCurrentCount);
                groupBoxPlantState.Controls.Add(LabelsInfoTablePlantsState[PlantCurrentCount]);
                PlantCurrentCount += 1;
            }
            foreach (Consumer consumer in Grid.List_Node.OfType<Consumer>())
            {
                LabelsInfoTableConsumerName[ConsumerCurrentCount] = new Label();
                LabelsInfoTableConsumerName[ConsumerCurrentCount].AutoSize = true;
                LabelsInfoTableConsumerName[ConsumerCurrentCount].Text = consumer.ToString();
                LabelsInfoTableConsumerName[ConsumerCurrentCount].Location = new Point(5, 10 + 16 * ConsumerCurrentCount);
                groupBoxConsumerName.Controls.Add(LabelsInfoTableConsumerName[ConsumerCurrentCount]);
                LabelsInfoTableConsumerRequest[ConsumerCurrentCount] = new Label();
                LabelsInfoTableConsumerRequest[ConsumerCurrentCount].AutoSize = true;
                LabelsInfoTableConsumerRequest[ConsumerCurrentCount].Text = consumer.PwRequest.ToString() + " MW";
                LabelsInfoTableConsumerRequest[ConsumerCurrentCount].Location = new Point(5, 10 + 16 * ConsumerCurrentCount);
                groupBoxConsumerRequest.Controls.Add(LabelsInfoTableConsumerRequest[ConsumerCurrentCount]);
                ConsumerCurrentCount += 1;
            }
        }
        private void AddCheckBoxInfoTable()
        {
            int PlantTotalCount = Grid.List_Node.OfType<Plant>().Count();
            int ConsumerTotalCount = Grid.List_Node.OfType<Consumer>().Count();
            CheckBox[] CheckBoxPlant = new CheckBox[PlantTotalCount];
            CheckBox[] CheckBoxConsumer = new CheckBox[PlantTotalCount];
            int PlantCurrentCount = 0;
            int ConsumerCurrentCount = 0;

            foreach (Plant plant in Grid.List_Node.OfType<Plant>())
            {
                CheckBoxPlant[PlantCurrentCount] = new CheckBox();
                CheckBoxPlant[PlantCurrentCount].Size = new Size(15, 15);
                CheckBoxPlant[PlantCurrentCount].Checked = true;
                CheckBoxPlant[PlantCurrentCount].AccessibleName = plant.ToString();
                CheckBoxPlant[PlantCurrentCount].CheckStateChanged += new EventHandler(CheckBoxChart);
                //CheckBoxPlant[PlantCurrentCount].CheckedChanged = true;
                CheckBoxPlant[PlantCurrentCount].Location = new Point(1, 10 + 16 * PlantCurrentCount);
                groupBoxCheckBoxPlant.Controls.Add(CheckBoxPlant[PlantCurrentCount]);
                PlantCurrentCount += 1;
            }
            foreach (Consumer consumer in Grid.List_Node.OfType<Consumer>())
            {
                CheckBoxConsumer[ConsumerCurrentCount] = new CheckBox();
                CheckBoxConsumer[ConsumerCurrentCount].Size = new Size(15, 15);
                CheckBoxConsumer[ConsumerCurrentCount].Checked = true;
                CheckBoxConsumer[ConsumerCurrentCount].AccessibleName = consumer.ToString();
                CheckBoxConsumer[ConsumerCurrentCount].CheckStateChanged += new EventHandler(CheckBoxChart);
                CheckBoxConsumer[ConsumerCurrentCount].Location = new Point(1, 10 + 16 * ConsumerCurrentCount);
                groupBoxCheckBoxConsumer.Controls.Add(CheckBoxConsumer[ConsumerCurrentCount]);
                ConsumerCurrentCount += 1;
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
                LabelsInfoMap[i].Location = new Point(PBXFactor * Grid.List_Node[i].Location.X - (NodeSizeX / 2) +(PBXFactor / 2) + 10, PBYFactor * Grid.List_Node[i].Location.Y - (NodeSizeY / 2) + (PBYFactor / 2) + 10);
                LabelsInfoMap[i].Hide();
                PictureBoxGrid.Controls.Add(LabelsInfoMap[i]);
            }
        }

        private void FillChartNode()
        {
            int n = Grid.List_Node.OfType<Plant>().Count();
            n += Grid.List_Node.OfType<Consumer>().Count();
            int r, g, b;
            double h = 0;
            double DeltaHue = (double)360 / n;
            
            for (int i = 0; i < Grid.List_Node.Count; i++)
            {
                if (Grid.List_Node[i] is Junction)
                {
                    continue;
                }
                Hsv.HsvToRgb(h, 1, 0.7, out r, out g, out b);
                if (Grid.List_Node[i] is Plant)
                {
                    ChartNode.Series.Add(new Series(Grid.List_Node[i].Name));
                    ChartNode.Series[Grid.List_Node[i].Name].ChartType = SeriesChartType.Line;
                    ChartNode.Series[Grid.List_Node[i].Name].BorderWidth = 2;
                    ChartNode.Series[Grid.List_Node[i].Name].Color = Color.FromArgb(r , g, b);                  
                }
                else if (Grid.List_Node[i] is Consumer)
                {
                    ChartNode.Series.Add(new Series(Grid.List_Node[i].Name));
                    ChartNode.Series[Grid.List_Node[i].Name].ChartType = SeriesChartType.Line;
                    ChartNode.Series[Grid.List_Node[i].Name].BorderWidth = 2;
                    ChartNode.Series[Grid.List_Node[i].Name].Color = Color.FromArgb(r, g, b);                 
                }
                h += DeltaHue;
            }
        }
        private void pictureBox1_PaintEdge(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            Canvas.DrawLine(new Pen(Color.Black, 1), 0, 0, PictureBoxGrid.Width-1, 0);
            Canvas.DrawLine(new Pen(Color.Black, 1), PictureBoxGrid.Width-1, 0, PictureBoxGrid.Width-1, PictureBoxGrid.Height-1);
            Canvas.DrawLine(new Pen(Color.Black, 1), PictureBoxGrid.Width-1, PictureBoxGrid.Height-1, 0, PictureBoxGrid.Height-1);
            Canvas.DrawLine(new Pen(Color.Black, 1), 0, PictureBoxGrid.Height-1, 0, 0);
        }
        private void pictureBox1_PaintBaseGrid(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            for (int i = 0; i < Grid.SizeX; i++)
            {
                for (int j = 0; j < Grid.SizeX; j++)
                {
                    Canvas.DrawEllipse(new Pen(Color.Gray, 2), PBXFactor * i + (PBXFactor/2), PBYFactor * j + (PBYFactor / 2), GridDotSizeX, GridDotSizeY);                   
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
                    Pen.Width = NodeSizeX;
                    Canvas.DrawEllipse(Pen, PBXFactor * node.Location.X - (NodeSizeX / 2) + (PBXFactor / 2), PBYFactor * node.Location.Y - (NodeSizeY / 2) + (PBYFactor / 2), NodeSizeX, NodeSizeY);

                }
                else if (node is Plant)
                {
                    Pen.Color = Color.Red;
                    Pen.Width = PlantSizeX;
                    Canvas.DrawEllipse(Pen, PBXFactor * node.Location.X - (PlantSizeX / 2) + (PBXFactor / 2), PBYFactor * node.Location.Y - (PlantSizeY / 2) + (PBYFactor / 2), PlantSizeX, PlantSizeY);

                }
                else if (node is Consumer)
                {
                    Pen.Color = Color.Green;
                    Pen.Width = ConsumerSizeX;
                    Canvas.DrawEllipse(Pen, PBXFactor * node.Location.X - (ConsumerSizeX / 2) + (PBXFactor / 2), PBYFactor * node.Location.Y - (ConsumerSizeY / 2) + (PBYFactor / 2), ConsumerSizeX, ConsumerSizeY);

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
                Canvas.DrawLine(new Pen(Color.Blue, PowerLineWidth), PBXFactor * PowerLine.StartLocation.X + (PBXFactor / 2), PBYFactor * PowerLine.StartLocation.Y + (PBYFactor / 2), PBXFactor * PowerLine.EndLocation.X + (PBXFactor / 2), PBYFactor * PowerLine.EndLocation.Y + (PBYFactor / 2));
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            foreach(Label label in PictureBoxGrid.Controls.OfType<Label>())
            {
                label.Show();
            }           
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            foreach (Label label in PictureBoxGrid.Controls.OfType<Label>())
            {
                label.Hide();
            }            
        }
        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void ButtonPause_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        private void CheckBoxChart(object sender, EventArgs e)
        {
            CheckBox CheckBox = (CheckBox)sender;
            string SerieName = CheckBox.AccessibleName;
            if (CheckBox.Checked)
            {
                ChartNode.Series[SerieName].Enabled = true;
            }
            else
            {
                ChartNode.Series[SerieName].Enabled = false;
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            TrackBar TrackBar = (TrackBar)sender;
            switch (TrackBar.Value)
            {
                case 0:
                    timer1.Interval = 100;
                    break;
                case 1:
                    timer1.Interval = 50;
                    break;
                case 2:
                    timer1.Interval = 10;
                    break;
                default:
                    timer1.Interval = 50;
                    break;
            }
        }
    }
}

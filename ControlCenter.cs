using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProjetSimulationReseaux
{
    /// <summary>
    /// A Graphical UI that takes a Grid object as parameters.
    /// Displays various information about said Grid in a windows Form window,
    /// including the location of plants/consumers, graphs of production/consumption
    /// and various data about the nodes of the Grid.
    /// Only used to update the Grid object and display the data,
    /// doesn't compute any data itself.
    /// Inherit from the Form Object.
    /// See <see cref="ProjetSimulationReseaux.Grid"/>.
    /// </summary>
    ///
    public partial class ControlCenter : Form
    {
        private readonly Grid Grid;
        private readonly int PBXFactor;
        private readonly int PBYFactor;

        private readonly int PlantSizeX;
        private readonly int PlantSizeY;
        private readonly int ConsumerSizeX;
        private readonly int ConsumerSizeY;
        private readonly int NodeSizeX;
        private readonly int NodeSizeY;
        private readonly int PowerLineWidth;
        private readonly int GridDotSizeX;
        private readonly int GridDotSizeY;

        /// <summary>
        /// A Graphical UI that takes a Grid object as parameters.
        /// Displays various information about said Grid in a windows Form window,
        /// including the location of plants/consumers, graphs of production/consumption
        /// and various data about the nodes of the Grid.
        /// Only used to update the Grid object and display the data,
        /// doesn't compute any data itself.
        /// Inherit from the Form Object.
        /// See <see cref="ProjetSimulationReseaux.Grid"/>.
        /// </summary>
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

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Grid.Update();
            UpdateAllChart();
            UpdateAllLabel();

            Update();
        }

        /// <summary>
        /// Updates the chart displaying data on the Form.
        /// </summary>
        private void UpdateAllChart()
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

        /// <summary>
        /// Updates the labels displaying data on the Form.
        /// </summary>
        private void UpdateAllLabel()
        {
            int i = 0;

            foreach (Plant plant in Grid.List_Node.OfType<Plant>())
            {
                groupBoxPlantProduction.Controls[i].Text = plant.PwProduction.ToString() + " / " + plant.MaxPwProduction.ToString() + " MW";
                if (plant.IsOn)
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
            i = 0;
            foreach (Fuel fuel in Grid.List_Fuel)
            {
                groupBoxFuelPrice.Controls[i].Text = fuel.CurrentPrice.ToString("C");
                i += 1;
            }
        }

        /// <summary>
        /// Runs once when the Windows Form is loaded.
        /// </summary>
        private void ControlCenter_Load(object sender, EventArgs e)
        {
            DrawAll();
            AddLabelInfoTable();
            AddCheckBoxInfoTable();
            AddLabelInfoMap();
            FillChartNode();
            foreach (ChartArea chartArea in ChartNode.ChartAreas)
            {
                chartArea.AxisX.Maximum = 200;
            }
            foreach (ChartArea chartArea in ChartTotal.ChartAreas)
            {
                chartArea.AxisX.Maximum = 200;
            }
        }

        /// <summary>
        /// Draws the grid on the Windows Form.
        /// </summary>
        private void DrawAll()
        {
            PictureBoxGrid.Paint += new PaintEventHandler(PictureBox1_PaintEdge);
            PictureBoxGrid.Paint += new PaintEventHandler(PictureBox1_PaintBaseGrid);
            PictureBoxGrid.Paint += new PaintEventHandler(PictureBox1_PaintPowerLine);
            PictureBoxGrid.Paint += new PaintEventHandler(PictureBox1_PaintNode);
        }

        /// <summary>
        /// Adds the data labels on the Windows Form.
        /// </summary>
        private void AddLabelInfoTable()
        {
            int PlantTotalCount = Grid.List_Node.OfType<Plant>().Count();
            int ConsumerTotalCount = Grid.List_Node.OfType<Consumer>().Count();
            int FuelTotalCount = Grid.List_Fuel.Count();
            Label[] LabelsInfoTablePlantsName = new Label[PlantTotalCount];
            Label[] LabelsInfoTablePlantsProduction = new Label[PlantTotalCount];
            Label[] LabelsInfoTablePlantsState = new Label[PlantTotalCount];
            Label[] LabelsInfoTableConsumerName = new Label[ConsumerTotalCount];
            Label[] LabelsInfoTableConsumerRequest = new Label[ConsumerTotalCount];
            Label[] LabelsInfoTableFuelName = new Label[FuelTotalCount];
            Label[] LabelsInfoTableFuelPrice = new Label[FuelTotalCount];
            int PlantCurrentCount = 0;
            int ConsumerCurrentCount = 0;
            int FuelCurrentCount = 0;

            foreach (Plant plant in Grid.List_Node.OfType<Plant>())
            {
                LabelsInfoTablePlantsName[PlantCurrentCount] = new Label
                {
                    AutoSize = true,
                    Text = plant.ToString(),
                    Location = new Point(5, 10 + 16 * PlantCurrentCount)
                };
                LabelsInfoTablePlantsProduction[PlantCurrentCount] = new Label
                {
                    AutoSize = true,
                    Text = plant.PwProduction.ToString() + " / " + plant.MaxPwProduction.ToString() + " MW",
                    Location = new Point(5, 10 + 16 * PlantCurrentCount)
                };
                LabelsInfoTablePlantsState[PlantCurrentCount] = new Label
                {
                    AutoSize = true
                };
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
                groupBoxPlantName.Controls.Add(LabelsInfoTablePlantsName[PlantCurrentCount]);
                groupBoxPlantProduction.Controls.Add(LabelsInfoTablePlantsProduction[PlantCurrentCount]);
                groupBoxPlantState.Controls.Add(LabelsInfoTablePlantsState[PlantCurrentCount]);
                PlantCurrentCount += 1;
            }
            foreach (Consumer consumer in Grid.List_Node.OfType<Consumer>())
            {
                LabelsInfoTableConsumerName[ConsumerCurrentCount] = new Label
                {
                    AutoSize = true,
                    Text = consumer.ToString(),
                    Location = new Point(5, 10 + 16 * ConsumerCurrentCount)
                };

                LabelsInfoTableConsumerRequest[ConsumerCurrentCount] = new Label
                {
                    AutoSize = true,
                    Text = consumer.PwRequest.ToString() + " MW",
                    Location = new Point(5, 10 + 16 * ConsumerCurrentCount)
                };
                groupBoxConsumerName.Controls.Add(LabelsInfoTableConsumerName[ConsumerCurrentCount]);
                groupBoxConsumerRequest.Controls.Add(LabelsInfoTableConsumerRequest[ConsumerCurrentCount]);
                ConsumerCurrentCount += 1;
            }
            foreach (Fuel fuel in Grid.List_Fuel)
            {
                LabelsInfoTableFuelName[FuelCurrentCount] = new Label
                {
                    AutoSize = true,
                    Text = fuel.ToString(),
                    Location = new Point(5, 10 + 16 * FuelCurrentCount)
                };

                LabelsInfoTableFuelPrice[FuelCurrentCount] = new Label
                {
                    AutoSize = true,
                    Text = fuel.CurrentPrice.ToString() + " $",
                    Location = new Point(5, 10 + 16 * FuelCurrentCount)
                };
                groupBoxFuelLabel.Controls.Add(LabelsInfoTableFuelName[FuelCurrentCount]);
                groupBoxFuelPrice.Controls.Add(LabelsInfoTableFuelPrice[FuelCurrentCount]);
                FuelCurrentCount += 1;
            }
        }

        /// <summary>
        /// Adds checkbox to each Nodes to toggle the display of that Node on the Node chart
        /// </summary>
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
                CheckBoxPlant[PlantCurrentCount] = new CheckBox
                {
                    Size = new Size(15, 15),
                    Checked = true,
                    AccessibleName = plant.ToString()
                };
                CheckBoxPlant[PlantCurrentCount].CheckStateChanged += new EventHandler(CheckBoxChart);
                CheckBoxPlant[PlantCurrentCount].Location = new Point(1, 10 + 16 * PlantCurrentCount);
                groupBoxCheckBoxPlant.Controls.Add(CheckBoxPlant[PlantCurrentCount]);
                PlantCurrentCount += 1;
            }
            foreach (Consumer consumer in Grid.List_Node.OfType<Consumer>())
            {
                CheckBoxConsumer[ConsumerCurrentCount] = new CheckBox
                {
                    Size = new Size(15, 15),
                    Checked = true,
                    AccessibleName = consumer.ToString()
                };
                CheckBoxConsumer[ConsumerCurrentCount].CheckStateChanged += new EventHandler(CheckBoxChart);
                CheckBoxConsumer[ConsumerCurrentCount].Location = new Point(1, 10 + 16 * ConsumerCurrentCount);
                groupBoxCheckBoxConsumer.Controls.Add(CheckBoxConsumer[ConsumerCurrentCount]);
                ConsumerCurrentCount += 1;
            }
        }

        /// <summary>
        /// Adds the names of the Nodes on the "map" on the Windows Form (only seen when hovering the map with the mouse cursor).
        /// </summary>
        private void AddLabelInfoMap()
        {
            int m = Grid.List_Node.Count;
            Label[] LabelsInfoMap = new Label[m];

            for (int i = 0; i < m; i++)
            {
                LabelsInfoMap[i] = new Label
                {
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    Text = Grid.List_Node[i].ToString(),
                    Location = new Point(PBXFactor * Grid.List_Node[i].Location.X - (NodeSizeX / 2) + (PBXFactor / 2) + 10, PBYFactor * Grid.List_Node[i].Location.Y - (NodeSizeY / 2) + (PBYFactor / 2) + 10)
                };
                LabelsInfoMap[i].Hide();
                PictureBoxGrid.Controls.Add(LabelsInfoMap[i]);
            }
        }

        /// <summary>
        /// Adds each Nodes as a series on the Node Chart (done once).
        /// This allows us to automatically update the content of the chart if new Nodes are added/removed.
        /// Note the genius trick i used to make every series a different color over the whole r,g,b spectrum :D.
        /// </summary>
        private void FillChartNode()
        {
            int n = Grid.List_Node.OfType<Plant>().Count();
            n += Grid.List_Node.OfType<Consumer>().Count();
            double h = 0;
            double DeltaHue = (double)360 / n;
            for (int i = 0; i < Grid.List_Node.Count; i++)
            {
                if (Grid.List_Node[i] is Junction)
                {
                    continue;
                }
                Hsv.HsvToRgb(h, 1, 0.7, out int r, out int g, out int b);
                if (Grid.List_Node[i] is Plant)
                {
                    ChartNode.Series.Add(new Series(Grid.List_Node[i].Name));
                    ChartNode.Series[Grid.List_Node[i].Name].ChartType = SeriesChartType.Line;
                    ChartNode.Series[Grid.List_Node[i].Name].BorderWidth = 2;
                    ChartNode.Series[Grid.List_Node[i].Name].Color = Color.FromArgb(r, g, b);
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

        private void PictureBox1_PaintEdge(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            Canvas.DrawLine(new Pen(Color.Black, 1), 0, 0, PictureBoxGrid.Width - 1, 0);
            Canvas.DrawLine(new Pen(Color.Black, 1), PictureBoxGrid.Width - 1, 0, PictureBoxGrid.Width - 1, PictureBoxGrid.Height - 1);
            Canvas.DrawLine(new Pen(Color.Black, 1), PictureBoxGrid.Width - 1, PictureBoxGrid.Height - 1, 0, PictureBoxGrid.Height - 1);
            Canvas.DrawLine(new Pen(Color.Black, 1), 0, PictureBoxGrid.Height - 1, 0, 0);
        }

        private void PictureBox1_PaintBaseGrid(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            for (int i = 0; i < Grid.SizeX; i++)
            {
                for (int j = 0; j < Grid.SizeY; j++)
                {
                    Canvas.DrawEllipse(new Pen(Color.Gray, 2), PBXFactor * i + (PBXFactor / 2), PBYFactor * j + (PBYFactor / 2), GridDotSizeX, GridDotSizeY);
                }
            }
        }

        private void PictureBox1_PaintNode(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            Pen Pen = new Pen(Color.Black);
            foreach (Node node in Grid.List_Node)
            {
                if (node is Junction)
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

        private void PictureBox1_PaintPowerLine(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            foreach (PowerLine PowerLine in Grid.List_PowerLine)
            {
                Canvas.DrawLine(new Pen(Color.Blue, PowerLineWidth), PBXFactor * PowerLine.StartLocation.X + (PBXFactor / 2), PBYFactor * PowerLine.StartLocation.Y + (PBYFactor / 2), PBXFactor * PowerLine.EndLocation.X + (PBXFactor / 2), PBYFactor * PowerLine.EndLocation.Y + (PBYFactor / 2));
            }
        }

        private void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            foreach (Label label in PictureBoxGrid.Controls.OfType<Label>())
            {
                label.Show();
            }
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
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

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
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
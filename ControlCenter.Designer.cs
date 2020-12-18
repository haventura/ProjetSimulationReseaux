namespace ProjetSimulationReseaux
{
    partial class ControlCenter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.ChartTotal = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PictureBoxGrid = new System.Windows.Forms.PictureBox();
            this.ChartNode = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ButtonStop = new System.Windows.Forms.Button();
            this.ButtonPlay = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxPlantName = new System.Windows.Forms.GroupBox();
            this.groupBoxPlantProduction = new System.Windows.Forms.GroupBox();
            this.groupBoxPlantState = new System.Windows.Forms.GroupBox();
            this.groupBoxConsumerName = new System.Windows.Forms.GroupBox();
            this.groupBoxConsumerRequest = new System.Windows.Forms.GroupBox();
            this.groupBoxCheckBoxPlant = new System.Windows.Forms.GroupBox();
            this.groupBoxCheckBoxConsumer = new System.Windows.Forms.GroupBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.ChartTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartNode)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // ChartTotal
            // 
            chartArea1.Name = "ChartArea1";
            this.ChartTotal.ChartAreas.Add(chartArea1);
            this.ChartTotal.Cursor = System.Windows.Forms.Cursors.Default;
            legend1.Name = "Legend1";
            this.ChartTotal.Legends.Add(legend1);
            this.ChartTotal.Location = new System.Drawing.Point(457, 12);
            this.ChartTotal.Name = "ChartTotal";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            series1.Legend = "Legend1";
            series1.Name = "TotalPwProduction";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "TotalPwRequest";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Green;
            series3.Legend = "Legend1";
            series3.Name = "PwDeficit";
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            series4.Legend = "Legend1";
            series4.Name = "TotalCO2Emission";
            series5.BorderWidth = 2;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.Fuchsia;
            series5.Legend = "Legend1";
            series5.Name = "TotalOperatingCost";
            this.ChartTotal.Series.Add(series1);
            this.ChartTotal.Series.Add(series2);
            this.ChartTotal.Series.Add(series3);
            this.ChartTotal.Series.Add(series4);
            this.ChartTotal.Series.Add(series5);
            this.ChartTotal.Size = new System.Drawing.Size(584, 339);
            this.ChartTotal.TabIndex = 0;
            this.ChartTotal.Text = "Total";
            title1.Name = "Title1";
            title1.Text = "Chart Total";
            this.ChartTotal.Titles.Add(title1);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PictureBoxGrid
            // 
            this.PictureBoxGrid.Location = new System.Drawing.Point(12, 12);
            this.PictureBoxGrid.Name = "PictureBoxGrid";
            this.PictureBoxGrid.Size = new System.Drawing.Size(414, 414);
            this.PictureBoxGrid.TabIndex = 1;
            this.PictureBoxGrid.TabStop = false;
            this.PictureBoxGrid.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.PictureBoxGrid.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            // 
            // ChartNode
            // 
            chartArea2.Name = "ChartArea1";
            this.ChartNode.ChartAreas.Add(chartArea2);
            this.ChartNode.Cursor = System.Windows.Forms.Cursors.Default;
            legend2.Name = "Legend1";
            this.ChartNode.Legends.Add(legend2);
            this.ChartNode.Location = new System.Drawing.Point(457, 357);
            this.ChartNode.Name = "ChartNode";
            this.ChartNode.Size = new System.Drawing.Size(584, 339);
            this.ChartNode.TabIndex = 2;
            this.ChartNode.Text = "Nodes";
            title2.Name = "Title1";
            title2.Text = "Chart Nodes";
            this.ChartNode.Titles.Add(title2);
            // 
            // ButtonStop
            // 
            this.ButtonStop.Location = new System.Drawing.Point(211, 432);
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.Size = new System.Drawing.Size(75, 23);
            this.ButtonStop.TabIndex = 3;
            this.ButtonStop.Text = "Stop";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.ButtonPause_Click);
            // 
            // ButtonPlay
            // 
            this.ButtonPlay.Location = new System.Drawing.Point(130, 432);
            this.ButtonPlay.Name = "ButtonPlay";
            this.ButtonPlay.Size = new System.Drawing.Size(75, 23);
            this.ButtonPlay.TabIndex = 4;
            this.ButtonPlay.Text = "Play";
            this.ButtonPlay.UseVisualStyleBackColor = true;
            this.ButtonPlay.Click += new System.EventHandler(this.ButtonPlay_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxPlantName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxPlantProduction, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxPlantState, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxConsumerName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxConsumerRequest, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxCheckBoxPlant, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxCheckBoxConsumer, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 475);
            this.tableLayoutPanel1.MaximumSize = new System.Drawing.Size(400, 800);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(100, 50);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(100, 50);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // groupBoxPlantName
            // 
            this.groupBoxPlantName.AutoSize = true;
            this.groupBoxPlantName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxPlantName.Location = new System.Drawing.Point(3, 3);
            this.groupBoxPlantName.Name = "groupBoxPlantName";
            this.groupBoxPlantName.Size = new System.Drawing.Size(6, 19);
            this.groupBoxPlantName.TabIndex = 0;
            this.groupBoxPlantName.TabStop = false;
            // 
            // groupBoxPlantProduction
            // 
            this.groupBoxPlantProduction.AutoSize = true;
            this.groupBoxPlantProduction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxPlantProduction.Location = new System.Drawing.Point(31, 3);
            this.groupBoxPlantProduction.Name = "groupBoxPlantProduction";
            this.groupBoxPlantProduction.Size = new System.Drawing.Size(6, 19);
            this.groupBoxPlantProduction.TabIndex = 1;
            this.groupBoxPlantProduction.TabStop = false;
            // 
            // groupBoxPlantState
            // 
            this.groupBoxPlantState.AutoSize = true;
            this.groupBoxPlantState.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxPlantState.Location = new System.Drawing.Point(67, 3);
            this.groupBoxPlantState.Name = "groupBoxPlantState";
            this.groupBoxPlantState.Size = new System.Drawing.Size(6, 19);
            this.groupBoxPlantState.TabIndex = 2;
            this.groupBoxPlantState.TabStop = false;
            // 
            // groupBoxConsumerName
            // 
            this.groupBoxConsumerName.AutoSize = true;
            this.groupBoxConsumerName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxConsumerName.Location = new System.Drawing.Point(3, 28);
            this.groupBoxConsumerName.Name = "groupBoxConsumerName";
            this.groupBoxConsumerName.Size = new System.Drawing.Size(6, 19);
            this.groupBoxConsumerName.TabIndex = 3;
            this.groupBoxConsumerName.TabStop = false;
            // 
            // groupBoxConsumerRequest
            // 
            this.groupBoxConsumerRequest.AutoSize = true;
            this.groupBoxConsumerRequest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxConsumerRequest.Location = new System.Drawing.Point(31, 28);
            this.groupBoxConsumerRequest.Name = "groupBoxConsumerRequest";
            this.groupBoxConsumerRequest.Size = new System.Drawing.Size(6, 19);
            this.groupBoxConsumerRequest.TabIndex = 4;
            this.groupBoxConsumerRequest.TabStop = false;
            // 
            // groupBoxCheckBoxPlant
            // 
            this.groupBoxCheckBoxPlant.AutoSize = true;
            this.groupBoxCheckBoxPlant.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxCheckBoxPlant.Location = new System.Drawing.Point(83, 3);
            this.groupBoxCheckBoxPlant.Name = "groupBoxCheckBoxPlant";
            this.groupBoxCheckBoxPlant.Size = new System.Drawing.Size(6, 19);
            this.groupBoxCheckBoxPlant.TabIndex = 5;
            this.groupBoxCheckBoxPlant.TabStop = false;
            // 
            // groupBoxCheckBoxConsumer
            // 
            this.groupBoxCheckBoxConsumer.AutoSize = true;
            this.groupBoxCheckBoxConsumer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxCheckBoxConsumer.Location = new System.Drawing.Point(83, 28);
            this.groupBoxCheckBoxConsumer.Name = "groupBoxCheckBoxConsumer";
            this.groupBoxCheckBoxConsumer.Size = new System.Drawing.Size(6, 19);
            this.groupBoxCheckBoxConsumer.TabIndex = 6;
            this.groupBoxCheckBoxConsumer.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(307, 432);
            this.trackBar1.Maximum = 2;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(119, 45);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.Value = 1;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // ControlCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 728);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ButtonPlay);
            this.Controls.Add(this.ButtonStop);
            this.Controls.Add(this.ChartNode);
            this.Controls.Add(this.PictureBoxGrid);
            this.Controls.Add(this.ChartTotal);
            this.Name = "ControlCenter";
            this.Text = "Control Center";
            this.Load += new System.EventHandler(this.ControlCenter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChartTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartNode)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ChartTotal;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox PictureBoxGrid;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartNode;
        private System.Windows.Forms.Button ButtonStop;
        private System.Windows.Forms.Button ButtonPlay;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxPlantName;
        private System.Windows.Forms.GroupBox groupBoxPlantProduction;
        private System.Windows.Forms.GroupBox groupBoxPlantState;
        private System.Windows.Forms.GroupBox groupBoxConsumerName;
        private System.Windows.Forms.GroupBox groupBoxConsumerRequest;
        private System.Windows.Forms.GroupBox groupBoxCheckBoxPlant;
        private System.Windows.Forms.GroupBox groupBoxCheckBoxConsumer;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}


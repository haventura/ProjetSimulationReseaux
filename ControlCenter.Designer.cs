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
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.ChartTotal = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ChartNode = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.ChartTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartNode)).BeginInit();
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
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            series1.Legend = "Legend1";
            series1.Name = "TotalPwProduction";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "TotalPwRequest";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Green;
            series3.Legend = "Legend1";
            series3.Name = "PwDeficit";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            series4.Legend = "Legend1";
            series4.Name = "TotalCO2Emission";
            this.ChartTotal.Series.Add(series1);
            this.ChartTotal.Series.Add(series2);
            this.ChartTotal.Series.Add(series3);
            this.ChartTotal.Series.Add(series4);
            this.ChartTotal.Size = new System.Drawing.Size(584, 339);
            this.ChartTotal.TabIndex = 0;
            this.ChartTotal.Text = "Chart Total";
            title1.Name = "Title1";
            title1.Text = "Chart Total";
            this.ChartTotal.Titles.Add(title1);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(414, 414);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
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
            this.ChartNode.Text = "Chart Nodes";
            title2.Name = "Title1";
            title2.Text = "Chart Nodes";
            this.ChartNode.Titles.Add(title2);
            // 
            // ControlCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 728);
            this.Controls.Add(this.ChartNode);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ChartTotal);
            this.Name = "ControlCenter";
            this.Text = "Control Center";
            this.Load += new System.EventHandler(this.ControlCenter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChartTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartNode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ChartTotal;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartNode;
    }
}


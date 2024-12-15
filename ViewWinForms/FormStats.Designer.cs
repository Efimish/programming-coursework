namespace ViewWinForms
{
    partial class FormStats
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartMostRented = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartMostRented)).BeginInit();
            this.SuspendLayout();
            // 
            // chartMostRented
            // 
            chartArea1.Name = "ChartArea1";
            this.chartMostRented.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartMostRented.Legends.Add(legend1);
            this.chartMostRented.Location = new System.Drawing.Point(12, 12);
            this.chartMostRented.Name = "chartMostRented";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartMostRented.Series.Add(series1);
            this.chartMostRented.Size = new System.Drawing.Size(493, 313);
            this.chartMostRented.TabIndex = 0;
            this.chartMostRented.Text = "chartMostRented";
            // 
            // FormStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 337);
            this.Controls.Add(this.chartMostRented);
            this.Name = "FormStats";
            this.Text = "Статистика";
            ((System.ComponentModel.ISupportInitialize)(this.chartMostRented)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartMostRented;
    }
}
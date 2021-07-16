namespace ContourMap
{
    partial class Form1
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
            this.plotViewContours = new OxyPlot.WindowsForms.PlotView();
            this.buttonVolume = new System.Windows.Forms.Button();
            this.buttonTrucks = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxVolume = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.buttonDiagram = new System.Windows.Forms.Button();
            this.tabPageP1 = new System.Windows.Forms.TabPage();
            this.plotViewColumnSeries1 = new OxyPlot.WindowsForms.PlotView();
            this.tabControlHillProfiles = new System.Windows.Forms.TabControl();
            this.NumberOfTrucks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NeededTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPageP1.SuspendLayout();
            this.tabControlHillProfiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // plotViewContours
            // 
            this.plotViewContours.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plotViewContours.ForeColor = System.Drawing.SystemColors.ControlText;
            this.plotViewContours.Location = new System.Drawing.Point(32, 427);
            this.plotViewContours.Name = "plotViewContours";
            this.plotViewContours.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewContours.Size = new System.Drawing.Size(505, 329);
            this.plotViewContours.TabIndex = 0;
            this.plotViewContours.Text = "plotView1";
            this.plotViewContours.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewContours.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewContours.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // buttonVolume
            // 
            this.buttonVolume.Location = new System.Drawing.Point(645, 211);
            this.buttonVolume.Name = "buttonVolume";
            this.buttonVolume.Size = new System.Drawing.Size(262, 53);
            this.buttonVolume.TabIndex = 3;
            this.buttonVolume.Text = "Calculate volume";
            this.buttonVolume.UseVisualStyleBackColor = true;
            this.buttonVolume.Click += new System.EventHandler(this.buttonVolume_Click);
            // 
            // buttonTrucks
            // 
            this.buttonTrucks.Location = new System.Drawing.Point(645, 317);
            this.buttonTrucks.Name = "buttonTrucks";
            this.buttonTrucks.Size = new System.Drawing.Size(262, 53);
            this.buttonTrucks.TabIndex = 4;
            this.buttonTrucks.Text = "Calculate optimal number of trucks";
            this.buttonTrucks.UseVisualStyleBackColor = true;
            this.buttonTrucks.Click += new System.EventHandler(this.buttonTrucks_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumberOfTrucks,
            this.NeededTime});
            this.dataGridView1.Location = new System.Drawing.Point(645, 427);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(262, 329);
            this.dataGridView1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(647, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "V:";
            // 
            // textBoxVolume
            // 
            this.textBoxVolume.Location = new System.Drawing.Point(674, 270);
            this.textBoxVolume.Name = "textBoxVolume";
            this.textBoxVolume.ReadOnly = true;
            this.textBoxVolume.Size = new System.Drawing.Size(100, 22);
            this.textBoxVolume.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(780, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "m³";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(642, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Insert the path of the file here";
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(645, 94);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(262, 22);
            this.textBoxFilePath.TabIndex = 10;
            // 
            // buttonDiagram
            // 
            this.buttonDiagram.Location = new System.Drawing.Point(645, 122);
            this.buttonDiagram.Name = "buttonDiagram";
            this.buttonDiagram.Size = new System.Drawing.Size(262, 61);
            this.buttonDiagram.TabIndex = 11;
            this.buttonDiagram.Text = "Draw diagrams";
            this.buttonDiagram.UseVisualStyleBackColor = true;
            this.buttonDiagram.Click += new System.EventHandler(this.buttonDiagram_Click);
            // 
            // tabPageP1
            // 
            this.tabPageP1.Controls.Add(this.plotViewColumnSeries1);
            this.tabPageP1.Location = new System.Drawing.Point(4, 25);
            this.tabPageP1.Name = "tabPageP1";
            this.tabPageP1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageP1.Size = new System.Drawing.Size(497, 311);
            this.tabPageP1.TabIndex = 0;
            this.tabPageP1.Text = "Profile 1";
            this.tabPageP1.UseVisualStyleBackColor = true;
            // 
            // plotViewColumnSeries1
            // 
            this.plotViewColumnSeries1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plotViewColumnSeries1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewColumnSeries1.Location = new System.Drawing.Point(3, 3);
            this.plotViewColumnSeries1.Name = "plotViewColumnSeries1";
            this.plotViewColumnSeries1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewColumnSeries1.Size = new System.Drawing.Size(491, 305);
            this.plotViewColumnSeries1.TabIndex = 2;
            this.plotViewColumnSeries1.Text = "plotView1";
            this.plotViewColumnSeries1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewColumnSeries1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewColumnSeries1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // tabControlHillProfiles
            // 
            this.tabControlHillProfiles.Controls.Add(this.tabPageP1);
            this.tabControlHillProfiles.Location = new System.Drawing.Point(32, 37);
            this.tabControlHillProfiles.Name = "tabControlHillProfiles";
            this.tabControlHillProfiles.SelectedIndex = 0;
            this.tabControlHillProfiles.Size = new System.Drawing.Size(505, 340);
            this.tabControlHillProfiles.TabIndex = 2;
            // 
            // NumberOfTrucks
            // 
            this.NumberOfTrucks.HeaderText = "Number of trucks";
            this.NumberOfTrucks.Name = "NumberOfTrucks";
            this.NumberOfTrucks.ReadOnly = true;
            // 
            // NeededTime
            // 
            this.NeededTime.HeaderText = "Needed time";
            this.NeededTime.Name = "NeededTime";
            this.NeededTime.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 786);
            this.Controls.Add(this.buttonDiagram);
            this.Controls.Add(this.textBoxFilePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxVolume);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonTrucks);
            this.Controls.Add(this.buttonVolume);
            this.Controls.Add(this.tabControlHillProfiles);
            this.Controls.Add(this.plotViewContours);
            this.Name = "Form1";
            this.Text = "Hillvolume and contour lines";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPageP1.ResumeLayout(false);
            this.tabControlHillProfiles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OxyPlot.WindowsForms.PlotView plotViewContours;
        private System.Windows.Forms.Button buttonVolume;
        private System.Windows.Forms.Button buttonTrucks;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxVolume;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Button buttonDiagram;
        private System.Windows.Forms.TabPage tabPageP1;
        private OxyPlot.WindowsForms.PlotView plotViewColumnSeries1;
        private System.Windows.Forms.TabControl tabControlHillProfiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberOfTrucks;
        private System.Windows.Forms.DataGridViewTextBoxColumn NeededTime;
    }
}


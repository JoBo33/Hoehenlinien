namespace ContourMap
{
    partial class Hillproject
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
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCalculate = new System.Windows.Forms.ToolStripMenuItem();
            this.volumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optimalNumberOfTrucksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDraw = new System.Windows.Forms.ToolStripMenuItem();
            this.hillProfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contourMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 476);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Path of the file";
            this.label3.Visible = false;
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(35, 505);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.ReadOnly = true;
            this.textBoxFilePath.Size = new System.Drawing.Size(262, 22);
            this.textBoxFilePath.TabIndex = 10;
            this.textBoxFilePath.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemCalculate,
            this.toolStripMenuItemDraw});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1372, 28);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(44, 24);
            this.toolStripMenuItemFile.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(120, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripMenuItemCalculate
            // 
            this.toolStripMenuItemCalculate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.volumeToolStripMenuItem,
            this.optimalNumberOfTrucksToolStripMenuItem});
            this.toolStripMenuItemCalculate.Name = "toolStripMenuItemCalculate";
            this.toolStripMenuItemCalculate.Size = new System.Drawing.Size(82, 24);
            this.toolStripMenuItemCalculate.Text = "Calculate";
            // 
            // volumeToolStripMenuItem
            // 
            this.volumeToolStripMenuItem.Name = "volumeToolStripMenuItem";
            this.volumeToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.volumeToolStripMenuItem.Text = "Volume";
            this.volumeToolStripMenuItem.Click += new System.EventHandler(this.volumeToolStripMenuItem_Click);
            // 
            // optimalNumberOfTrucksToolStripMenuItem
            // 
            this.optimalNumberOfTrucksToolStripMenuItem.Name = "optimalNumberOfTrucksToolStripMenuItem";
            this.optimalNumberOfTrucksToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.optimalNumberOfTrucksToolStripMenuItem.Text = "Optimal number of trucks";
            this.optimalNumberOfTrucksToolStripMenuItem.Click += new System.EventHandler(this.optimalNumberOfTrucksToolStripMenuItem_Click);
            // 
            // toolStripMenuItemDraw
            // 
            this.toolStripMenuItemDraw.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hillProfilesToolStripMenuItem,
            this.contourMapToolStripMenuItem});
            this.toolStripMenuItemDraw.Name = "toolStripMenuItemDraw";
            this.toolStripMenuItemDraw.Size = new System.Drawing.Size(56, 24);
            this.toolStripMenuItemDraw.Text = "Draw";
            // 
            // hillProfilesToolStripMenuItem
            // 
            this.hillProfilesToolStripMenuItem.Name = "hillProfilesToolStripMenuItem";
            this.hillProfilesToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.hillProfilesToolStripMenuItem.Text = "Hill profiles";
            this.hillProfilesToolStripMenuItem.Click += new System.EventHandler(this.hillProfilesToolStripMenuItem_Click);
            // 
            // contourMapToolStripMenuItem
            // 
            this.contourMapToolStripMenuItem.Name = "contourMapToolStripMenuItem";
            this.contourMapToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.contourMapToolStripMenuItem.Text = "Contour map";
            this.contourMapToolStripMenuItem.Click += new System.EventHandler(this.contourMapToolStripMenuItem_Click);
            // 
            // Hillproject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1372, 551);
            this.Controls.Add(this.textBoxFilePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Hillproject";
            this.Text = "Hillvolume and contour lines";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCalculate;
        private System.Windows.Forms.ToolStripMenuItem volumeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optimalNumberOfTrucksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDraw;
        private System.Windows.Forms.ToolStripMenuItem hillProfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contourMapToolStripMenuItem;
    }
}


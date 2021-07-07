namespace ContourSeriesTest
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
            this.plotViewTest = new OxyPlot.WindowsForms.PlotView();
            this.SuspendLayout();
            // 
            // plotViewTest
            // 
            this.plotViewTest.Location = new System.Drawing.Point(21, 12);
            this.plotViewTest.Name = "plotViewTest";
            this.plotViewTest.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewTest.Size = new System.Drawing.Size(820, 417);
            this.plotViewTest.TabIndex = 0;
            this.plotViewTest.Text = "plotViewtest";
            this.plotViewTest.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewTest.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewTest.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.plotViewTest.Click += new System.EventHandler(this.plotViewTest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 450);
            this.Controls.Add(this.plotViewTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private OxyPlot.WindowsForms.PlotView plotViewTest;
    }
}


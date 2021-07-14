using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ContourSeriesTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        PlotModel model = new PlotModel { Title = "ContourSeries" };
        public void DrawContours()
        {

            double x0 = 0;
            double x1 = 6;
            double y0 = 0;
            double y1 = 6;

            //generate values
            Func<double, double, double> peaks = (x, y) => -(Math.Pow(x - 4, 2) + Math.Pow(y - 4, 2)) + 4;
            double[] xx = ArrayBuilder.CreateVector(x0, x1, 100);
            double[] yy = ArrayBuilder.CreateVector(y0, y1, 100);
            double[,] peaksData = ArrayBuilder.Evaluate(peaks, xx, yy);

            ContourSeries cs = new ContourSeries
            {
                Color = OxyColors.Black,
                LabelBackground = OxyColors.White,
                ColumnCoordinates = yy,
                RowCoordinates = xx,
                Data = peaksData
            };
            model.Series.Add(cs);
        }

        private void plotViewTest_Click(object sender, EventArgs e)
        {
            plotViewTest.Model = model;
            DrawContours();
        }
    }
}

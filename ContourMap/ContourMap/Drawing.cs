using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourMap
{
    class Drawing
    {


        public static void CatmullRomSplines(List<double[]> contourLine, PlotModel model)
        {
            LineSeries line = new LineSeries();

            for (float t = 0; t <= contourLine.Count; t += 0.01f)
            {
                int p1 = (int)t;
                int p2 = (p1 + 1) % (contourLine.Count );
                int p3 = (p2 + 1) % (contourLine.Count );
                int p0 = p1 >= 1 ? p1 - 1 : contourLine.Count - 1;

                float tmp = t - (int)t;
                float tt = tmp * tmp;
                float ttt = tmp * tmp * tmp;

                float q1 = -ttt + 2.0f * tt - tmp;
                float q2 = 3.0f * ttt - 5.0f * tt + 2;
                float q3 = -3.0f * ttt + 4.0f * tt + tmp;
                float q4 = ttt - tt;

                double x = 0.5 * (contourLine[p0][0] * q1 + contourLine[p1][0] * q2 + contourLine[p2][0] * q3 + contourLine[p3][0] * q4);
                double y = 0.5 * (contourLine[p0][1] * q1 + contourLine[p1][1] * q2 + contourLine[p2][1] * q3 + contourLine[p3][1] * q4);

                line.Points.Add(new DataPoint(x, y));
            }

            model.Series.Add(line);


            model.InvalidatePlot(true);
        }

        public static List<double[]> SortContourPoints(ref List<double[]> contourLine)
        {
            List<double[]> sortedContourLine = new List<double[]> { contourLine[0] };
            contourLine.RemoveAt(0);
            for (int j = 0; j < sortedContourLine.Count; j++)
            {
                int removeIndex = 0;
                double distance = Math.Pow(sortedContourLine[j][0] - contourLine[0][0], 2) + Math.Pow(sortedContourLine[j][1] - contourLine[0][1], 2); // distanz von punkt j zu einem punkt der auf dem kreis liegen könnte
                double[] chosenPoint = contourLine[0];
                for (int i = 1; i < contourLine.Count; i++)
                {
                    double testDistance = Math.Pow(sortedContourLine[j][0] - contourLine[i][0], 2) + Math.Pow(sortedContourLine[j][1] - contourLine[i][1], 2); // dinstanz von punkt j zu punkt i 
                    if (distance > testDistance)  // wurde ein punkt gefunden der näher an dem derzeitigen endpunkt liegt, werden die daten dementsprechend angepasst
                    {
                        distance = testDistance;
                        chosenPoint = contourLine[i];
                        removeIndex = i;
                    }
                }
                double distanceToStart = Math.Pow(sortedContourLine[0][0] - sortedContourLine[j][0], 2) + Math.Pow(sortedContourLine[0][1] - sortedContourLine[j][1], 2); // distanz vom startpunkt des kreises zum letzten gefundenen punkt des kreises
                if (j > 1 && distance > distanceToStart) // ist die kürzeste distanz von dem endpunkt zu einem punkt der noch nicht im kreis ist länger als die distanz zwischen endpunkt und startpunkt wird der kreis geschlossen
                {
                    return sortedContourLine;
                }
                sortedContourLine.Add(chosenPoint);  // punkt mit der kürzesten distanz wird zum kreis hinzugefüngt
                contourLine.RemoveAt(removeIndex); // der punkt wird aus der liste der nicht enthaltenen punkte entfernt
                if (contourLine.Count == 0)
                {
                    return sortedContourLine;
                }
            }
            return sortedContourLine;
                }

        public static void DrawColumnSeries(ref PlotModel plot, List<double[]> data, int pointsInOneRow, double maxHeight, int rowNumber = 0)
        {
           
            ColumnSeries columnSeries = PrepareColumnSeries(plot, maxHeight);

            for (int i = 0; i < pointsInOneRow; i++)
            {
                columnSeries.Items.Add(new ColumnItem(data[i + pointsInOneRow * rowNumber][2]));
            }

            plot.Series.Add(columnSeries);
            plot.InvalidatePlot(true);

        }

        private static ColumnSeries PrepareColumnSeries(PlotModel plot, double maxHeight)
        {
            ColumnSeries columnSeries = new ColumnSeries();

            LinearAxis height = new LinearAxis { Minimum = 0, Maximum = maxHeight };
            height.IsZoomEnabled = false;
            plot.Axes.Add(height);

            CategoryAxis xAxis = new CategoryAxis { };
            xAxis.IsZoomEnabled = false;
            plot.Axes.Add(xAxis);

            return columnSeries;
        }


        

       
    }
}

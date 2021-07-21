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
        public static void DetermineContourLines(List<double[]> data, PlotModel model, int pointsInOneRow)
        {
            for (int i = 0; i <= EditingData.FindMaxHeight(data); i += (1)) 
            {
                List<double[]> contourLine = new List<double[]> { };

                for (int j = 0; j < data.Count - 1; j += (1))
                {
                    if (j < data.Count - pointsInOneRow)
                    {
                        if (data[j][2] < i && data[j + pointsInOneRow][2] > i || data[j][2] > i && data[j + pointsInOneRow][2] < i)
                        {
                            double tmp = (i - data[j][2]) / (data[j + pointsInOneRow][2] - data[j][2]);
                            double[] pointOnHeight = { data[j][0] + tmp * (data[j + pointsInOneRow][0] - data[j][0]), data[j][1] + tmp * (data[j + pointsInOneRow][1] - data[j][1]), i };
                            contourLine.Add(pointOnHeight);
                        }
                    }
                    if ((j % pointsInOneRow) - (pointsInOneRow - 1) == 0 && j != 0)
                    {
                        continue;
                    }
                    if (data[j][2] < i && data[j + 1][2] > i || data[j][2] > i && data[j + 1][2] < i)
                    {
                        double tmp = (i - data[j][2]) / (data[j + 1][2] - data[j][2]);
                        double[] pointOnHeight = { data[j][0] + tmp * (data[j + 1][0] - data[j][0]), data[j][1] + tmp * (data[j + 1][1] - data[j][1]), i };
                        contourLine.Add(pointOnHeight);
                    }
                }

                if (contourLine.Count != 0)
                {
                    sortContourPoints(ref contourLine);
                    CatmullRomSplines(contourLine, model);
                }
            }
        }

        private static void CatmullRomSplines(List<double[]> contourLine, PlotModel model)
        {
            LineSeries line = new LineSeries();

            for (float t = 0; t < contourLine.Count - 1; t += 0.01f)
            {
                int p1 = (int)t;
                int p2 = (p1 + 1) % (contourLine.Count - 1);
                int p3 = (p2 + 1) % (contourLine.Count - 1);
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

        public static void sortContourPoints(ref List<double[]> contourLine)
        {
            List<double[]> sortedContourLine = new List<double[]> { };

            for (int i = 0; i < contourLine.Count; i++)
            {
                for (int j = 0; j < contourLine.Count - 1; j++)
                {
                    if (contourLine[j][0] > contourLine[j + 1][0])
                    {
                        EditingData.Swap(ref contourLine, j, j + 1);
                    }
                }
            }

            for (int i = 0; i < contourLine.Count - 1; i++)
            {
                if (contourLine[i][0] == contourLine[i + 1][0] && contourLine[i][1] > contourLine[i + 1][1])
                {
                    EditingData.Swap(ref contourLine, i, i + 1);
                }
            }

            for (int i = 0; i < contourLine.Count; i += 2)
            {
                sortedContourLine.Add(contourLine[i]);
            }

            for (int i = contourLine.Count - 1; i >= 0; i -= 2)
            {
                sortedContourLine.Add(contourLine[i]);
            }

            contourLine = sortedContourLine;
        }

        public static void DrawColumnSeries(ref PlotModel plot, List<double[]> data, int pointsInOneRow, int rowNumber = 0)
        {
            double maxHeight = EditingData.FindMaxHeight(data);

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

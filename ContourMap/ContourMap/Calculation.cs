using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourMap
{
    class Calculation
    {
        public static void DeterminePointsInOneRow(List<double[]> data, ref int pointsInOneRow)
        {
            pointsInOneRow = 0;
            for (int i = 0; i < data.Count; i += (1))
            {
                pointsInOneRow = pointsInOneRow + 1;
                if (data[i][1] != data[i + 1][1])
                {
                    break;
                }
            }
        }
        public static double CalculateVolume(List<double[]> data, int pointsInOneRow)
        {
            double volume = 0;
            double ground = Math.Pow((data[1][0] - data[0][0]), 2);

            for (int i = 0; i < data.Count - pointsInOneRow - 1; i += (1))
            {
                if ((i % pointsInOneRow) - (pointsInOneRow - 1) == 0 && i != 0)
                {
                    continue;
                }

                double height = (data[i][2] + data[i + 1][2] + data[i + pointsInOneRow][2] + data[i + pointsInOneRow + 1][2]) / 4;
                volume = volume + ground * height;
            }
            return volume;
        }

       
        public static double CalculateTimeTrucksNeeded(double volume, int i)
        {
            return Math.Ceiling((volume / 7) / i) * 30;
        }

        public static void DetermineContourLines(List<double[]> data, PlotModel model, int pointsInOneRow, double maxHeight)
        {
            LineSeries gridPoints = new LineSeries()
            {
                MarkerType = MarkerType.Circle,
                LineStyle = LineStyle.None,
                Color = OxyColors.Black
            };
            foreach (double[] point in data)
            {

                gridPoints.Points.Add(new DataPoint(point[0], point[1]));
            }

            model.Series.Add(gridPoints);

            for (int i = 0; i <= maxHeight; i += (1))
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
                            LineSeries points = new LineSeries()
                            {
                                MarkerType = MarkerType.Circle,
                                LineStyle = LineStyle.None,
                                Color = OxyColors.Red
                            };
                            points.Points.Add(new DataPoint(pointOnHeight[0], pointOnHeight[1]));

                            model.Series.Add(points);
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
                        LineSeries points = new LineSeries()
                        {
                            MarkerType = MarkerType.Circle,
                            LineStyle = LineStyle.None,
                            Color = OxyColors.Red
                        };
                        points.Points.Add(new DataPoint(pointOnHeight[0], pointOnHeight[1]));

                        model.Series.Add(points);
                    }
                }
                //if (data[data.Count - 1][2] == i)
                //{
                //    contourLine.Add(data[data.Count - 1]);
                //}
                while (contourLine.Count > 2)
                {
                    List<double[]> oneContourLine = Drawing.SortContourPoints(ref contourLine);
                    Drawing.CatmullRomSplines(oneContourLine, model);
                }
            }
        }


    }
}
